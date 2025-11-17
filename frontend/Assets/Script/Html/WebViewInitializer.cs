using UnityEngine;
using VoltstroStudios.UnityWebBrowser;             // 기본 네임스페이스
using VoltstroStudios.UnityWebBrowser.Core;        // 코어 기능 네임스페이스

public class WebViewInitializer : MonoBehaviour
{
    [SerializeField]
    private BaseUwbClientManager clientManager;     // UWB 문서에 있는 타입

    private WebBrowserClient webBrowserClient;       // 실제 브라우저 제어용

    private void Start()
    {
        if (clientManager == null)
        {
            Debug.LogError("❌ UWB Client Manager 참조가 비어 있습니다!");
            return;
        }

        // 브라우저 클라이언트 인스턴스 가져오기
        webBrowserClient = clientManager.browserClient;

        // ✅ 클라이언트 초기화 완료 이벤트 등록
        webBrowserClient.OnClientInitialized += OnPageLoaded;
    }

    private void OnPageLoaded()
    {
        // ✅ 페이지 로드 이후 JS 브리지 주입
        webBrowserClient.ExecuteJs(@"
            window.unityInstance = {
                SendMessage: function(obj, func, param) {
                    if (typeof unity !== 'undefined' && unity.SendMessage) {
                        unity.SendMessage(obj, func, param);
                    } else {
                        console.warn('⚠️ Unity 객체를 찾을 수 없습니다.');
                    }
                }
            };
            console.log('✅ window.unityInstance 등록 완료!');
        ");
    }
}
