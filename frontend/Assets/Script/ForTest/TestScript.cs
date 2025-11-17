using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    public Button testButton;
    public YouTubePanelControllerWebGL panelCtrl; // ← 여기!

    public string testUrl = "https://www.youtube.com/watch?v=a6sHa24_BbU";

    void Start()
    {
        if (panelCtrl == null)  // 비워놔도 자동으로 찾아오게
            panelCtrl = FindObjectOfType<YouTubePanelControllerWebGL>(true);

        testButton.onClick.RemoveAllListeners();
        testButton.onClick.AddListener(() => panelCtrl.Open(testUrl));
    }
}
