using UnityEngine;

public class ESCPanelHandler : MonoBehaviour
{
    [SerializeField] private GameObject defaultPanel;   // 내부에서 기본으로 다시 보여줄 Panel

    void OnEnable()
    {
        if (defaultPanel != null)
            defaultPanel.SetActive(true);
    }
}
