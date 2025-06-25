using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ESC 누르면 PauseMenu 토글
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }

    // 게임 종료
    public void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }

    // 로그인 씬으로 이동 (예: LoginPage라는 씬 이름)
    public void GoToLoginPage()
    {
        SceneManager.LoadScene("LoginPage");  // 정확한 씬 이름으로 바꿔줘
    }
}
