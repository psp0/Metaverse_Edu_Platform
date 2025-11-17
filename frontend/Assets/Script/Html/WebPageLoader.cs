using UnityEngine;
using VoltstroStudios.UnityWebBrowser;
using System.Collections;

public class WebPageLoader : MonoBehaviour
{
    [SerializeField] private WebBrowserUIBasic browser;
    [SerializeField] private string localHtmlFile = "index.html";
    [SerializeField] private string externalUrl = "https://www.naver.com";

    private bool readyOnce = false;

    void Start()
    {
        StartCoroutine(WaitForBrowserReady());

        var client = browser.browserClient;
        if (client == null)
        {
            Debug.LogWarning("⚠️ [WebPageLoader] BrowserClient가 아직 초기화되지 않았습니다.");
            return;
        }

        // ✅ HTML 페이지 로드가 끝난 후 JS 함수 등록
        client.OnLoadFinish += (string url) =>
        {
            Debug.Log($"🌐 [WebPageLoader] 페이지 로드 완료 ({url}) → exitLecture() 등록");

            // ✅ JS에서 window.browser = browser; 강제 주입
            client.ExecuteJs("window.browser = browser; console.log('✅ window.browser 등록 완료');");

            client.RegisterJsMethod("exitLecture", () =>
            {
                Debug.Log("📩 HTML → Unity: exitLecture() 호출됨");

                if (ChairSeatLocal.CurrentSeated != null)
                {
                    ChairSeatLocal.CurrentSeated.OnExitLecture();
                }
                else
                {
                    Debug.LogWarning("⚠️ 착석 중이 아닙니다 (ChairSeatLocal.CurrentSeated == null)");
                }
            });
        };

    }

    private IEnumerator WaitForBrowserReady()
    {
        while (browser == null || browser.browserClient == null ||
              !browser.browserClient.ReadySignalReceived || !browser.browserClient.IsConnected)
        {
            yield return null;
        }

        readyOnce = true;
        Debug.Log("🟢 [WebPageLoader] UWB Ready & Connected!");
        LoadLocalHtml();
    }

    public void LoadLocalHtml()
    {
        if (!readyOnce)
        {
            Debug.LogWarning("⚠️ [WebPageLoader] 브라우저 준비 전입니다.");
            return;
        }

        StartCoroutine(SafeLoadLocal());
    }

    private IEnumerator SafeLoadLocal()
    {
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "UnityWebBrowser", localHtmlFile);
        string url = "file:///" + path.Replace("\\", "/");
        browser.browserClient.LoadUrl(url);
        Debug.Log($"✅ [WebPageLoader] 내부 HTML 로드 완료 → {url}");

        yield return new WaitForSeconds(0.8f);

        // ✅ 토큰 주입
        if (AuthManager.Instance != null && !string.IsNullOrEmpty(AuthManager.Instance.AccessToken))
        {
            string token = AuthManager.Instance.AccessToken;
            string jsCommand = $"if (window.setAuthToken) setAuthToken('{token}');";
            browser.browserClient.ExecuteJs(jsCommand);
            Debug.Log($"🔐 [WebPageLoader] AccessToken 강제 주입 완료 → {token.Substring(0, 10)}...");
        }
        else
        {
            Debug.LogWarning("⚠️ [WebPageLoader] AuthManager 토큰 없음");
        }
    }

    public void LoadExternalSite()
    {
        if (!readyOnce)
        {
            Debug.LogWarning("⚠️ [WebPageLoader] 브라우저 준비 전입니다.");
            return;
        }

        browser.browserClient.LoadUrl(externalUrl);
        Debug.Log($"🌐 [WebPageLoader] 외부 사이트 로드: {externalUrl}");
    }
}
