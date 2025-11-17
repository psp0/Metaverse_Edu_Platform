using UnityEngine;

public class LectureProgressManager : MonoBehaviour
{
    public YouTubePanelControllerWebGL youtubePlayer;
    public LectureProgressSender progressSender;

    public int subUnitId = 20101; // 현재 보고 있는 소단원 ID (동적으로 할당해야 함)
    private float checkInterval = 5f; // 5초마다 기록
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= checkInterval)
        {
            timer = 0f;
            SaveProgress();
        }
    }

    public void SaveProgress()
    {
        float currentTime = youtubePlayer.GetCurrentTime();
        int lastTimeStamp = Mathf.FloorToInt(currentTime);

        Debug.Log($"[학습기록] SubUnitId={subUnitId}, Time={lastTimeStamp}");
        progressSender.SendLectureProgress(subUnitId, lastTimeStamp);
    }

    public void OnExitLecture()
    {
        SaveProgress(); // 패널 닫을 때 마지막 기록
    }
}
