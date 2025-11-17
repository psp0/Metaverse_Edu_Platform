using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubjectChanger : MonoBehaviour
{
    public TextMeshProUGUI subjectText; // SubjectName 텍스트 UI
    public Button buttonAllSubject;
    public Button buttonKorean;
    public Button buttonEnglish;
    public Button buttonMath;

    void Start()
    {
        ChangeSubject("전체");
        // 버튼 클릭 시 실행될 이벤트 등록
        buttonAllSubject.onClick.AddListener(() => ChangeSubject("전체"));
        buttonKorean.onClick.AddListener(() => ChangeSubject("국어"));
        buttonEnglish.onClick.AddListener(() => ChangeSubject("영어"));
        buttonMath.onClick.AddListener(() => ChangeSubject("수학"));
    }

    void ChangeSubject(string subject)
    {
        subjectText.text = subject;
    }
}
