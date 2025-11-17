using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;

public class PingHandler : MonoBehaviour
{
    public Browser browser; // Inspector에 Browser 컴포넌트 드래그

    void Start()
    {
        if (browser == null)
        {
            Debug.LogError("[PingHandler] browser 연결 안 됨!");
            return;
        }

        // HTML → Unity 함수 등록
        browser.RegisterFunction("ping", args =>
        {
            string msg = args[0].ToString();
            Debug.Log("[PingHandler] ping 호출됨, args[0] = " + msg);
        });

        Debug.Log("[PingHandler] RegisterFunction ping 완료");
    }
}
