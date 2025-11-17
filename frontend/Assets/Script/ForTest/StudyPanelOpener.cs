using UnityEngine;

public class StudyPanelOpener : MonoBehaviour
{
    [Header("연결할 패널")]
    public GameObject studyPanel;   // Inspector에서 StudyPanel UI 연결
    [Header("플레이어 오브젝트")]
    public GameObject player;       // Hierarchy 속 플레이어 오브젝트 연결

    private bool isPlayerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerInside = false;
        }
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            studyPanel.SetActive(true);  // 패널 켜기
        }
    }
}
