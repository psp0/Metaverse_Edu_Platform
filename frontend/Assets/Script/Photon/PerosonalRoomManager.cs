using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PersonalRoomManager : MonoBehaviour
{
    public TextMeshProUGUI loginStatusText;

    void Start()
    {
        if (!PhotonNetwork.IsConnected || !PhotonNetwork.IsConnectedAndReady)
        {
            Debug.LogWarning("Photon 서버에 연결되지 않았습니다. 로그인 씬으로 이동합니다.");
            SceneManager.LoadScene("LoginPage");
            return;
        }

        string currentUser = PhotonNetwork.NickName;
        loginStatusText.text = $"로그인됨: {currentUser}";
        Debug.Log($"[PersonalRoom] 로그인한 유저: {currentUser}");
    }

    public void GoToMainMap()
    {
        SceneManager.LoadScene("MainMap");
    }
}
