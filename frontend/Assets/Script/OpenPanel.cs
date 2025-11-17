using UnityEngine;
using UnityEngine.UI;

public class OpenPanel : MonoBehaviour
{
    [SerializeField] private Button openButton;
    [SerializeField] private GameObject targetPanel;

    private void Awake()
    {
        if (openButton != null && targetPanel != null)
        {
            openButton.onClick.AddListener(() => targetPanel.SetActive(true));
        }
        else
        {
            Debug.LogWarning("[OpenPanel] openButton 또는 targetPanel이 설정되지 않았습니다.");
        }
    }
}
