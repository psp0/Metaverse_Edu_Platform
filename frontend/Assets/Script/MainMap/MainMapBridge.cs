using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;
using System.Collections;

public class MainMapBridge : MonoBehaviour
{
    [Header("Embedded Browser (강의 페이지)")]
    public Browser browser;

    [Header("HTML 파일 경로 (예: file:///C:/metaedu/index.html 또는 http://localhost/index.html)")]
    public string lecturePageUrl = "http://localhost/index.html";

    private bool tokenInjected = false;

    private void Start()
    {
        if (browser == null)
        {
            Debug.LogError("[MainMapBridge] ❌ Browser 연결 안됨");
            return;
        }

        // HTML 로드
        Debug.Log("[MainMapBridge] 🔗 강의 페이지 로드 시도: " + lecturePageUrl);
        browser.LoadURL(lecturePageUrl, false);
        StartCoroutine(WaitAndInjectToken());
    }

    private IEnumerator WaitAndInjectToken()
    {
        // Embedded Browser는 실제 DOM이 준비되기까지 약간의 지연이 필요함
        yield return new WaitForSeconds(1.5f);

        string token = AuthManager.Instance.AccessToken;
        if (string.IsNullOrEmpty(token))
        {
            Debug.LogWarning("[MainMapBridge] ⚠️ AccessToken 비어 있음. 로그인 후 진입했는지 확인 필요");
            yield break;
        }

        // HTML 쪽 setAccessToken 호출
        string js = $"if(window.setAccessToken) setAccessToken('{token}');";
        browser.EvalJS(js);
        tokenInjected = true;

        Debug.Log("[MainMapBridge] ✅ AccessToken HTML에 전달 완료");
    }
}
