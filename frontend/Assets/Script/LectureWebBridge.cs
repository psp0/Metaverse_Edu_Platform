using UnityEngine;
using VoltstroStudios.UnityWebBrowser;
using System.Collections;

public class LectureWebBridge : MonoBehaviour
{
    [SerializeField] private WebBrowserUIBasic browser;
    public static LectureWebBridge Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void OpenLecture(string subjectCode)
    {
        Debug.Log($"🎬 [LectureWebBridge] OpenLecture({subjectCode})");
        gameObject.SetActive(true);
        StartCoroutine(WaitUntilBrowserReadyAndSend(subjectCode));
    }

    private IEnumerator WaitUntilBrowserReadyAndSend(string subjectCode)
    {
        // 🔁 ReadySignalReceived true 될 때까지 대기
        while (browser == null ||
               browser.browserClient == null ||
               !browser.browserClient.ReadySignalReceived)
        {
            Debug.Log("⌛ Waiting for browser to be ready...");
            yield return null;
        }

        yield return new WaitForSeconds(0.5f); // 안전 버퍼 약간 주기

        // ✅ JS 실행
        string js = $"if (window.setSubject) setSubject('{subjectCode}');";
        browser.browserClient.ExecuteJs(js);

        Debug.Log($"📨 Sent subject to JS: {subjectCode}");
    }

    public void CloseLecture()
    {
        gameObject.SetActive(false);
        Debug.Log("🧭 Lecture panel closed");
    }
}
