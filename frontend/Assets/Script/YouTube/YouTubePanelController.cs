using UnityEngine;

public class YouTubePanelController : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public RectTransform webViewRect;   // StudyVideoPanel/WebViewRect
    public GameObject rootPanel;        // StudyVideoPanel

    private WebViewObject web;

    public void Open(string youtubeUrl)
    {
        rootPanel.SetActive(true);
        EnsureWebView();
        UpdateMargins();
        web.LoadURL(ToEmbedUrl(youtubeUrl));
        web.SetVisibility(true);
    }

    public void Close()
    {
        if (web != null) web.SetVisibility(false);
        rootPanel.SetActive(false);
    }

    void EnsureWebView()
    {
        if (web != null) return;
        web = new GameObject("WebView").AddComponent<WebViewObject>();
        web.Init(
            transparent: false,
            enableWKWebView: true,
            err: (m) => Debug.LogWarning("[WebView] Error: " + m),
            httpErr: (m) => Debug.LogWarning("[WebView] HttpError: " + m)
        );
        web.SetVisibility(false);
    }

    void UpdateMargins()
    {
        if (webViewRect == null || web == null) return;

        Vector3[] corners = new Vector3[4];
        webViewRect.GetWorldCorners(corners);
        Vector2 min = RectTransformUtility.WorldToScreenPoint(null, corners[0]); // ÁÂÇÏ
        Vector2 max = RectTransformUtility.WorldToScreenPoint(null, corners[2]); // ¿ì»ó

        int left = Mathf.RoundToInt(min.x);
        int right = Mathf.RoundToInt(Screen.width - max.x);
        int top = Mathf.RoundToInt(Screen.height - max.y);
        int bottom = Mathf.RoundToInt(min.y);

        web.SetMargins(left, top, right, bottom);
    }

    string ToEmbedUrl(string url)
    {
        if (string.IsNullOrEmpty(url)) return url;
        string id = null;

        var w = url.IndexOf("watch?v=");
        if (w >= 0) { id = url[(w + 8)..]; int cut = id.IndexOfAny(new[] { '&', '?', '/' }); if (cut >= 0) id = id[..cut]; }
        if (string.IsNullOrEmpty(id))
        {
            var b = url.IndexOf("youtu.be/");
            if (b >= 0) { id = url[(b + 9)..]; int cut = id.IndexOfAny(new[] { '&', '?', '/' }); if (cut >= 0) id = id[..cut]; }
        }
        if (string.IsNullOrEmpty(id))
        {
            var s = url.IndexOf("/shorts/");
            if (s >= 0) { id = url[(s + 8)..]; int cut = id.IndexOfAny(new[] { '&', '?', '/' }); if (cut >= 0) id = id[..cut]; }
        }

        return string.IsNullOrEmpty(id)
            ? url
            : $"https://www.youtube.com/embed/{id}?playsinline=1&rel=0&modestbranding=1&autoplay=1";
    }

    void LateUpdate()
    {
        if (rootPanel != null && rootPanel.activeSelf && web != null)
            UpdateMargins();

#if UNITY_ANDROID
        if (rootPanel != null && rootPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            Close();
#endif
    }

    void OnDestroy()
    {
        if (web != null) Destroy(web.gameObject);
    }
}
