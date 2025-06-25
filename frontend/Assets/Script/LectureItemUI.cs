using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LectureItemUI : MonoBehaviour
{
    [Header("UI 요소들")]
    public TMP_Text lectureNameText;
    public Button studyButton1; // 개념공부 버튼
    public Button studyButton2; // 문제풀이 버튼

    [Header("켜고 싶은 UI")]
    public GameObject quizUI;   // 개념공부 UI
    public GameObject studyUI;  // 문제풀이 UI

    private string currentLectureName;

    public void Initialize(string lectureName)
    {
        currentLectureName = lectureName;
        lectureNameText.text = lectureName;

        studyButton1.onClick.AddListener(OnClickConceptStudy);
        studyButton2.onClick.AddListener(OnClickProblemSolve);
    }

    void OnClickConceptStudy()
    {
        Debug.Log($"{currentLectureName} - 개념공부 클릭");
        quizUI.SetActive(true);
        studyUI.SetActive(false);
    }

    void OnClickProblemSolve()
    {
        Debug.Log($"{currentLectureName} - 문제풀이 클릭");
        studyUI.SetActive(true);
        quizUI.SetActive(false);
    }
}
