using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System;

public class LoginHandler : MonoBehaviour
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

        Debug.Log($"[Login] 로그인 시도 → userId: {userId}");
        StartCoroutine(LoginCoroutine(userId, password));
    }

    IEnumerator LoginCoroutine(string userId, string password)
    {
        Debug.Log("✅ [Login] LoginCoroutine 진입함");

        string url = baseUrl + "/api/users/login";
        Debug.Log("[Login] 요청 URL: " + url);

        var loginData = new LoginRequest
        {
            userId = userId,
            password = password
        };

        string jsonData = JsonUtility.ToJson(loginData);
        Debug.Log("[Login] 전송 JSON: " + jsonData);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // 🔐 인증서 우회 적용
        //request.certificateHandler = new BypassCertificateHandler();

        Debug.Log("📤 [Login] 요청 전송 시작");
        yield return request.SendWebRequest();
        Debug.Log("📥 [Login] 요청 응답 완료");

        // ✅ [여기 아래에 디버그 코드 붙이기]
        Debug.Log($"[Login] request.result = {request.result}");
        Debug.Log($"[Login] responseCode = {request.responseCode}");
        Debug.Log($"[Login] error = {request.error}");
        Debug.Log($"[Login] responseText = {request.downloadHandler.text}");

        Debug.Log($"[Login] 응답 코드: {request.responseCode}, Result: {request.result}");
        Debug.Log("[Login] 응답 내용: " + request.downloadHandler.text);

        if (request.result == UnityWebRequest.Result.Success)
        {
            try
            {
                Debug.Log("[Login] 응답 수신 성공, 본문: " + request.downloadHandler.text);

                TokenResponse response = JsonUtility.FromJson<TokenResponse>(request.downloadHandler.text);

                Debug.Log("[Login] accessToken: " + response.accessToken);
                Debug.Log("[Login] refreshToken: " + response.refreshToken);

                AuthManager.Instance.SaveTokens(response.accessToken, response.refreshToken);
                Debug.Log("[Login] 토큰 저장 완료");

                messageText.text = "로그인 성공!";
            }
            catch (Exception e)
            {
                Debug.LogError("❌ 응답 파싱 오류: " + e.Message);
                messageText.text = "서버 응답 처리 오류.";
            }
        }
        else
        {
            Debug.LogError("❌ 로그인 실패: " + request.error);
            Debug.Log("[Login] 응답 내용: " + request.downloadHandler.text);

            if (request.responseCode == 401)
            {
                messageText.text = "아이디 또는 비밀번호가 올바르지 않습니다.";
            }
            else if (request.responseCode == 500)
            {
                messageText.text = "서버 내부 오류가 발생했습니다.";
            }
            else
            {
                messageText.text = $"알 수 없는 오류 발생 (코드: {request.responseCode})";
            }
        }
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
