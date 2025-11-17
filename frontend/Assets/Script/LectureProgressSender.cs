using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LectureProgressSender : MonoBehaviour
{
    private string apiUrl = "http://localhost:8080/api/progress/lecture";
    private string token = "ey..."; // 로그인 시 받은 JWT 토큰 저장해서 넣기

    public void SendLectureProgress(int subUnitId, int lastTimestamp)
    {
        StartCoroutine(SendProgressCoroutine(subUnitId, lastTimestamp));
    }

    IEnumerator SendProgressCoroutine(int subUnitId, int lastTimestamp)
    {
        LectureProgressData data = new LectureProgressData
        {
            subUnitId = subUnitId,
            lastTimeStamp = lastTimestamp
        };

        string jsonBody = JsonUtility.ToJson(data);

        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + token);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("진행도 전송 성공: " + request.responseCode);
        }
        else
        {
            Debug.LogError("진행도 전송 실패: " + request.error);
        }
    }
}

[System.Serializable]
public class LectureProgressData
{
    public int subUnitId;
    public int lastTimeStamp;
}
