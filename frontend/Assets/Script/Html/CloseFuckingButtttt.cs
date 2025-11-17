using UnityEngine;
using UnityEngine.UI;

public class CloseFuckingButtttt : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject targetPanel;
    public ChairSeatLocal chairSeatLocal;

    private void Awake()
    {
        if (closeButton != null && targetPanel != null)
        {
            closeButton.onClick.AddListener(() => targetPanel.SetActive(false));
            chairSeatLocal.OnExitLecture();
        }
        else
        {
            Debug.LogWarning("[ClosePanel] closeButton 또는 targetPanel이 설정되지 않았습니다.");
        }
    }
    
}
