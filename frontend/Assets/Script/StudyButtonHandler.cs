using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StudyButtonHandler : MonoBehaviour
{
    public Button studyButton; // UI¿¡ ¿¬°á

    void Start()
    {
        if (studyButton != null)
        {
            studyButton.onClick.AddListener(OnStudyButtonClick);
        }
        else
        {
            Debug.LogWarning("Study Button is not assigned.");
        }
    }

    void OnStudyButtonClick()
    {
        SceneManager.LoadScene("StudyRoom"); 
    }
}
