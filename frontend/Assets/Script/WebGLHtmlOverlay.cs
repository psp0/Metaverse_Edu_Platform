using System.Runtime.InteropServices;
using UnityEngine;

public class WebGLHtmlOverlay : MonoBehaviour
{
    [Header("기본 URL (임베드 허용 주소를 쓰세요)")]
    public string url = "StreamingAssets/Hello.html";



    [Header("정규화 사각형 (0~1, 캔버스 기준)")]
    public Rect rect01 = new Rect(0.1f, 0.1f, 0.8f, 0.8f); // 좌상단(0.1,0.1), 폭/높이(0.8,0.8)

    [Header("ID (여러 개 쓸 때 구분용)")]
    public string iframeId = "unity_iframe_1";

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")] private static extern void HTML_CreateIframe(string id, string url);
    [DllImport("__Internal")] private static extern void HTML_SetIframeRectNormalized(string id, float nx, float ny, float nw, float nh);
    [DllImport("__Internal")] private static extern void HTML_SetIframeVisible(string id, int visible);
    [DllImport("__Internal")] private static extern void HTML_SetIframeURL(string id, string url);
    [DllImport("__Internal")] private static extern void HTML_SetIframeInteractable(string id, int interactable);
    [DllImport("__Internal")] private static extern void HTML_DestroyIframe(string id);
#endif

    public void Show()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        HTML_CreateIframe(iframeId, url);
        HTML_SetIframeRectNormalized(iframeId, rect01.x, rect01.y, rect01.width, rect01.height);
        HTML_SetIframeVisible(iframeId, 1);
        HTML_SetIframeInteractable(iframeId, 1);
#else
        Debug.LogWarning("WebGL에서만 iframe 오버레이가 생성됩니다. (에디터/스탠드얼론은 Application.OpenURL 권장)");
#endif
    }

    public void Hide()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        HTML_SetIframeVisible(iframeId, 0);
#endif
    }

    public void CloseAndDestroy()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        HTML_DestroyIframe(iframeId);
#endif
    }

    public void SetUrl(string newUrl)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        url = newUrl;
        HTML_SetIframeURL(iframeId, url);
#endif
    }

    public void SetRect01(Rect r)
    {
        rect01 = r;
#if UNITY_WEBGL && !UNITY_EDITOR
        HTML_SetIframeRectNormalized(iframeId, r.x, r.y, r.width, r.height);
#endif
    }

    // 데모: 시작 시 자동 표시
    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    url = System.IO.Path.Combine(Application.streamingAssetsPath, "Hello.html");
#endif
        Show();
    }


    // ESC로 닫기 (데모)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Hide();
    }
}
