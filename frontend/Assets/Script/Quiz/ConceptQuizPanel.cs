using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConceptQuizPanel : MonoBehaviour
{
    public QuizManager quizManager;

    [Header("UI")]
    public TextMeshProUGUI questionText;
    public Button[] choiceButtons;
    public Button actionButton;
    public TextMeshProUGUI actionButtonText;
    public TextMeshProUGUI commentaryText;

    private int currentIndex = 0;
    private int selectedAnswerIndex = -1;
    private bool waitingForNext = false;

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
            Debug.Log("개념 퀴즈 완료!");
            return;
        }

        var q = quizManager.quizList[currentIndex];
        questionText.text = q.content;
        commentaryText.gameObject.SetActive(false);

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

        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(OnActionButton);

        actionButtonText.text = "제출하기";
        waitingForNext = false;
        selectedAnswerIndex = -1;
    }

    void OnChoiceSelected(int index)
    {
        foreach (var btn in choiceButtons)
            btn.image.color = Color.white;

        choiceButtons[index].image.color = Color.cyan;
        selectedAnswerIndex = index;
    }

    void OnActionButton()
    {
        if (!waitingForNext)
        {
            if (selectedAnswerIndex == -1) return;

            var q = quizManager.quizList[currentIndex];
            var chosenOption = q.options[selectedAnswerIndex];

            StartCoroutine(quizManager.SendAnswer(q.quizId, chosenOption.optionId, (res) =>
            {
                commentaryText.gameObject.SetActive(true);
                commentaryText.text = res.commentary;

                if (res.correct)
                    choiceButtons[selectedAnswerIndex].image.color = Color.green;
                else
                {
                    choiceButtons[selectedAnswerIndex].image.color = Color.red;
                    // 정답 버튼 찾기 (option에 is_answer 필드 있으면 활용)
                }

                actionButtonText.text = "다음으로";
                waitingForNext = true;
            }));
        }
        else
        {
            currentIndex++;
            LoadQuestion();
        }
    }
}
