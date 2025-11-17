using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;

public class BrowserLoader : MonoBehaviour
{
    public Browser browser;
    public string htmlFileName = "auth.html"; // 인스펙터에서 바꿀 수 있음

    void Start()
    {
        string path = Application.streamingAssetsPath.Replace("\\", "/");
        string url = "file:///" + path + "/" + htmlFileName;
        browser.Url = url;
        Debug.Log("Loaded HTML: " + url);
    }

    public void LoadPage(string fileName)
    {
        string path = Application.streamingAssetsPath.Replace("\\", "/");
        string url = "file:///" + path + "/" + fileName;
        browser.Url = url;
        Debug.Log("Changed Page: " + url);
    }
}
