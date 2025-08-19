using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;


public class LogoutHandler : MonoBehaviour
{   

    public Button logoutButton;

    void Start()
    {
        logoutButton.onClick.AddListener(Logout);
        Debug.Log($"[LogoutHandler] Backend URL: {BackendConfig.GetApiUrl()}");
    }

    public void Logout()
    {
        StartCoroutine(LogoutCoroutine());
    }

    IEnumerator LogoutCoroutine()
    {
        string url = BackendConfig.GetApiUrl() + "/api/users/logout";
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.downloadHandler = new DownloadHandlerBuffer();
        Debug.Log("AccessToken = " + AuthManager.Instance.AccessToken);
        request.SetRequestHeader("Authorization", "Bearer " + AuthManager.Instance.AccessToken);

        // 🔐 인증서 우회 적용
        //request.certificateHandler = new BypassCertificateHandler();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("서버 로그아웃 성공");
        }
        else
        {
            Debug.LogWarning("서버 로그아웃 실패 또는 무시됨: " + request.error);
        }

        // ✅ 로컬 토큰 제거
        AuthManager.Instance.Logout();

        // ✅ LoginPage 씬으로 이동
        SceneManager.LoadScene("LoginPage");
    }
}
