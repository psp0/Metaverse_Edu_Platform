using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubjectPrefabController : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI subjectName;
    public Button studyVideoButton;
    public Button quizButton;
    public Button customQuizButton;
    public TextMeshProUGUI progressText;

    [Header("Panels")]
    public GameObject studyVideoPanel;       // 씬에 있는 StudyVideoPanel
    public ConceptQuizPanel conceptQuizPanel; // 씬에 있는 QuizPanel (ConceptQuizPanel 붙음)
    public CustomQuizPanel customQuizPanel;   // 씬에 있는 CustomQuizPanel (CustomQuizPanel 붙음)

    [Header("Mode")]
    public bool useDummy = true;

    private float lectureProgress;
    private int subUnitId;

    void Awake()
    {
        // Inspector에서 안 채워졌다면 자동으로 씬에서 찾아오기
        if (studyVideoPanel == null)
            studyVideoPanel = GameObject.Find("StudyVideoPanel");

        if (conceptQuizPanel == null)
            conceptQuizPanel = FindObjectOfType<ConceptQuizPanel>(true);
        // true = 비활성화된 오브젝트까지 검색

        if (customQuizPanel == null)
            customQuizPanel = FindObjectOfType<CustomQuizPanel>(true);
    }

    public void Setup(string name, float lecture_progress, int subUnitId)
    {
        subjectName.text = name;
        progressText.text = $"진행도: {lecture_progress}%";
        lectureProgress = lecture_progress;
        this.subUnitId = subUnitId;

        // 버튼 이벤트 초기화
        studyVideoButton.onClick.RemoveAllListeners();
        quizButton.onClick.RemoveAllListeners();
        customQuizButton.onClick.RemoveAllListeners();

        // 강의 버튼 (항상 가능)
        studyVideoButton.onClick.AddListener(OpenStudyVideo);

        // 퀴즈 & 맞춤 문제 버튼
        quizButton.interactable = lecture_progress >= 100f;
        customQuizButton.interactable = lecture_progress >= 100f;

        quizButton.onClick.AddListener(OpenQuiz);
        customQuizButton.onClick.AddListener(OpenCustomQuiz);
    }

    private void OpenStudyVideo()
    {
        Debug.Log($"{subjectName.text} 강의 패널 열기");
        if (studyVideoPanel != null)
            studyVideoPanel.SetActive(true);
    }

    private void OpenQuiz()
    {
        if (lectureProgress >= 100f && conceptQuizPanel != null)
        {
            Debug.Log($"{subjectName.text} 개념퀴즈 패널 열기");
            conceptQuizPanel.gameObject.SetActive(true);
            conceptQuizPanel.Init(subUnitId);
        }
        else
        {
            Debug.Log($"{subjectName.text} 개념퀴즈 잠금 or 패널 없음");
        }
    }

    private void OpenCustomQuiz()
    {
        if (lectureProgress >= 100f && customQuizPanel != null)
        {
            Debug.Log($"{subjectName.text} 맞춤문제 패널 열기");
            customQuizPanel.gameObject.SetActive(true);
            customQuizPanel.Init(subUnitId);
        }
        else
        {
            Debug.Log($"{subjectName.text} 맞춤문제 잠금 or 패널 없음");
        }
    }
}
