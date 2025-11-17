using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ChatUIManager : MonoBehaviour
{
    public static ChatUIManager instance;

    [Header("UI 연결")]
    public RectTransform content;
    public ScrollRect scrollRect;
    public TMP_InputField chatInputField;
    public TMP_FontAsset defaultFont;
    public int MaxMessages = 50;

    private List<string> chatMessages = new List<string>();
    private bool isTyping = false;

    private PlayerMovement playerMovement;
    private CameraController cameraController;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        chatInputField.gameObject.SetActive(true);
        chatInputField.text = "";

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerMovement = player.GetComponent<PlayerMovement>();

        cameraController = Camera.main.GetComponent<CameraController>();

        chatInputField.onEndEdit.AddListener(delegate { StartCoroutine(SendMessageNextFrame()); });
    }

    private void Update()
    {
        // ✅ T키로 포커스
        if (Input.GetKeyDown(KeyCode.T))
            EnterChatMode();

        // ✅ ESC로 빠져나오기
        if (isTyping && Input.GetKeyDown(KeyCode.Escape))
            ExitChatMode();
    }

    private void EnterChatMode()
    {
        isTyping = true;
        chatInputField.interactable = true;
        chatInputField.Select();
        chatInputField.ActivateInputField();

        if (playerMovement) playerMovement.enabled = false;
        if (cameraController) cameraController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("💬 채팅 입력 시작");
    }

    private void ExitChatMode()
    {
        isTyping = false;
        chatInputField.interactable = false;
        EventSystem.current.SetSelectedGameObject(null);

        if (playerMovement) playerMovement.enabled = true;
        if (cameraController) cameraController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("🚪 채팅 입력 종료 (카메라 복구됨)");
    }

    private IEnumerator SendMessageNextFrame()
    {
        // ✅ 한 프레임 대기 → 한글 입력 완성 후 처리
        yield return null;
        Debug.Log("📨 SendMessageNextFrame() 호출됨");
        string message = chatInputField.text.Trim();
        if (!string.IsNullOrEmpty(message))
        {
            // ✅ 한글 마지막 글자 잘림 방지용 공백 추가
            message += " ";

            ChatManager.instance.SendMessageToAll(message);

            chatInputField.text = "";
        }

        // ✅ 메시지 전송 후 바로 카메라 복구
        ExitChatMode();
    }

    public void DisplayMessage(string message)
    {
        chatMessages.Add(message);
        if (chatMessages.Count > MaxMessages)
            chatMessages.RemoveAt(0);

        UpdateChatLog();
    }

    private void UpdateChatLog()
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);

        foreach (string msg in chatMessages)
        {
            GameObject msgObj = new GameObject("ChatMessage", typeof(TextMeshProUGUI));
            msgObj.transform.SetParent(content, false);

            var tmp = msgObj.GetComponent<TextMeshProUGUI>();
            tmp.text = msg;
            tmp.fontSize = 25;
            tmp.enableWordWrapping = true;
            tmp.alignment = TextAlignmentOptions.TopLeft;
            tmp.color = Color.white;

            if (defaultFont != null)
                tmp.font = defaultFont;
            else if (chatInputField != null)
                tmp.font = chatInputField.textComponent.font;

            RectTransform rt = tmp.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(580f, rt.sizeDelta.y);
            rt.pivot = new Vector2(0, 1);
            rt.anchorMin = new Vector2(0, 1);
            rt.anchorMax = new Vector2(0, 1);
            rt.anchoredPosition = new Vector2(10f, rt.anchoredPosition.y);

            var fitter = msgObj.AddComponent<ContentSizeFitter>();
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
