using UnityEngine;
using VoltstroStudios.UnityWebBrowser.Core;

public class WebViewTester : MonoBehaviour
{
    public WebViewObject browser;

    void Start()
    {
        browser.LoadURL("https://www.naver.com");
    }
}
