using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class UserPasswordChangeUI : MonoBehaviour
{
    [Header("입력 필드")]
    public TMP_InputField inputCurrentPw;
    public TMP_InputField inputNewPw;
    public TMP_InputField inputConfirmPw;

    [Header("메시지 표시")]
    public TMP_Text confirmMsg; // 비밀번호 불일치 즉시 표시
    public GameObject errorPanel; // 서버 오류 패널
    public TMP_Text errorMsg;     // 오류 메시지 텍스트

    private string baseUrl = "http://localhost/api/users/me/password";
    private string accessToken;

    void Start()
    {
        accessToken = AuthManager.Instance.AccessToken;
        confirmMsg.gameObject.SetActive(false);
        errorPanel.SetActive(false);

        // ✅ 실시간으로 new/confirm 비밀번호 일치 검사
        inputNewPw.onValueChanged.AddListener(delegate { CheckPasswordMatch(); });
        inputConfirmPw.onValueChanged.AddListener(delegate { CheckPasswordMatch(); });
    }

    // 🔹 비밀번호 일치 여부 실시간 확인
    private void CheckPasswordMatch()
    {
        if (string.IsNullOrEmpty(inputNewPw.text) && string.IsNullOrEmpty(inputConfirmPw.text))
        {
            confirmMsg.gameObject.SetActive(false);
            return;
        }

        if (inputNewPw.text != inputConfirmPw.text)
        {
            confirmMsg.text = "비밀번호가 일치하지 않습니다.";
            confirmMsg.color = Color.red;
            confirmMsg.gameObject.SetActive(true);
        }
        else
        {
            confirmMsg.text = "비밀번호가 일치합니다.";
            confirmMsg.color = new Color(0.1f, 0.7f, 0.2f); // 초록색 느낌
            confirmMsg.gameObject.SetActive(true);
        }
    }

    // 🔹 버튼 클릭 → 서버로 변경 요청
    public void OnClickChangePassword()
    {
        errorPanel.SetActive(false);

        // 아직 비밀번호가 불일치하면 전송 X
        if (inputNewPw.text != inputConfirmPw.text)
        {
            ShowErrorPanel("새 비밀번호가 서로 일치하지 않습니다.");
            return;
        }

        StartCoroutine(ChangePassword());
    }

    private IEnumerator ChangePassword()
    {
        PasswordChangeRequest reqData = new PasswordChangeRequest
        {
            currentPassword = inputCurrentPw.text,
            newPassword = inputNewPw.text
        };

        string jsonBody = JsonUtility.ToJson(reqData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        using (UnityWebRequest req = new UnityWebRequest(baseUrl, "PUT"))
        {
            req.uploadHandler = new UploadHandlerRaw(bodyRaw);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Authorization", "Bearer " + accessToken);
            req.SetRequestHeader("Content-Type", "application/json");

            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("✅ 비밀번호 변경 성공!");
                ShowErrorPanel("비밀번호가 성공적으로 변경되었습니다.", true);
            }
            else
            {
                string serverMsg = req.downloadHandler.text;
                Debug.LogError($"❌ 비밀번호 변경 실패: {req.responseCode} {req.error}");
                Debug.LogError(serverMsg);

                if (req.responseCode == 400 || req.responseCode == 401)
                    ShowErrorPanel("현재 비밀번호가 틀렸습니다.");
                else
                    ShowErrorPanel("서버 오류가 발생했습니다. 잠시 후 다시 시도해주세요.");
            }
        }
    }

    private void ShowErrorPanel(string message, bool success = false)
    {
        errorPanel.SetActive(true);
        errorMsg.text = message;
        errorMsg.color = success ? Color.green : Color.red;
    }

    public void CloseErrorPanel()
    {
        errorPanel.SetActive(false);
    }

    public void OpenPasswordPanel()
    {
        // 🔹 입력창 초기화
        inputCurrentPw.text = "";
        inputNewPw.text = "";
        inputConfirmPw.text = "";

        // 🔹 메시지 및 패널 초기화
        confirmMsg.gameObject.SetActive(false);
        errorPanel.SetActive(false);

        // 🔹 필요하면 패널 자체 켜기 (비밀번호 변경 UI를 숨겨놨다면)
        gameObject.SetActive(true);

        Debug.Log("[PasswordChangeUI] 비밀번호 변경 패널 초기화 완료");
    }

    [System.Serializable]
    private class PasswordChangeRequest
    {
        public string currentPassword;
        public string newPassword;
    }
}
