using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ESC ������ PauseMenu ���
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }

    // ���� ����
    public void QuitGame()
    {
        Debug.Log("���� ����");
        Application.Quit();
    }

    // �α��� ������ �̵� (��: LoginPage��� �� �̸�)
    public void GoToLoginPage()
    {
        SceneManager.LoadScene("LoginPage");  // ��Ȯ�� �� �̸����� �ٲ���
    }
}
