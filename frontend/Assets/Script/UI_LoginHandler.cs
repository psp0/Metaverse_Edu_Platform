using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_LoginHandler : MonoBehaviour
{
    public TMP_InputField inputID;
    public TMP_InputField inputPW;
    public GameObject popupPanel;
    public LoginHandler loginHandler; // LoginHandler 참조 연결 필요!

    public void OnLoginClick()
    {
        string id = inputID.text.Trim();
        string pw = inputPW.text.Trim();

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pw))
        {
            popupPanel.SetActive(true);
            return;
        }

        StartCoroutine(LoginAndConnect(id, pw));
    }

    private System.Collections.IEnumerator LoginAndConnect(string id, string pw)
    {
        yield return StartCoroutine(loginHandler.LoginCoroutine(id, pw));

        // 로그인 성공 여부를 loginHandler 내부에서 토큰 저장 여부로 판단
        if (!string.IsNullOrEmpty(AuthManager.Instance.AccessToken))
        {
            Debug.Log("✅ 로그인 성공, Photon 연결 시도");

            // 1. Photon 연결
            if (!Photon.Pun.PhotonNetwork.IsConnected)
                Photon.Pun.PhotonNetwork.ConnectUsingSettings();

            // 2. 룸 접속 대기
            while (!Photon.Pun.PhotonNetwork.InRoom)
            {
                yield return null;
            }

            Debug.Log("✅ 룸 접속 완료 → 씬 전환");
            SceneManager.LoadScene("PersonalRoom");
        }
        else
        {
            Debug.LogError("❌ 로그인 실패");
            popupPanel.SetActive(true);
        }
    }

    public void ClosePopup()
    {
        if (popupPanel != null)
            popupPanel.SetActive(false);
    }
}
