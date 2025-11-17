using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class MyclassRoomManager : MonoBehaviour
{
    [Header("UI References")]
    public Transform unitListParent;      // 단원 버튼들이 들어갈 ScrollView Content
    public GameObject unitButtonPrefab;   // 단원 버튼 프리팹

    public Button allButton;
    public Button koreanButton;
    public Button englishButton;
    public Button mathButton;

    private ClassroomData classroomData;  // 서버에서 받아온 전체 데이터
    private string currentFilter = "전체";

    void Start()
    {
        // 버튼 이벤트 연결
        allButton.onClick.AddListener(() => { currentFilter = "전체"; ShowUnits(); });
        koreanButton.onClick.AddListener(() => { currentFilter = "국어"; ShowUnits(); });
        englishButton.onClick.AddListener(() => { currentFilter = "영어"; ShowUnits(); });
        mathButton.onClick.AddListener(() => { currentFilter = "수학"; ShowUnits(); });

        // 🔹 더미 JSON 로드 (Resources/Recent_dummy.json)
        TextAsset dummyJson = Resources.Load<TextAsset>("Dummy/Classroom_dummy");
        classroomData = JsonUtility.FromJson<ClassroomData>(dummyJson.text);
        ShowUnits();

        // 🔹 실제 서버 연동 시
        // StartCoroutine(LoadClassroomData());
    }

    IEnumerator LoadClassroomData()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://mep.run.place:53001/api/myclassroom");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("데이터 불러오기 실패: " + www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
            classroomData = JsonUtility.FromJson<ClassroomData>(json);
            ShowUnits(); // 전체 단원 처음 표시
        }
    }

    void ShowUnits()
    {
        // 기존 버튼 삭제
        foreach (Transform child in unitListParent)
        {
            Destroy(child.gameObject);
        }

        if (classroomData == null || classroomData.subjects == null) return;

        foreach (var subj in classroomData.subjects)
        {
            if (currentFilter == "전체" || subj.name == currentFilter)
            {
                foreach (var unit in subj.units)
                {
                    foreach (var subUnit in unit.subUnits) // 🔹 소단원까지 표시
                    {
                        GameObject btnObj = Instantiate(unitButtonPrefab, unitListParent);
                        var controller = btnObj.GetComponent<SubjectPrefabController>();

                        if (controller != null)
                        {
                            controller.Setup(
                            $"[{subj.name}] {unit.title} - {subUnit.subTitle}",
                            subUnit.lectureProgress,
                            subUnit.subUnitId   // ✅ 여기 소단원 ID 추가
                            );

                        }
                        else
                        {
                            Debug.LogError("SubjectPrefabController가 프리팹에 없음!");
                        }
                    }
                }
            }
        }
    }
}
