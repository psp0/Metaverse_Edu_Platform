using UnityEngine;
using System.IO;

/// <summary>
/// 간단한 백엔드 설정 로더
/// StreamingAssets/backend-config.json에서 URL을 읽어옴
/// </summary>
public static class BackendConfig 
{
    private static string _cachedUrl;
    private static bool _loaded = false;
    
    /// <summary>
    /// API URL을 반환합니다.
    /// </summary>
    public static string GetApiUrl()
    {
        if (!_loaded)
        {
            LoadConfig();
        }
        
        return _cachedUrl ?? GetDefaultUrl();
    }
    
    private static void LoadConfig()
    {
        var configPath = Path.Combine(Application.streamingAssetsPath, "backend-config.json");
        
        if (File.Exists(configPath))
        {
            try
            {
                var json = File.ReadAllText(configPath);
                var config = JsonUtility.FromJson<BackendConfigData>(json);

        bool configExists = File.Exists(configPath);
        if (configExists)
        {
            try
            {
                var json = File.ReadAllText(configPath);
                var config = JsonUtility.FromJson<BackendConfigData>(json);
                if (!string.IsNullOrEmpty(config.BaseUrl))
                {
                    // 포트가 443이고 HTTPS면 포트 생략, 그 외에는 포트 포함
                    if (config.BaseUrl.StartsWith("https://") && config.Port == "443")
                    {
                        _cachedUrl = config.BaseUrl;
                    }
                    else
                    {
                    // Validate that BaseUrl is a well-formed absolute URI
                    if (Uri.TryCreate(config.BaseUrl, UriKind.Absolute, out var uriResult))
                    {
                        // 포트가 443이고 HTTPS면 포트 생략, 그 외에는 포트 포함
                        if (config.BaseUrl.StartsWith("https://") && config.Port == "443")
                        {
                            _cachedUrl = config.BaseUrl;
                        }
                        else
                        {
                            _cachedUrl = $"{config.BaseUrl}:{config.Port}";
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"[BackendConfig] 잘못된 BaseUrl 형식: {config.BaseUrl}");
                    }
                }
                
                Debug.Log($"[BackendConfig] 설정 로드됨: {_cachedUrl}");
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning($"[BackendConfig] 설정 파일 읽기 실패: {ex.Message}");
            }
        }
        else
        {
            Debug.LogWarning($"[BackendConfig] 설정 파일 없음: {configPath}");
        }
        
        _loaded = true;
    }
    
    private static string GetDefaultUrl()
    {
        #if UNITY_EDITOR
        return "http://localhost:8080";
        #else
        Debug.LogError("[BackendConfig] Production API URL is not configured! Please provide backend-config.json with the correct BaseUrl and Port.");
        return null;
        #endif
    }
    
    [System.Serializable]
    private class BackendConfigData
    {
        public string BaseUrl;
        public string Port;
    }
}