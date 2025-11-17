using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    [Header("UI 패널 오브젝트")]
    public GameObject panel; // 켜고 끌 대상 패널

    private bool isPlayerInRange = false;

    private void Start()
    {
        if (panel != null)
            panel.SetActive(false); // 시작 시 꺼두기
    }

    private void Update()
    {
        // 범위 안에 있을 때만 키 입력 받기
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (panel != null)
                panel.SetActive(!panel.activeSelf); // 토글 방식
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player entered trigger zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player left trigger zone");

            // 나가면 자동으로 꺼지게
            if (panel != null)
                panel.SetActive(false);
        }
    }
}
