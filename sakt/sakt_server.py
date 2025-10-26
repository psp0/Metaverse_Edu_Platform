import os
import json
import torch
import numpy as np
import pandas as pd
from flask import Flask, request, jsonify

from torch.utils.data import Dataset, DataLoader, random_split
from torch.optim import Adam
from torch.nn import Module, Embedding, Linear, LayerNorm, Dropout, Sequential, ReLU, MultiheadAttention, Parameter
from torch.nn.init import kaiming_normal_
from torch.nn.functional import binary_cross_entropy
from torch.nn.utils.rnn import pad_sequence
from sklearn.metrics import roc_auc_score

# --- 상수 정의 ---
DEVICE = torch.device("cuda" if torch.cuda.is_available() else "cpu")
DATA_DIR = "data"
MODEL_DIR = "models"
MODEL_PATH = os.path.join(MODEL_DIR, "sakt_model.pt")
Q_MATRIX_PATH = os.path.join(MODEL_DIR, "q_matrix.json")
SEQ_LEN = 100
EMBED_DIM = 128
NUM_HEADS = 8
BATCH_SIZE = 64
EPOCHS = 20
PAD_VAL = -1

# --- 1. 데이터 전처리 ---
class SAKTDataset(Dataset):
    def __init__(self, q_seqs, r_seqs):
        self.q_seqs = q_seqs
        self.r_seqs = r_seqs

    def __len__(self):
        return len(self.q_seqs)

    def __getitem__(self, index):
        q_seq, r_seq = self.q_seqs[index], self.r_seqs[index]
        return torch.LongTensor(q_seq[:-1]), torch.LongTensor(r_seq[:-1]), torch.LongTensor(q_seq[1:]), torch.LongTensor(r_seq[1:])

def match_seq_len(q_seqs, r_seqs, seq_len, pad_val=PAD_VAL):
    proc_q_seqs, proc_r_seqs = [], []
    for q_seq, r_seq in zip(q_seqs, r_seqs):
        i = 0
        chunk_size = seq_len + 1
        while i + chunk_size <= len(q_seq):
            proc_q_seqs.append(q_seq[i:i + chunk_size])
            proc_r_seqs.append(r_seq[i:i + chunk_size])
            i += chunk_size
        
        final_chunk = q_seq[i:]
        if len(final_chunk) > 1:
            pad_size = chunk_size - len(final_chunk)
            proc_q_seqs.append(np.concatenate([final_chunk, np.full(pad_size, pad_val)]))
            proc_r_seqs.append(np.concatenate([r_seq[i:], np.full(pad_size, pad_val)]))
            
    return proc_q_seqs, proc_r_seqs

def collate_fn(batch, pad_val=PAD_VAL):
    q_in, r_in, q_target, r_target = zip(*batch)
    q_in = pad_sequence(q_in, batch_first=True, padding_value=pad_val)
    r_in = pad_sequence(r_in, batch_first=True, padding_value=pad_val)
    q_target = pad_sequence(q_target, batch_first=True, padding_value=pad_val)
    r_target = pad_sequence(r_target, batch_first=True, padding_value=pad_val)
    mask = (q_target != pad_val)
    return q_in, r_in, q_target, r_target, mask

