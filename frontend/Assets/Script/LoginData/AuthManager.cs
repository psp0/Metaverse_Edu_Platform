using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance;

    public string AccessToken => PlayerPrefs.GetString("accessToken");
    public string RefreshToken => PlayerPrefs.GetString("refreshToken");
    public string ExpiresAt => PlayerPrefs.GetString("expiresAt");

    //[SerializeField] private string baseUrl = "http://mep.run.place:53001"; // 백엔드 주소
    [SerializeField] private string baseUrl = "http://localhost"; // 백엔드 주소

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 토큰 저장
    /// </summary>
    public void SaveTokens(string accessToken, string refreshToken, string expiresAt)
    {
        PlayerPrefs.SetString("accessToken", accessToken);
        PlayerPrefs.SetString("refreshToken", refreshToken);
        PlayerPrefs.SetString("expiresAt", expiresAt);
        PlayerPrefs.Save();
        Debug.Log("AuthManager: Tokens saved");
    }

    /// <summary>
    /// 로그아웃 처리
    /// </summary>
    public void Logout()
    {
        PlayerPrefs.DeleteKey("accessToken");
        PlayerPrefs.DeleteKey("refreshToken");
        PlayerPrefs.DeleteKey("expiresAt");
        Debug.Log("AuthManager: Tokens cleared");
    }

    /// <summary>
    /// Authorization 헤더 반환
    /// </summary>
    public string GetAuthHeader()
    {
        return "Bearer " + AccessToken;
    }

    /// <summary>
    /// 토큰 만료 여부 확인
    /// </summary>
    public bool IsTokenExpired()
    {
        if (string.IsNullOrEmpty(ExpiresAt)) return true;
        if (DateTime.TryParse(ExpiresAt, out var expTime))
        {
            return DateTime.UtcNow > expTime;
        }
        return true;
    }

    /// <summary>
    /// 만료되었을 경우 Refresh Token으로 Access Token 재발급
    /// </summary>
    public IEnumerator RefreshAccessToken(Action<bool> callback)
    {
        if (string.IsNullOrEmpty(RefreshToken))
        {
            Debug.LogWarning("No refresh token available.");
            callback?.Invoke(false);
            yield break;
        }

        string url = baseUrl + "/api/users/refresh"; // ⚠️ 백엔드 refresh API 엔드포인트 확인 필요
        WWWForm form = new WWWForm();
        form.AddField("refresh_token", RefreshToken);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Refresh token request failed: " + www.error);
                callback?.Invoke(false);
            }
            else
            {
                try
                {
                    var data = JsonUtility.FromJson<TokenResponse>(www.downloadHandler.text);
                    SaveTokens(data.access_token, data.refresh_token, data.expires_at);
                    Debug.Log("Access token refreshed.");
                    callback?.Invoke(true);
                }
                catch (Exception e)
                {
                    Debug.LogError("Failed to parse refresh response: " + e);
                    callback?.Invoke(false);
                }
            }
        }
    }

    [Serializable]
    private class TokenResponse
    {
        public string access_token;
        public string refresh_token;
        public string expires_at;
    }
}
