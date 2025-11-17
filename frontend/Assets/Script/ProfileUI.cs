using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class ProfileUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject profilePanel;   // 메인 프로필 패널
    public GameObject editPopupPanel; // 수정 팝업 패널

    [Header("Profile Texts")]
    public TMP_Text nicknameText;
    public TMP_Text emailText;
    public TMP_Text phoneText;
    public TMP_Text nameText;
    public TMP_Text birthDateText;
    public TMP_Text genderText;

    [Header("Edit Popup")]
    public TMP_Text popupTitle;
    public TMP_InputField popupInput;
    private string editingField; // 지금 수정하려는 항목 기억

    private string baseUrl = "http://mep.run.place:53001/api/user/me";

    // ===== 프로필 조회 =====
    public void ShowProfile()
    {
        profilePanel.SetActive(true);
        StartCoroutine(GetUserProfile());
    }

    private IEnumerator GetUserProfile()
    {
        string token = AuthManager.Instance.AccessToken;

        UnityWebRequest request = UnityWebRequest.Get(baseUrl);
        request.SetRequestHeader("Authorization", "Bearer " + token);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("프로필 조회 실패: " + request.error);
        }
        else
        {
            ProfileData profile = JsonUtility.FromJson<ProfileData>(request.downloadHandler.text);

            nicknameText.text = profile.nickname;
            emailText.text = profile.email;
            phoneText.text = profile.phone;
            nameText.text = profile.name;
            birthDateText.text = profile.birthDate;
            genderText.text = profile.gender;
        }
    }

    // ===== 수정 팝업 열기 =====
    public void OnClickEdit(string fieldName, string currentValue)
    {
        editingField = fieldName;
        popupTitle.text = fieldName + " 변경";
        popupInput.text = currentValue;
        editPopupPanel.SetActive(true);
    }

    // ===== 수정 적용 =====
    public void OnClickSubmitChange()
    {
        string newValue = popupInput.text;
        StartCoroutine(UpdateProfile(editingField, newValue));
        editPopupPanel.SetActive(false);
    }

    private IEnumerator UpdateProfile(string field, string value)
    {
        string token = AuthManager.Instance.AccessToken;

        // JSON 만들기
        string jsonBody = "{ \"" + field + "\": \"" + value + "\" }";

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        UnityWebRequest request = new UnityWebRequest(baseUrl, "PUT");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + token);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("수정 실패: " + request.error);
        }
        else
        {
            Debug.Log("수정 성공: " + request.downloadHandler.text);
            // 수정 후 프로필 새로고침
            StartCoroutine(GetUserProfile());
        }
    }
}
