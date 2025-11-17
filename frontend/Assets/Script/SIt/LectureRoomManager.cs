using UnityEngine;
using Photon.Pun;
using System.IO;

public class LectureRoomManager : MonoBehaviourPun
{
    public ChairSeat[] chairs;

    public void TryToggleSeat(int seatIndex)
    {
        bool occupied = chairs[seatIndex].IsOccupied;
        string json = null;

        // 착석일 경우 캐릭터 데이터 불러오기
        if (!occupied)
        {
            string path = Application.persistentDataPath + "/characterData.json";
            if (File.Exists(path))
                json = File.ReadAllText(path);
        }

        // RPC로 모든 클라이언트에 상태 전파 + 주인(ActorNumber) 포함
        photonView.RPC("RPC_UpdateSeatState", RpcTarget.AllBuffered,
            seatIndex, !occupied, json, PhotonNetwork.LocalPlayer.ActorNumber);
    }

    [PunRPC]
    private void RPC_UpdateSeatState(int seatIndex, bool occupied, string json, int actorNumber)
    {
        if (seatIndex < 0 || seatIndex >= chairs.Length) return;
        ChairSeat seat = chairs[seatIndex];

        seat.SetState(occupied, json);
        seat.occupiedActor = occupied ? actorNumber : -1;

        // 캐릭터 렌더러 표시/숨김 동기화
        foreach (var playerObj in GameObject.FindGameObjectsWithTag("Player"))
        {
            PhotonView pv = playerObj.GetComponent<PhotonView>();
            if (pv != null && pv.Owner.ActorNumber == actorNumber)
            {
                foreach (var r in playerObj.GetComponentsInChildren<Renderer>())
                    r.enabled = !occupied; // 착석 시 숨기기, 해제 시 보이기
                break;
            }
        }
    }
}
