using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.IO;

/// <summary>
/// 비동기 방식의 백엔드 설정 로더
/// 모든 플랫폼에서 StreamingAssets/backend-config.json을 안전하게 읽어옴
/// 첫 번째 접근 시 자동으로 초기화됩니다.
/// </summary>
public static class BackendConfig
{
    private static string _cachedUrl;
    private static bool _isInitialized = false;
    private static bool _isInitializing = false;
    private static bool _hasTriedSync = false;

    // API URL에 접근하기 위한 프로퍼티
    public static string ApiUrl
    {
        get
        {
            if (!_isInitialized && !_hasTriedSync)
            {
                // 동기식으로 한 번 시도
                TryInitializeSync();
            }
            
            return _cachedUrl ?? GetDefaultUrl();
        }
    }

    public static bool IsInitialized => _isInitialized;

    /// <summary>
    /// 다른 스크립트와의 호환성을 위한 메서드 (ApiUrl 프로퍼티와 동일)
    /// </summary>
    public static string GetApiUrl()
    {
        return ApiUrl;
    }

    /// <summary>
    /// 동기식으로 설정 파일을 읽으려고 시도합니다. (Editor에서만 작동)
    /// </summary>
    private static void TryInitializeSync()
    {
        _hasTriedSync = true;
        
        #if UNITY_EDITOR
        try
        {
            var configPath = Path.Combine(Application.streamingAssetsPath, "backend-config.json");
            if (System.IO.File.Exists(configPath))
            {
                var json = System.IO.File.ReadAllText(configPath);
                var config = JsonUtility.FromJson<BackendConfigData>(json);
                
                if (!string.IsNullOrEmpty(config.BaseUrl))
                {
                    _cachedUrl = BuildUrl(config.BaseUrl, config.Port);
                    _isInitialized = true;
                    Debug.Log($"[BackendConfig] 동기 초기화 성공: {_cachedUrl}");
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning($"[BackendConfig] 동기 초기화 실패: {ex.Message}");
        }
        #else
        Debug.LogWarning("[BackendConfig] 빌드된 버전에서는 비동기 초기화가 필요합니다. Initialize() 코루틴을 사용하세요.");
        #endif
    }

    /// <summary>
    /// 설정을 비동기적으로 로드하고 초기화합니다. 시작 시 한 번 호출해야 합니다.
    /// </summary>
    public static IEnumerator Initialize(Action onComplete = null)
    {
        if (_isInitialized || _isInitializing)
        {
            yield break;
        }

        _isInitializing = true;
        
        var configPath = Path.Combine(Application.streamingAssetsPath, "backend-config.json");

        // UnityWebRequest는 플랫폼에 맞는 올바른 경로/URL을 자동으로 처리합니다.
        using (UnityWebRequest request = UnityWebRequest.Get(configPath))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                try
                {
                    var json = request.downloadHandler.text;
                    var config = JsonUtility.FromJson<BackendConfigData>(json);

                    if (!string.IsNullOrEmpty(config.BaseUrl))
                    {
                        _cachedUrl = BuildUrl(config.BaseUrl, config.Port);
                        Debug.Log($"[BackendConfig] 설정 로드 성공: {_cachedUrl}");
                    }
                    else
                    {
                        Debug.LogWarning("[BackendConfig] BaseUrl이 비어있습니다.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"[BackendConfig] JSON 파싱 실패: {ex.Message}");
                }
            }
            else
            {
                Debug.LogWarning($"[BackendConfig] 설정 파일을 찾을 수 없거나 읽기 실패: {configPath}, Error: {request.error}");
            }
        }
        
        _isInitializing = false;
        _isInitialized = true;
        onComplete?.Invoke();
    }

    // URL 빌드 및 기본 URL 로직은 기존 코드와 유사하게 유지
    private static string BuildUrl(string baseUrl, string port)
    {
        // UriBuilder를 사용하면 더 안전하고 간결하게 URL을 구성할 수 있습니다.
        try
        {
            UriBuilder uriBuilder = new UriBuilder(baseUrl);
            if (!string.IsNullOrEmpty(port) && int.TryParse(port, out int portNumber))
            {
                // 포트 번호가 제공된 경우 설정
                uriBuilder.Port = portNumber;
            }
            return uriBuilder.Uri.ToString().TrimEnd('/');
        }
        catch (UriFormatException ex)
        {
            Debug.LogWarning($"[BackendConfig] 잘못된 BaseUrl 형식: {baseUrl}. 오류: {ex.Message}");
            return null;
        }
    }

    private static string GetDefaultUrl()
    {
        #if UNITY_EDITOR
        return "http://localhost:8080";
        #else        
        Debug.LogError("[BackendConfig] 프로덕션 API URL이 설정되지 않았습니다! backend-config.json 파일이 필요합니다.");
        return null; 
        #endif
    }

    [Serializable]
    private class BackendConfigData
    {
        public string BaseUrl = "";
        public string Port = "";
    }
}