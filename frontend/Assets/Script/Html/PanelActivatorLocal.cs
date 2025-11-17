using UnityEngine;
using Photon.Pun;

public class PanelActivatorLocal : MonoBehaviour
{
    [Header("패널 설정")]
    public GameObject targetPanel;       // 공부 패널
    public string panelName = "Quiz";
    public GameObject Fuckyou;           // Q&A 안내 패널

    private bool isPlayerNear = false;
    private PhotonView pv;

    public static PanelActivatorLocal CurrentActive { get; private set; }

    void Start()
    {
        if (targetPanel != null)
            targetPanel.SetActive(false);
        if (Fuckyou != null)
            Fuckyou.SetActive(false);
    }

    void Update()
    {
        // ✅ 공부패널 (E키로 열고 닫기)
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (CurrentActive == null)
            {
                OpenPanel();
            }
            else if (CurrentActive == this)
            {
                ClosePanel();
            }
        }
    }

    // 공부패널 열기
    private void OpenPanel()
    {
        Debug.Log($"🪟 [PanelActivatorLocal] {panelName} 패널 열기");

        CurrentActive = this;

        if (targetPanel != null)
            targetPanel.SetActive(true);

        var loader = targetPanel.GetComponent<WebPageLoader>();
        if (loader != null)
        {
            loader.LoadExternalSite();
            Debug.Log($"🌐 [PanelActivatorLocal] WebPageLoader → LoadExternalSite() 호출");
        }

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
    }

    // 공부패널 닫기
    public void ClosePanel()
    {
        if (CurrentActive != this) return;

        Debug.Log($"🚪 [PanelActivatorLocal] {panelName} 패널 닫기");
        CurrentActive = null;

        if (targetPanel != null)
            targetPanel.SetActive(false);

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
    }

    // ✅ Q&A 안내패널 (자동 On/Off)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pv = other.GetComponent<PhotonView>();
            if (pv != null && pv.IsMine)
            {
                isPlayerNear = true;
                Debug.Log($"👣 [PanelActivatorLocal] 플레이어 접근 - {panelName}");

                if (Fuckyou != null)
                    Fuckyou.SetActive(true); // 👉 Q&A 안내패널 자동 켜기
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PhotonView pvExit = other.GetComponent<PhotonView>();
            if (pvExit != null && pvExit.IsMine)
            {
                isPlayerNear = false;
                Debug.Log($"🚶 [PanelActivatorLocal] 플레이어 이탈 - {panelName}");

                if (Fuckyou != null)
                    Fuckyou.SetActive(false); // 👉 Q&A 안내패널 자동 끄기

                // 범위를 벗어나면 공부패널도 닫기
                if (CurrentActive == this)
                    ClosePanel();
            }
        }
    }
}
