using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    public static ChatManager instance;
    private ChatClient chatClient;

    private const string GLOBAL_CHANNEL = "Global";
    private bool isConnected = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        // ✅ UserProfileUI에서 닉네임을 이미 설정함 (PhotonNetwork.NickName)
        if (string.IsNullOrEmpty(PhotonNetwork.NickName))
        {
            PhotonNetwork.NickName = $"Player_{System.Environment.TickCount % 10000}";
            Debug.LogWarning($"⚠️ 닉네임 미설정 — 임시 닉네임 사용: {PhotonNetwork.NickName}");
        }

        chatClient = new ChatClient(this);
        chatClient.Connect(
            PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat,
            PhotonNetwork.AppVersion,
            new Photon.Chat.AuthenticationValues(PhotonNetwork.NickName)
        );

        Debug.Log($"💬 [ChatManager] 채팅 서버 연결 시도 중... (닉네임: {PhotonNetwork.NickName})");
    }

    private void Update()
    {
        chatClient?.Service();
    }

    // ✅ 전체 채팅 전송 (Global Channel)
    public void SendMessageToAll(string message)
    {
        if (!isConnected || string.IsNullOrEmpty(message))
        {
            Debug.LogWarning("⚠️ 채팅 서버 연결 전이거나 빈 메시지입니다.");
            return;
        }

        chatClient.PublishMessage(GLOBAL_CHANNEL, message);
        // ❌ 내 메시지는 서버 브로드캐스트로 자동 수신됨 → 여기서 따로 표시하지 않음
    }

    #region Photon Chat Callbacks

    public void OnConnected()
    {
        Debug.Log("✅ Photon Chat Connected");
        chatClient.Subscribe(new string[] { GLOBAL_CHANNEL });
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        isConnected = true;
        Debug.Log($"📢 채널 구독 완료: {channels[0]}");
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        if (channelName != GLOBAL_CHANNEL) return;

        for (int i = 0; i < senders.Length; i++)
        {
            string sender = senders[i];
            string msg = messages[i].ToString();

            // ✅ 자기 자신 메시지 구분
            if (sender == PhotonNetwork.NickName)
            {
                ChatUIManager.instance.DisplayMessage($"<color=#22c55e>[나]</color> {msg}");
            }
            else
            {
                ChatUIManager.instance.DisplayMessage($"<color=#60a5fa>[{sender}]</color> {msg}");
            }
        }
    }

    public void OnDisconnected()
    {
        Debug.Log("❌ Photon Chat Disconnected");
        isConnected = false;
    }

    // 필요 없는 콜백 비워두기
    public void DebugReturn(DebugLevel level, string message) { }
    public void OnChatStateChange(ChatState state) { }
    public void OnPrivateMessage(string sender, object message, string channelName) { }
    public void OnStatusUpdate(string user, int status, bool gotMessage, object message) { }
    public void OnUnsubscribed(string[] channels) { }
    public void OnUserSubscribed(string channel, string user) { }
    public void OnUserUnsubscribed(string channel, string user) { }

    #endregion
}
