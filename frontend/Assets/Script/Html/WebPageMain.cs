using UnityEngine;
using VoltstroStudios.UnityWebBrowser;

public class WebPageMain : MonoBehaviour
{
    [SerializeField] private WebBrowserUIBasic browser;
    [SerializeField] private string htmlFileName = "index.html";
    private bool hasLoaded = false;

    private void Update()
    {
        if (browser != null && browser.browserClient != null && browser.browserClient.ReadySignalReceived && !hasLoaded)
        {
            string fixedFileName = htmlFileName.EndsWith(".html") ? htmlFileName : htmlFileName + ".html";
            string path = System.IO.Path.Combine(Application.streamingAssetsPath, "UnityWebBrowser", fixedFileName);
            string url = "file:///" + path.Replace("\\", "/");

            browser.browserClient.LoadUrl(url);
            Debug.Log($"✅ Loaded HTML: {url}");

            hasLoaded = true;

            // ✅ WebBrowserClient에는 OnLoadFinish 이벤트가 있음
            browser.browserClient.OnLoadFinish += OnHtmlPageLoaded;
        }
    }

    private void OnHtmlPageLoaded(string url)
    {
        Debug.Log($"📄 HTML Page Fully Loaded! ({url})");
        SendAuthTokenToBrowser();
    }

    private void SendAuthTokenToBrowser()
    {
        if (AuthManager.Instance == null)
        {
            Debug.LogWarning("AuthManager not found!");
            return;
        }

        string token = AuthManager.Instance.AccessToken;
        if (string.IsNullOrEmpty(token))
        {
            Debug.LogWarning("⚠️ No token found in AuthManager!");
            return;
        }

        // ✅ app.js의 setAuthToken 호출
        string jsCommand = $"if (window.setAuthToken) setAuthToken('{token}');";
        browser.browserClient.ExecuteJs(jsCommand);

        Debug.Log($"📨 Sent AccessToken to JS: {token.Substring(0, 10)}...");
    }
}
