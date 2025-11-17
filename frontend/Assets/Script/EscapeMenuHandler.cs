using UnityEngine;

public class EscapeMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject escapePanel;

    private void Start()
    {
        if (escapePanel != null)
            escapePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && escapePanel != null)
        {
            Debug.Log("ESC ´­¸²");
            escapePanel.SetActive(!escapePanel.activeSelf);
        }
    }
}

