using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.IO;
using System.Collections.Generic;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("✅ 포톤 마스터 서버에 연결되었습니다.");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("⚠️ 방 참가 실패 → 새 방 생성 중...");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 5 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("🎉 룸 접속 완료");

        // 🔹 1️⃣ 로컬 커스터마이징 데이터 불러오기
        string path = Application.persistentDataPath + "/characterData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            CharacterData data = JsonUtility.FromJson<CharacterData>(json);

            // 🔹 2️⃣ Photon Properties에 업로드
            Hashtable customProps = new Hashtable();
            for (int i = 0; i < data.parts.Length; i++)
            {
                customProps[data.parts[i].type.ToString()] = data.parts[i].index;
            }

            PhotonNetwork.LocalPlayer.SetCustomProperties(customProps);
            Debug.Log("📡 커스터마이징 데이터 포톤에 업로드 완료!");
        }
        else
        {
            Debug.LogWarning("⚠️ 커스터마이징 데이터 파일이 없습니다.");
        }

        // 🔹 3️⃣ 플레이어 스폰
        Invoke(nameof(SpawnPlayer), 0.1f);
    }

    private void SpawnPlayer()
    {
        float randomX = Random.Range(-258f, -247f);
        float randomZ = Random.Range(111f, 117f);
        Vector3 spawnPosition = new Vector3(randomX, 1f, randomZ);
        Quaternion spawnRotation = Quaternion.Euler(0f, 180f, 0f);

        GameObject playerObject = PhotonNetwork.Instantiate("MainCharacter", spawnPosition, spawnRotation);

        PhotonView pv = playerObject.GetComponent<PhotonView>();
        if (pv != null && pv.IsMine)
        {
            CameraController cam = Camera.main.GetComponent<CameraController>();
            if (cam != null)
            {
                cam.Initalize(playerObject.transform);
                Debug.Log("🎥 내 카메라가 내 캐릭터에 연결되었습니다!");
            }
            else
                Debug.LogWarning("⚠️ CameraController가 메인 카메라에 없습니다!");
        }
    }

    // 🔹 4️⃣ 다른 유저의 외형 업데이트
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.Count == 0) return;

        Debug.Log($"👀 {targetPlayer.NickName}의 커스터마이징 데이터 업데이트 감지!");

        foreach (var key in changedProps.Keys)
        {
            Debug.Log($"   {key}: {changedProps[key]}");
        }

        // 여기서 targetPlayer에 해당하는 캐릭터 GameObject 찾아서
        // CharacterCustomizer.Apply() 호출하면 실제 외형 반영 가능
    }
}
