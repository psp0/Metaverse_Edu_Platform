#if UNITY_WEBGL && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif
using UnityEngine;

public class YouTubePanelControllerWebGL : MonoBehaviour
{
    public RectTransform webViewRect;
    public GameObject rootPanel;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")] private static extern void YT_Show(string url, int x, int y, int w, int h);
    [DllImport("__Internal")] private static extern void YT_Move(int x, int y, int w, int h);
    [DllImport("__Internal")] private static extern void YT_Hide();
    [DllImport("__Internal")] private static extern void YT_Dispose();

    // 🔹 새로 추가: 현재 재생 시간 가져오기
    [DllImport("__Internal")] private static extern float YT_GetCurrentTime();
#endif

    public void Open(string watchUrl)
    {
        rootPanel.SetActive(true);
        var embed = ToEmbedUrl(watchUrl);
        GetScreenRect(webViewRect, out int x, out int y, out int w, out int h);
#if UNITY_WEBGL && !UNITY_EDITOR
        YT_Show(embed, x, y, w, h);
#else
        Application.OpenURL(watchUrl);
#endif
    }

    public void Close()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        YT_Hide();
#endif
        rootPanel.SetActive(false);
    }

    void LateUpdate()
    {
        if (!rootPanel.activeSelf) return;
        GetScreenRect(webViewRect, out int x, out int y, out int w, out int h);
#if UNITY_WEBGL && !UNITY_EDITOR
        YT_Move(x, y, w, h);
#endif
    }

    void OnDestroy()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        YT_Dispose();
#endif
    }

    void GetScreenRect(RectTransform rt, out int x, out int y, out int w, out int h)
    {
        Vector3[] c = new Vector3[4];
        rt.GetWorldCorners(c);
        var min = RectTransformUtility.WorldToScreenPoint(null, c[0]);
        var max = RectTransformUtility.WorldToScreenPoint(null, c[2]);
        x = Mathf.RoundToInt(min.x);
        y = Mathf.RoundToInt(Screen.height - max.y);
        w = Mathf.RoundToInt(max.x - min.x);
        h = Mathf.RoundToInt(max.y - min.y);
    }

    string ToEmbedUrl(string url)
    {
        if (string.IsNullOrEmpty(url)) return url;
        string id = null;

        var w = url.IndexOf("watch?v=");
        if (w >= 0)
        {
            id = url[(w + 8)..];
            int cut = id.IndexOfAny(new[] { '&', '?', '/' });
            if (cut >= 0) id = id[..cut];
        }
        if (string.IsNullOrEmpty(id))
        {
            var b = url.IndexOf("youtu.be/");
            if (b >= 0)
            {
                id = url[(b + 9)..];
                int cut = id.IndexOfAny(new[] { '&', '?', '/' });
                if (cut >= 0) id = id[..cut];
            }
        }
        if (string.IsNullOrEmpty(id))
        {
            var s = url.IndexOf("/shorts/");
            if (s >= 0)
            {
                id = url[(s + 8)..];
                int cut = id.IndexOfAny(new[] { '?', '&', '/' });
                if (cut >= 0) id = id[..cut];
            }
        }

        return string.IsNullOrEmpty(id)
            ? url
            : $"https://www.youtube.com/embed/{id}?playsinline=1&rel=0&modestbranding=1&autoplay=1&enablejsapi=1";
    }

    // 🔹 현재 재생 시간 가져오기
    public float GetCurrentTime()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return YT_GetCurrentTime();
#else
        return 0f; // 에디터에서는 0 리턴
#endif
    }
}
