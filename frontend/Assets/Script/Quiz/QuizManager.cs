using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

[System.Serializable]
public class QuizOption
{
    public int optionId;
    public string label;
    public string optionText;
    public bool isAnswer; // ✅ 더미 모드에서 정답 판별용
}

[System.Serializable]
public class QuizQuestion
{
    public int quizId;
    public string content;
    public QuizOption[] options;
}

[System.Serializable]
public class QuizAnswerRequest
{
    public int quizId;
    public int selectedOptionId;
}

[System.Serializable]
public class QuizAnswerResponse
{
    public string commentary;
    public string correctionUpdateAt;
    public bool correct;
}

public class QuizManager : MonoBehaviour
{
    [Header("Mode")]
    public bool useDummy = true; // ✅ Inspector에서 더미/서버 모드 전환

    [Header("API Info")]
    public string baseUrl = "http://localhost:8080/api/quizzes";
    public string token = "ey..."; // JWT

    public QuizQuestion[] quizList;

    // 🔹 퀴즈 불러오기
    public void LoadQuiz(int subUnitId, System.Action onLoaded)
    {
        if (useDummy)
        {
            LoadQuizFromDummy(onLoaded);
        }
        else
        {
            StartCoroutine(LoadQuizFromServer(subUnitId, onLoaded));
        }
    }

    private IEnumerator LoadQuizFromServer(int subUnitId, System.Action onLoaded)
    {
        string url = $"{baseUrl}/sub-unit/{subUnitId}/state";

        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("Authorization", "Bearer " + token);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            string wrapped = "{\"questions\":" + json + "}";
            QuizQuestionArray arr = JsonUtility.FromJson<QuizQuestionArray>(wrapped);
            quizList = arr.questions;

            onLoaded?.Invoke();
        }
        else
        {
            Debug.LogError("퀴즈 로드 실패: " + www.error);
        }
    }

    private void LoadQuizFromDummy(System.Action onLoaded)
    {
        TextAsset dummyJson = Resources.Load<TextAsset>("Dummy/Quiz_dummy");
        if (dummyJson != null)
        {
            QuizQuestionArray arr = JsonUtility.FromJson<QuizQuestionArray>(dummyJson.text);
            quizList = arr.questions;
            onLoaded?.Invoke();
        }
        else
        {
            Debug.LogError("더미 퀴즈 파일을 찾을 수 없습니다!");
        }
    }

    // 🔹 정답 제출
    public IEnumerator SendAnswer(int quizId, int selectedOptionId, System.Action<QuizAnswerResponse> onComplete)
    {
        if (useDummy)
        {
            // ✅ 더미 모드: quizList에서 직접 정답 판별
            var question = System.Array.Find(quizList, q => q.quizId == quizId);
            var chosen = System.Array.Find(question.options, o => o.optionId == selectedOptionId);
            var correctOption = System.Array.Find(question.options, o => o.isAnswer);

            QuizAnswerResponse dummyRes = new QuizAnswerResponse
            {
                commentary = correctOption != null
                    ? $"정답은 {correctOption.optionText} 입니다."
                    : "정답 데이터를 찾을 수 없습니다.",
                correct = chosen != null && chosen.isAnswer,
                correctionUpdateAt = System.DateTime.Now.ToString()
            };

            onComplete?.Invoke(dummyRes);
            yield break;
        }
        else
        {
            // ✅ 서버 모드
            QuizAnswerRequest req = new QuizAnswerRequest { quizId = quizId, selectedOptionId = selectedOptionId };
            string jsonBody = JsonUtility.ToJson(req);

            UnityWebRequest request = new UnityWebRequest($"{baseUrl}/answer", "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + token);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var res = JsonUtility.FromJson<QuizAnswerResponse>(request.downloadHandler.text);
                onComplete?.Invoke(res);
            }
            else
            {
                Debug.LogError("정답 제출 실패: " + request.error);
            }
        }
    }

    [System.Serializable]
    private class QuizQuestionArray { public QuizQuestion[] questions; }
}