# --- 2. SAKT 모델 정의 ---
class SAKT(Module):
    def __init__(self, num_q, seq_len, embed_dim, num_attn_heads, dropout=0.2):
        super().__init__()
        self.num_q, self.seq_len, self.embed_dim = num_q, seq_len, embed_dim
        self.interaction_embed = Embedding(num_q * 2, embed_dim)
        self.exercise_embed = Embedding(num_q, embed_dim)
        self.positional_embed = Parameter(torch.Tensor(seq_len, embed_dim))
        kaiming_normal_(self.positional_embed)
        self.attn = MultiheadAttention(embed_dim, num_attn_heads, dropout=dropout)
        self.attn_layer_norm = LayerNorm(embed_dim)
        self.ffn = Sequential(Linear(embed_dim, embed_dim), ReLU(), Dropout(dropout), Linear(embed_dim, embed_dim))
        self.ffn_layer_norm = LayerNorm(embed_dim)
        self.predictor = Linear(embed_dim, 1)

    # [핵심 수정 1] forward 함수가 '다음에 풀 문제(q_target)'를 입력받도록 변경
    def forward(self, q, r, q_target):
        non_pad_mask = (q != PAD_VAL)
        q_safe = q.clone().masked_fill_(~non_pad_mask, 0)
        r_safe = r.clone().masked_fill_(~non_pad_mask, 0)
        q_target_safe = q_target.clone().masked_fill_(~(q_target != PAD_VAL), 0)

        x = q_safe + self.num_q * r_safe
        
        interaction_embeds = self.interaction_embed(x)
        # [핵심 수정 2] Query(E)를 만들 때, 과거 문제(q)가 아닌 다음 문제(q_target)의 임베딩을 사용
        E = self.exercise_embed(q_target_safe) + self.positional_embed.unsqueeze(0)
        M = interaction_embeds + self.positional_embed.unsqueeze(0)
        
        causal_mask = torch.triu(torch.ones(self.seq_len, self.seq_len), diagonal=1).bool().to(DEVICE)
        
        S, _ = self.attn(E.permute(1, 0, 2), M.permute(1, 0, 2), M.permute(1, 0, 2), attn_mask=causal_mask)
        S = self.attn_layer_norm(S.permute(1, 0, 2) + M)
        
        F = self.ffn(S)
        F = self.ffn_layer_norm(F + S)
        
        p = torch.sigmoid(self.predictor(F)).squeeze(-1)
        return p

