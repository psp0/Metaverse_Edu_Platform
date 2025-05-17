using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_LoginHandler : MonoBehaviour
{
    public TMP_InputField inputID;
    public TMP_InputField inputPW;
    public GameObject popupPanel;

    public void OnLoginClick()
    {
        string id = inputID.text.Trim();
        string pw = inputPW.text.Trim();

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pw))
        {
            popupPanel.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("PersonalRoom");
        }
    }

    public void ClosePopup()
    {
        if (popupPanel != null)
        popupPanel.SetActive(false);
    }
}
