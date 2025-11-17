using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomQuizPanel : MonoBehaviour
{
    public QuizManager quizManager;

    [Header("UI")]
    public GameObject quizPanel;
    public GameObject commentPanel;
    public Transform commentContent;
    public GameObject commentTextPrefab;
    public TextMeshProUGUI questionText;
    public Button[] choiceButtons;
    public Button submitButton;

    private int currentIndex = 0;
    private int selectedAnswerIndex = -1;

    public void Init(int subUnitId)
    {
        quizManager.LoadQuiz(subUnitId, () => {
            currentIndex = 0;
            LoadQuestion();
        });
    }

    void LoadQuestion()
    {
        if (currentIndex >= quizManager.quizList.Length)
        {
            ShowCommentPanel();
            return;
        }

        var q = quizManager.quizList[currentIndex];
        questionText.text = q.content;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < q.options.Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.options[i].optionText;
                choiceButtons[i].image.color = Color.white;

                int index = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(index));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }

        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(OnSubmit);

        selectedAnswerIndex = -1;
    }

    void OnChoiceSelected(int index)
    {
        foreach (var btn in choiceButtons)
            btn.image.color = Color.white;

        choiceButtons[index].image.color = Color.cyan;
        selectedAnswerIndex = index;
    }

    void OnSubmit()
    {
        if (selectedAnswerIndex == -1) return;

        var q = quizManager.quizList[currentIndex];
        var chosenOption = q.options[selectedAnswerIndex];

        StartCoroutine(quizManager.SendAnswer(q.quizId, chosenOption.optionId, (res) =>
        {
            // 결과를 commentPanel에 출력할 데이터로 저장
            GameObject txtObj = Instantiate(commentTextPrefab, commentContent);
            var texts = txtObj.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = $"문제 {currentIndex + 1}";
            texts[1].text = $"해설: {res.commentary}";

            currentIndex++;
            LoadQuestion();
        }));
    }

    void ShowCommentPanel()
    {
        quizPanel.SetActive(false);
        commentPanel.SetActive(true);
    }
}
