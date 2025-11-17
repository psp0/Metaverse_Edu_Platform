using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ChairSeatLocal : MonoBehaviour
{
    [Header("좌석 정보")]
    public int seatIndex;
    public LectureRoomManager roomManager;
    public string subjectCode = "KOR"; // 국어/영어/수학

    [Header("강의용 패널 (WebPageLoader 포함)")]
    public GameObject lecturePanel; // 🔹 UWB WebBrowser가 들어있는 Canvas Panel
    private WebPageLoader webLoader;

    private bool isPlayerNear = false;
    private PhotonView pv;

    private Button exitButton;

    // ✅ 현재 착석한 좌석(전역 추적)
    public static ChairSeatLocal CurrentSeated { get; private set; }

    void Start()
    {
        // ✅ LecturePanel 관련 초기화
        if (lecturePanel != null)
        {
            // 기존 WebPageLoader 탐색
            webLoader = lecturePanel.GetComponent<WebPageLoader>();

            // 추가: BrowserFUCKYOU 자동 탐색
            if (webLoader == null)
            {
                Transform browserObj = lecturePanel.transform.Find("BrowserFUCKYOU");
                if (browserObj != null)
                {
                    webLoader = browserObj.GetComponent<WebPageLoader>();
                    Debug.Log("✅ BrowserFUCKYOU에서 WebPageLoader 자동 연결됨!");
                }
                else
                {
                    Debug.LogWarning("⚠️ BrowserFUCKYOU 오브젝트를 찾을 수 없습니다!");
                }
            }

            lecturePanel.SetActive(false); // 처음엔 꺼두기
        }

        // ✅ FuckingButton 자동 연결
        GameObject btnObj = GameObject.Find("FuckingButton");
        if (btnObj != null)
        {
            exitButton = btnObj.GetComponent<Button>();
            exitButton.onClick.RemoveAllListeners(); // 혹시 중복 등록 방지
            exitButton.onClick.AddListener(() =>
            {
                if (CurrentSeated != null)
                    CurrentSeated.StandUp();
            });
            exitButton.gameObject.SetActive(true); // 기본은 비활성화
        }
        else
        {
            Debug.LogWarning("⚠️ FuckingButton을 씬에서 찾을 수 없습니다!");
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (CurrentSeated == null)
                SitDown();
        }

        if (CurrentSeated == this && Input.GetKeyDown(KeyCode.Q))
        {
            StandUp();
        }
    }

    private void SitDown()
    {
        Debug.Log($"🪑 [ChairSeatLocal] 좌석 {seatIndex} 착석 시도");
        roomManager.TryToggleSeat(seatIndex);

        CurrentSeated = this;

        // ✅ 본인 이동 비활성화
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && player.GetComponent<PhotonView>()?.IsMine == true)
        {
            var move = player.GetComponent<PlayerMovement>();
            var cc = player.GetComponent<CharacterController>();
            if (move != null) move.enabled = false;
            if (cc != null) cc.enabled = false;

            var cam = Camera.main.GetComponent<CameraController>();
            if (cam != null) cam.UnlockCursor();
            
        }

        Debug.Log($"[ChairSeatLocal] lecturePanel = {lecturePanel}, webLoader = {(webLoader != null)}");

        // ✅ HTML(강의) 패널 열기
        if (lecturePanel != null)
        {
            lecturePanel.SetActive(true);
            if (webLoader != null)
            {
                switch (subjectCode)
                {
                    case "KOR": webLoader.LoadLocalHtml(); break;
                    case "ENG": webLoader.LoadExternalSite(); break;
                    case "MATH": webLoader.LoadExternalSite(); break;
                    default: webLoader.LoadLocalHtml(); break;
                }
            }

            Debug.Log($"🌐 [ChairSeatLocal] {subjectCode} 강의실 HTML 열림");
        }

        // ✅ 착석 시 버튼 켜기
        if (exitButton != null)
            exitButton.gameObject.SetActive(true);
    }

    public void StandUp()
    {
        if (CurrentSeated != this) return;

        Debug.Log($"🚶 [ChairSeatLocal] 좌석 {seatIndex}에서 일어남");
        roomManager.TryToggleSeat(seatIndex);
        CurrentSeated = null;

        // ✅ 플레이어 이동 복귀
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && player.GetComponent<PhotonView>()?.IsMine == true)
        {
            var move = player.GetComponent<PlayerMovement>();
            var cc = player.GetComponent<CharacterController>();
            if (move != null) move.enabled = true;
            if (cc != null) cc.enabled = true;

            var cam = Camera.main.GetComponent<CameraController>();
            if (cam != null) cam.LockCursor();
        }

        // ✅ HTML(강의) 패널 닫기
        if (lecturePanel != null)
        {
            lecturePanel.SetActive(false);
            Debug.Log("🌐 [ChairSeatLocal] 강의 패널 닫힘");
        }

    }

    public void OnExitLecture()
    {
        StandUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pv = other.GetComponent<PhotonView>();
            if (pv != null && pv.IsMine)
                isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PhotonView pvExit = other.GetComponent<PhotonView>();
            if (pvExit != null && pvExit.IsMine)
                isPlayerNear = false;
        }
    }
}