# --- 3. 학습 및 예측 로직 ---
class SAKTModelHandler:
    def __init__(self):
        self.model, self.q_matrix, self.num_q = None, None, 0
        self._load_metadata()

    def _load_metadata(self):
        if os.path.exists(Q_MATRIX_PATH):
            with open(Q_MATRIX_PATH, 'r') as f: self.q_matrix = json.load(f)
            self.num_q = len(self.q_matrix)

    def train(self):
        if not os.path.exists(MODEL_DIR): os.makedirs(MODEL_DIR)
        
        df = pd.read_csv(os.path.join(DATA_DIR, 'skill_builder_data.csv'), usecols=['user_id', 'skill_name', 'correct'],
                         encoding='unicode_escape', low_memory=False).dropna()
        df.rename(columns={'skill_name': 'problem_id', 'correct': 'is_correct'}, inplace=True)
        df['problem_id'] = df['problem_id'].astype(str)
        
        problems = sorted(df['problem_id'].unique())
        self.q_matrix = {problem: i for i, problem in enumerate(problems)}
        self.num_q = len(problems)
        with open(Q_MATRIX_PATH, 'w') as f: json.dump(self.q_matrix, f)
        
        grouped_df = df.groupby('user_id')
        q_seqs, r_seqs = [], []
        for _, group in grouped_df:
            q_seqs.append([self.q_matrix[p] for p in group['problem_id'].values])
            r_seqs.append(group['is_correct'].values)

        proc_q, proc_r = match_seq_len(q_seqs, r_seqs, SEQ_LEN)
        if not proc_q: raise ValueError("No valid sequences created from the data. Check data format and length.")
        
        dataset = SAKTDataset(proc_q, proc_r)
        train_size = int(len(dataset) * 0.8)
        test_size = len(dataset) - train_size
        train_dataset, test_dataset = random_split(dataset, [train_size, test_size])
        train_loader = DataLoader(train_dataset, batch_size=BATCH_SIZE, shuffle=True, collate_fn=collate_fn)
        test_loader = DataLoader(test_dataset, batch_size=len(test_dataset) or 1, shuffle=False, collate_fn=collate_fn)

        self.model = SAKT(self.num_q, SEQ_LEN, EMBED_DIM, NUM_HEADS).to(DEVICE)
        optimizer = Adam(self.model.parameters(), lr=1e-3)

        for epoch in range(EPOCHS):
            self.model.train()
            for data in train_loader:
                q, r, q_target, r_target, m = [d.to(DEVICE) for d in data]
                optimizer.zero_grad()
                # [핵심 수정 3] 모델 호출 시 q_target을 함께 전달
                p = self.model(q, r, q_target)
                p, t = torch.masked_select(p, m), torch.masked_select(r_target, m)
                loss = binary_cross_entropy(p, t.float())
                loss.backward()
                optimizer.step()
            print(f"Epoch {epoch+1}/{EPOCHS}, Loss: {loss.item():.4f}")

        self.model.eval()
        with torch.no_grad():
            for data in test_loader:
                q, r, q_target, r_target, m = [d.to(DEVICE) for d in data]
                # [핵심 수정 4] 평가 시에도 q_target을 함께 전달
                p = self.model(q, r, q_target)
                p, t = torch.masked_select(p, m).cpu().numpy(), torch.masked_select(r_target, m).cpu().numpy()
        
        if len(np.unique(t)) > 1:
            auc = roc_auc_score(t, p)
        else:
            auc = 0.5
            print("Warning: Test set contains only one class. AUC is set to 0.5.")

        loss = binary_cross_entropy(torch.from_numpy(p), torch.from_numpy(t).float()).item()
        print(f"Training Finished. AUC: {auc:.4f}, Loss: {loss:.4f}")
        torch.save(self.model.state_dict(), MODEL_PATH)
        return auc, loss

    def predict(self, df):
        if self.model is None:
            if not os.path.exists(MODEL_PATH): raise FileNotFoundError("Model not trained. Please call /sakt/train first.")
            self.model = SAKT(self.num_q, SEQ_LEN, EMBED_DIM, NUM_HEADS).to(DEVICE)
            self.model.load_state_dict(torch.load(MODEL_PATH, map_location=DEVICE))
        self.model.eval()
        
        df['problem_id'] = df['problem_id'].astype(str)
        q_seq = [self.q_matrix.get(p, PAD_VAL) for p in df['problem_id'].values]
        r_seq = df['is_correct'].values
        
        valid_indices = np.array(q_seq) != PAD_VAL
        q_seq, r_seq = np.array(q_seq)[valid_indices], np.array(r_seq)[valid_indices]

        if len(q_seq) == 0: return 30.0

        q_seq_in, r_seq_in = q_seq[:-1], r_seq[:-1]
        q_seq_target = q_seq[1:]
        
        pad_len = SEQ_LEN - len(q_seq_in)
        q_in_padded = np.concatenate([q_seq_in, np.full(pad_len, PAD_VAL)])
        r_in_padded = np.concatenate([r_seq_in, np.full(pad_len, PAD_VAL)])
        q_target_padded = np.concatenate([q_seq_target, np.full(pad_len, PAD_VAL)])
        
        q_tensor = torch.LongTensor(q_in_padded).unsqueeze(0).to(DEVICE)
        r_tensor = torch.LongTensor(r_in_padded).unsqueeze(0).to(DEVICE)
        q_target_tensor = torch.LongTensor(q_target_padded).unsqueeze(0).to(DEVICE)
        
        with torch.no_grad():
            # [핵심 수정 5] 예측 시에도 q_target을 전달
            p = self.model(q_tensor, r_tensor, q_target_tensor)
            # 마지막 문제(len(q_seq)-1)를 풀었을 때의 예측값을 가져옴
            proficiency = p[0, len(q_seq)-2].item() * 100
        return round(proficiency, 2)

# --- 4. Flask 웹 서버 ---
app = Flask(__name__)
handler = SAKTModelHandler()

@app.route('/sakt/train', methods=['POST'])
def train_endpoint():
    try:
        auc, loss = handler.train()
        return jsonify({"message": "Training completed.", "final_auc": auc, "final_loss": loss})
    except Exception as e:
        import traceback; traceback.print_exc()
        return jsonify({"error": str(e)}), 500

@app.route('/sakt/predict', methods=['POST'])
def predict_endpoint():
    try:
        data = request.json
        df = pd.DataFrame(data['problemHistory'])
        df.rename(columns={'problemId': 'problem_id', 'isCorrect': 'is_correct'}, inplace=True)
        proficiency = handler.predict(df)
        return jsonify({"newProficiencyPercentage": proficiency})
    except Exception as e:
        import traceback; traceback.print_exc()
        return jsonify({"error": str(e)}), 500

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5001, debug=False)