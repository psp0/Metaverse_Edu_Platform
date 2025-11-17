using UnityEngine;
using VoltstroStudios.UnityWebBrowser;

/// <summary>
/// UnityWebBrowser를 전역에서 접근할 수 있게 해주는 싱글톤 클래스.
/// 모든 씬에서 단 하나의 WebBrowserUIBasic만 유지.
/// </summary>
public class WebBrowserSingleton : MonoBehaviour
{
    public static WebBrowserSingleton Instance;

    public WebBrowserUIBasic BrowserUI => GetComponent<WebBrowserUIBasic>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ✅ 씬 전환해도 유지
        }
        else
        {
            Destroy(gameObject); // ✅ 중복 방지
        }
    }
}
