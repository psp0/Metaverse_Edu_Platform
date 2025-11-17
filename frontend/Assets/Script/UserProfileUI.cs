using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

[System.Serializable]
public class UserData
{
    public string userId;
    public string email;
    public string phone;
    public string name;
    public string nickname;
    public string birthDate;
    public string gender;
}

public class UserProfileUI : MonoBehaviour
{
    [Header("View Panel (항상 켜짐)")]
    public TMP_Text viewEmail;
    public TMP_Text viewName;
    public TMP_Text viewPhone;
    public TMP_Text viewNickname;
    public TMP_Text viewBirth;
    public TMP_Text viewGender;

    [Header("Edit Panel (팝업)")]
    public GameObject editPanel;
    public TMP_InputField inputName;
    public TMP_InputField inputPhone;
    public TMP_InputField inputNickname;
    public TMP_InputField inputBirth;
    public TMP_Dropdown inputGender;

    private string baseUrl = "http://localhost/api/users"; // ✅ 기본 API 경로
    private string accessToken;
    private UserData currentUser;

    void Start()
    {
        accessToken = AuthManager.Instance.AccessToken;
        Debug.Log("[AuthManager] AccessToken = " + accessToken);

        editPanel.SetActive(false); // 시작 시 수정창 닫기
        StartCoroutine(GetUserInfo());
    }

    // 🔹 내 정보 조회
    IEnumerator GetUserInfo()
    {
        string url = $"{baseUrl}/me"; // ✅ /api/users/me 엔드포인트

        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            req.SetRequestHeader("Authorization", "Bearer " + accessToken);
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("✅ 내 정보 조회 성공");
                Debug.Log("[SERVER RESPONSE] " + req.downloadHandler.text);

                currentUser = JsonUtility.FromJson<UserData>(req.downloadHandler.text);
                UpdateViewPanel();

                // ✅ Photon 닉네임 설정 (여기로 이동)
                if (!string.IsNullOrEmpty(currentUser.nickname))
                {
                    Photon.Pun.PhotonNetwork.NickName = currentUser.nickname;
                    Debug.Log($"[Photon] NickName 설정 완료: {Photon.Pun.PhotonNetwork.NickName}");
                }
            }
            else
            {
                Debug.LogError($"❌ 내 정보 조회 실패: {req.responseCode} {req.error}");
                Debug.LogError(req.downloadHandler.text);
            }

        }
    }

    // 🔹 ViewPanel UI 업데이트
    void UpdateViewPanel()
    {
        if (currentUser == null)
        {
            Debug.LogError("[UserProfileUI] currentUser is null");
            return;
        }

        viewEmail.text = currentUser.email;
        viewName.text = currentUser.name;
        viewPhone.text = currentUser.phone;
        viewNickname.text = currentUser.nickname;
        viewBirth.text = currentUser.birthDate;
        viewGender.text = currentUser.gender == "M" ? "남성" : "여성";
    }

    // 🔹 수정하기 버튼 클릭 시
    public void OpenEditPanel()
    {
        if (currentUser == null)
        {
            Debug.LogError("[UserProfileUI] currentUser is null — 조회가 아직 안 됨");
            return;
        }

        editPanel.SetActive(true);
        inputName.text = currentUser.name;
        inputPhone.text = currentUser.phone;
        inputNickname.text = currentUser.nickname;
        inputBirth.text = currentUser.birthDate;
        inputGender.value = currentUser.gender == "M" ? 0 : 1;
    }

    // 🔹 닫기 버튼 클릭 시
    public void CloseEditPanel()
    {
        editPanel.SetActive(false);
    }

    // 🔹 저장 버튼 클릭 시
    public void OnClickSave()
    {
        StartCoroutine(UpdateUserProfile());
    }

    // 🔹 사용자 정보 수정
    IEnumerator UpdateUserProfile()
    {
        string url = $"{baseUrl}/me"; // ✅ PATCH /api/users/me
        UserData updated = new UserData
        {
            name = inputName.text,
            phone = inputPhone.text,
            nickname = inputNickname.text,
            birthDate = inputBirth.text,
            gender = inputGender.value == 0 ? "M" : "F"
        };

        string jsonBody = JsonUtility.ToJson(updated);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        using (UnityWebRequest req = new UnityWebRequest(url, "PATCH"))
        {
            req.uploadHandler = new UploadHandlerRaw(bodyRaw);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Authorization", "Bearer " + accessToken);
            req.SetRequestHeader("Content-Type", "application/json");

            Debug.Log("[UserProfileUI] PATCH 요청 전송: " + jsonBody);

            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("✅ 프로필 수정 성공");
                Debug.Log("[SERVER RESPONSE] " + req.downloadHandler.text);

                currentUser = updated;
                UpdateViewPanel();
                CloseEditPanel();
            }
            else
            {
                Debug.LogError($"❌ 수정 실패: {req.responseCode} {req.error}");
                Debug.LogError(req.downloadHandler.text);
            }
        }
    }
}
