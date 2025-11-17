using UnityEngine;

public class LectureExitReceiver : MonoBehaviour
{
    public void OnExitLecture()
    {
        Debug.Log("📤 [LectureExitReceiver] 강의 종료 신호 수신됨");

        // 현재 앉은 자리에서 일어나기
        if (ChairSeatLocal.CurrentSeated != null)
        {
            ChairSeatLocal.CurrentSeated.OnExitLecture();
            Debug.Log("🪑 현재 좌석에서 정상적으로 일어남");
        }
        else
        {
            Debug.LogWarning("⚠️ 현재 착석 중인 좌석이 없습니다!");
        }
    }
}
