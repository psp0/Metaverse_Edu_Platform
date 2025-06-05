using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    private Transform m_Target;         // 따라갈 대상 (플레이어)
    private Vector2 m_MouseDelta;       // 마우스 이동량
    private float m_Scroll;             // 줌(스크롤) 입력

    private float m_Distance = 5f;      // 카메라 기본 거리
    private float m_Yaw = 0f;           // 좌우 회전
    private float m_Pitch = 20f;        // 상하 회전 (기본 위로 약간 보기)

    public float mouseSensitivity = 3f; // 마우스 감도
    public float minPitch = -30f;       // 아래로 회전 제한
    public float maxPitch = 60f;        // 위로 회전 제한

    public float minZoom = 2f;
    public float maxZoom = 10f;
    public float zoomSpeed = 2f;

    public void SetPlayer(Transform target)
    {
        Debug.Log("SetPlayer 호출됨: " + target.name);
        m_Target = target;
        // 처음 카메라 위치 초기화
        Vector3 offset = transform.position - m_Target.position;
        m_Distance = offset.magnitude;
    }

    public void SetInput(in Vector2 mouseDelta, float scroll)
    {
        m_MouseDelta = mouseDelta;
        m_Scroll = scroll;
    }

    void LateUpdate()
    {
        if (m_Target == null)
        {
            Debug.LogWarning("카메라 대상이 없음! (m_Target is null)");
            return;
        }
        Debug.Log("카메라 LateUpdate 실행 중 - 대상 위치: " + m_Target.position);

        // 1. 마우스 회전 적용
        m_Yaw += m_MouseDelta.x * mouseSensitivity;
        m_Pitch -= m_MouseDelta.y * mouseSensitivity;
        m_Pitch = Mathf.Clamp(m_Pitch, minPitch, maxPitch);

        // 2. 스크롤 줌
        m_Distance -= m_Scroll * zoomSpeed;
        m_Distance = Mathf.Clamp(m_Distance, minZoom, maxZoom);

        // 3. 최종 카메라 위치 계산
        Quaternion rotation = Quaternion.Euler(m_Pitch, m_Yaw, 0f);
        Vector3 offset = rotation * new Vector3(0f, 0f, -m_Distance);
        Vector3 cameraPosition = m_Target.position + offset + Vector3.up * 2f; // 카메라 위로 살짝

        transform.position = cameraPosition;
        transform.LookAt(m_Target.position + Vector3.up * 2f);
    }
}
