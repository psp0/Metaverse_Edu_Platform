using UnityEngine;

public class UI_TogglePanel : MonoBehaviour
{
    public GameObject targetPanel;

    public void ShowPanel()
    {
        if (targetPanel != null)
            targetPanel.SetActive(true);
    }

    public void HidePanel()
    {
        if (targetPanel != null)
            targetPanel.SetActive(false);
    }

    public void TogglePanel()
    {
        if (targetPanel != null)
            targetPanel.SetActive(!targetPanel.activeSelf);
    }
}
