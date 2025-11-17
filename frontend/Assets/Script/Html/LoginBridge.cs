using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using System.Collections;
using SimpleJSON; // ✅ SimpleJSON 명시적 사용

public class LoginBridge : MonoBehaviour
{
    public Browser browser;
    private bool isSceneLoading = false;
    private string baseUrl = "http://localhost"; // ✅ Swagger와 동일
                                                 // 🔹 실제 백엔드 포트 명시
                                                 // ⚙️ 서버 주소

    void Start()
    {
        if (browser == null)
        {
            Debug.LogError("[LoginBridge] Browser 연결 안됨");
            return;
        }

        // ✅ HTML → Unity: 로그인 데이터 전달받기
        // ✅ HTML → Unity: 로그인 데이터 전달받기
        browser.RegisterFunction("sendLoginData", args =>
        {
            // 🔹 HTML에서 보낸 JSON 문자열 받기
            string json = args[0].Value.ToString(); // ✅ 명시적 변환 추가
            Debug.Log($"[LoginBridge] 받은 로그인 JSON: {json}");

            var node = SimpleJSON.JSON.Parse(json);
            string userId = node["userId"];
            string password = node["password"];

            Debug.Log($"[LoginBridge] 로그인 시도 → userId={userId}");
            StartCoroutine(LoginRequest(userId, password));
        });

    }

    private IEnumerator LoginRequest(string userId, string password)
    {
        string url = $"{baseUrl}/api/users/login";

        // ✅ JSON 직렬화
        LoginPayload requestData = new LoginPayload(userId, password);
        string jsonBody = JsonUtility.ToJson(requestData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        using (UnityWebRequest req = new UnityWebRequest(url, "POST"))
        {
            req.uploadHandler = new UploadHandlerRaw(bodyRaw);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            Debug.Log("[LoginBridge] 로그인 요청 중...");
            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"[LoginBridge] 로그인 실패: {req.error}");
                yield break;
            }

            Debug.Log($"[LoginBridge] 서버 응답: {req.downloadHandler.text}");
            var data = SimpleJSON.JSON.Parse(req.downloadHandler.text);

            // ✅ 응답 파싱
            string accessToken = data["accessToken"];
            string refreshToken = data["refreshToken"];
            string expiresAt = data["expiresAt"];

            if (string.IsNullOrEmpty(accessToken))
            {
                Debug.LogError("[LoginBridge] accessToken 비어 있음 → 로그인 실패");
                yield break;
            }

            // ✅ 토큰 저장 및 씬 이동
            AuthManager.Instance.SaveTokens(accessToken, refreshToken, expiresAt);
            Debug.Log("[LoginBridge] 로그인 성공 → PersonalRoom 이동");

            if (!isSceneLoading)
            {
                isSceneLoading = true;
                SceneManager.LoadScene("PersonalRoom");
            }
        }
    }

    // ✅ 내부 요청 데이터 구조
    [Serializable]
    private class LoginPayload
    {
        public string userId;
        public string password;

        public LoginPayload(string id, string pw)
        {
            userId = id;
            password = pw;
        }
    }
}
