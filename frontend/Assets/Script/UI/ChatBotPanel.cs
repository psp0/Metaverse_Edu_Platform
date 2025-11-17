using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatBotPanel : MonoBehaviour
{
    [Header("UI References")]
    public ScrollRect scrollView;
    public Transform content;
    public TMP_InputField inputField;
    public Button sendButton;
    public Button imageButton;

    [Header("Prefabs")]
    public GameObject messagePrefab;

    void Start()
    {
        sendButton.onClick.AddListener(OnSendMessage);
        imageButton.onClick.AddListener(OnAddImage);
    }

    void OnSendMessage()
    {
        string message = inputField.text.Trim();
        if (!string.IsNullOrEmpty(message))
        {
            AddMessage("나", message, true);
            inputField.text = "";
        }
    }

    void OnAddImage()
    {
        AddMessage("나", "[이미지 첨부]", true);
    }

    public void AddMessage(string sender, string text, bool isUser)
    {
        GameObject msgObj = Instantiate(messagePrefab, content);

        TMP_Text msgText = msgObj.GetComponentInChildren<TMP_Text>();
        Image bg = msgObj.GetComponentInChildren<Image>();
        HorizontalLayoutGroup layout = msgObj.GetComponent<HorizontalLayoutGroup>();

        msgText.text = text; // sender는 필요 없으면 제외 가능

        if (isUser)
        {
            layout.childAlignment = TextAnchor.MiddleRight;
            msgText.alignment = TextAlignmentOptions.MidlineRight;
            bg.color = new Color(0.8f, 1f, 0.8f); // 연한 초록 (유저)
        }
        else
        {
            layout.childAlignment = TextAnchor.MiddleLeft;
            msgText.alignment = TextAlignmentOptions.MidlineLeft;
            bg.color = new Color(0.9f, 0.9f, 0.9f); // 회색 (AI)
        }

        Canvas.ForceUpdateCanvases();
        scrollView.verticalNormalizedPosition = 0f;
    }



}
