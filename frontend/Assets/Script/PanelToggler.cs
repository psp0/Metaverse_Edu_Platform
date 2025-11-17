using UnityEngine;
using UnityEngine.UI;

public class PanelToggler : MonoBehaviour
{
    [Header("버튼 및 패널 설정")]
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject targetPanel;

    private void Awake()
    {
        if (openButton != null)
            openButton.onClick.AddListener(() => SetPanelActive(true));

        if (closeButton != null)
            closeButton.onClick.AddListener(() => SetPanelActive(false));
    }

    private void SetPanelActive(bool isActive)
    {
        if (targetPanel != null)
            targetPanel.SetActive(isActive);
        else
            Debug.LogWarning("[PanelToggler] 타겟 패널이 설정되지 않았습니다.");
    }
}
