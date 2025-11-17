using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;

public class WebViewTest : MonoBehaviour
{
    public Browser browser;

    void Start()
    {
        // 두 번째 매개변수(force)를 true로 설정 → 강제로 새로 로드
        browser.LoadURL("https://shanghai-labs-posters-balanced.trycloudflare.com/", true);
    }
}
