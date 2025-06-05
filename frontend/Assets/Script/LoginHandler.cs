using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Text;
using System;
using Photon.Pun; // 포톤 네임스페이스 추가

public class LoginHandler : MonoBehaviourPunCallbacks
{
    [Header("Input Fields")]
    public TMP_InputField userIdInput;
    public TMP_InputField passwordInput;

    [Header("UI")]
    public Button loginButton;
    public TextMeshProUGUI messageText;

    private string baseUrl = $"{Environment.GetEnvironmentVariable("BACK_SERVER_URL")}:{Environment.GetEnvironmentVariable("BACK_SERVER_PORT")}";

    void Start()
    {
        loginButton.onClick.AddListener(OnLoginClicked);
    }

    void OnLoginClicked()
    {
        string userId = userIdInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
        {
            messageText.text = "아이디와 비밀번호를 입력해주세요.";
            return;
        }

        StartCoroutine(LoginCoroutine(userId, password));
    }

    public IEnumerator LoginCoroutine(string userId, string password)
    {
        string url = baseUrl + "/api/users/login";

        var loginData = new LoginRequest { userId = userId, password = password };
        string jsonData = JsonUtility.ToJson(loginData);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            TokenResponse response = JsonUtility.FromJson<TokenResponse>(request.downloadHandler.text);

            AuthManager.Instance.SaveTokens(response.accessToken, response.refreshToken);

            // Photon 연결 시도
            PhotonNetwork.NickName = userId;
            PhotonNetwork.ConnectUsingSettings();  // 자동으로 서버에 연결
        }
        else
        {
            messageText.text = "로그인 실패: " + request.responseCode;
        }
    }

    // Photon 서버에 연결되었을 때 자동 호출
    public override void OnConnectedToMaster()
    {
        Debug.Log("[Photon] 마스터 서버 연결 성공");
        SceneManager.LoadScene("PersonalRoom");
    }

    // Photon 연결 실패 시
    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        Debug.LogError($"[Photon] 연결 실패: {cause}");
        messageText.text = "포톤 서버 연결 실패";
    }

    [Serializable]
    private class LoginRequest
    {
        public string userId;
        public string password;
    }

    [Serializable]
    private class TokenResponse
    {
        public string accessToken;
        public string refreshToken;
    }
}
