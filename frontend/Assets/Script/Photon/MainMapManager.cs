using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class MainMapManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        if (!PhotonNetwork.IsConnected || !PhotonNetwork.IsConnectedAndReady)
        {
            Debug.LogWarning("Photon 연결이 필요합니다. 로그인 씬으로 이동합니다.");
            SceneManager.LoadScene("LoginPage");
            return;
        }

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("방이 없어 새로 생성합니다.");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 10 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("멀티 룸에 입장하였습니다.");
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f));
        GameObject player = PhotonNetwork.Instantiate("PlayerPrefab", spawnPos, Quaternion.identity);

        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        //player.GetComponent<PlayerController>().Initalize(actorNumber);
        //Camera.main.GetComponent<CameraController>().Initalize(player.transform);
    }
}
