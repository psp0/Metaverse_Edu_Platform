using UnityEngine;
using TMPro;
using Photon.Pun;

public class DoorTrigger : MonoBehaviour
{
    [Header("문 정보")]
    public string roomName = "국어 강의실";   // UI에 표시할 이름
    public Transform doorDestination;          // 이동할 목표 위치 (예: KoreanSpawn 등)

    [Header("UI 연결")]
    public GameObject interactionUI;           // 전체 UI 오브젝트 (예: [E] 국어 강의실로 이동)
    public TMP_Text interactionText;           // UI 안의 텍스트 (TMP)

    private bool isPlayerNear = false;         // 플레이어가 문 근처에 있는지 여부
    private Transform player;                  // 내 캐릭터 Transform (Photon IsMine)

    private void Update()
    {
        // ✅ 내 캐릭터가 아직 등록되지 않았다면, 씬 내 모든 Player 중 내 것 찾기
        if (player == null)
        {
            foreach (var obj in GameObject.FindGameObjectsWithTag("Player"))
            {
                PhotonView pv = obj.GetComponent<PhotonView>();
                if (pv != null && pv.IsMine)
                {
                    player = obj.transform;
                    break;
                }
            }
        }

        // ✅ 상호작용 키 입력 처리 (내 캐릭터가 근처에 있을 때만)
        if (isPlayerNear && player != null && Input.GetKeyDown(KeyCode.E))
        {
            MovePlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ✅ Player 태그를 가진 객체 + 내 로컬 플레이어일 때만 UI 표시
        if (other.CompareTag("Player"))
        {
            PhotonView pv = other.GetComponent<PhotonView>();
            if (pv != null && pv.IsMine)
            {
                isPlayerNear = true;
                if (interactionText != null)
                    interactionText.text = $"{roomName}로 이동";
                interactionUI?.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ✅ 트리거 벗어났을 때 UI 닫기
        if (other.CompareTag("Player"))
        {
            PhotonView pv = other.GetComponent<PhotonView>();
            if (pv != null && pv.IsMine)
            {
                isPlayerNear = false;
                interactionUI?.SetActive(false);
            }
        }
    }

    private void MovePlayer()
    {
        if (doorDestination == null || player == null) return;

        // ✅ CharacterController가 있을 경우 잠시 끄고 위치 이동
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null) controller.enabled = false;

        player.position = doorDestination.position;
        player.rotation = doorDestination.rotation;

        if (controller != null) controller.enabled = true;

        // ✅ UI 닫기 및 상태 초기화
        interactionUI?.SetActive(false);
        isPlayerNear = false;

        Debug.Log($"🚪 {roomName}로 이동 완료! ({doorDestination.position})");
    }
}
