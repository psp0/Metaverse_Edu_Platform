using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System;

public class RegisterHandler : MonoBehaviour
{
    [Header("Input Fields")]
    public TMP_InputField userIdInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;
    public TMP_InputField emailInput;
    public TMP_InputField phoneInput;
    public TMP_InputField nameInput;
    public TMP_InputField nicknameInput;
    public TMP_Dropdown yearDropdown;
    public TMP_Dropdown monthDropdown;
    public TMP_Dropdown dayDropdown;

    [Header("Gender Toggle")]
    public Toggle maleToggle;
    public Toggle femaleToggle;

    [Header("UI")]
    public Button registerButton;
    public TextMeshProUGUI messageText;

    [Header("Register Panel")]
    public GameObject registerPanel;

    private string baseUrl = $"{Environment.GetEnvironmentVariable("BACK_SERVER_URL")}:{Environment.GetEnvironmentVariable("BACK_SERVER_PORT")}";

    void Start()
    {
        registerButton.onClick.AddListener(OnRegisterClicked);
    }

    void OnRegisterClicked()
    {
        string userId = userIdInput.text;
        string password = passwordInput.text;
        string confirmPassword = confirmPasswordInput.text;
        string email = emailInput.text;
        string phone = phoneInput.text;
        string name = nameInput.text;
        string nickname = nicknameInput.text;
        string birthDate = $"{yearDropdown.captionText.text}-{monthDropdown.captionText.text}-{dayDropdown.captionText.text}";
        string gender = maleToggle.isOn ? "M" : (femaleToggle.isOn ? "F" : "");

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) ||
            string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(name) ||
            string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(birthDate) || string.IsNullOrEmpty(gender))
        {
            messageText.text = "모든 항목을 입력해주세요.";
            return;
        }

        if (password != confirmPassword)
        {
            messageText.text = "비밀번호가 일치하지 않습니다.";
            return;
        }

        StartCoroutine(RegisterCoroutine(userId, password, confirmPassword, email, phone, name, nickname, birthDate, gender));
    }

    IEnumerator RegisterCoroutine(string userId, string password, string confirmPassword,
                                   string email, string phone, string name, string nickname,
                                   string birthDate, string gender)
    {
        string url = baseUrl + "/api/users/register";

        var data = new RegisterRequest
        {
            userId = userId,
            password = password,
            confirmPassword = confirmPassword,
            email = email,
            phone = phone,
            name = name,
            nickname = nickname,
            birthDate = birthDate,
            gender = gender
        };

        string jsonData = JsonUtility.ToJson(data);
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // 🔐 인증서 우회 적용
        //request.certificateHandler = new BypassCertificateHandler();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            messageText.text = "회원가입 성공!";
            // TODO: 자동 로그인 or 로그인 씬 이동
            if (registerPanel != null)
                registerPanel.SetActive(false);
        }
        else
        {
            messageText.text = "회원가입 실패: " + request.downloadHandler.text;
            Debug.LogError("회원가입 실패: " + request.error);
        }
    }

    [Serializable]
    private class RegisterRequest
    {
        public string userId;
        public string password;
        public string confirmPassword;
        public string email;
        public string phone;
        public string name;
        public string nickname;
        public string birthDate;
        public string gender;
    }
}
