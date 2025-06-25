using UnityEngine;
using UnityEngine.UI;

public class LectureListManager : MonoBehaviour
{
    [Header("프리팹과 부모")]
    public GameObject lectureItemPrefab;
    public Transform contentParent;

    [Header("각 버튼이 열 UI")]
    public GameObject quizUI;
    public GameObject studyUI;

    [Header("UI 내 닫기 버튼")]
    public Button quizCloseButton;
    public Button studyCloseButton;

    [Header("단원 이름 리스트")]
    public string[] lectureNames;

    void Start()
    {
        // 프리팹 생성
        foreach (string lectureName in lectureNames)
        {
            GameObject itemObj = Instantiate(lectureItemPrefab, contentParent);
            LectureItemUI itemUI = itemObj.GetComponent<LectureItemUI>();

            itemUI.quizUI = quizUI;
            itemUI.studyUI = studyUI;

            itemUI.Initialize(lectureName);
        }

        // ❌ 닫기 버튼 기능 연결
        quizCloseButton.onClick.AddListener(() => quizUI.SetActive(false));
        studyCloseButton.onClick.AddListener(() => studyUI.SetActive(false));
    }
}
