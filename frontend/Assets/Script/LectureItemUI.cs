using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LectureItemUI : MonoBehaviour
{
    [Header("UI ��ҵ�")]
    public TMP_Text lectureNameText;
    public Button studyButton1; // ������� ��ư
    public Button studyButton2; // ����Ǯ�� ��ư

    [Header("�Ѱ� ���� UI")]
    public GameObject quizUI;   // ������� UI
    public GameObject studyUI;  // ����Ǯ�� UI

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
        Debug.Log($"{currentLectureName} - ������� Ŭ��");
        quizUI.SetActive(true);
        studyUI.SetActive(false);
    }

    void OnClickProblemSolve()
    {
        Debug.Log($"{currentLectureName} - ����Ǯ�� Ŭ��");
        studyUI.SetActive(true);
        quizUI.SetActive(false);
    }
}
