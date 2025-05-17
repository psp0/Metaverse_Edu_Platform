using UnityEngine;

public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance;

    public string AccessToken => PlayerPrefs.GetString("accessToken");
    public string RefreshToken => PlayerPrefs.GetString("refreshToken");

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveTokens(string accessToken, string refreshToken)
    {
        PlayerPrefs.SetString("accessToken", accessToken);
        PlayerPrefs.SetString("refreshToken", refreshToken);
    }

    public void Logout()
    {
        PlayerPrefs.DeleteKey("accessToken");
        PlayerPrefs.DeleteKey("refreshToken");
    }
}
