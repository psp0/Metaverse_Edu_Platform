using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    private Transform m_Target;         // ���� ��� (�÷��̾�)
    private Vector2 m_MouseDelta;       // ���콺 �̵���
    private float m_Scroll;             // ��(��ũ��) �Է�

    private float m_Distance = 5f;      // ī�޶� �⺻ �Ÿ�
    private float m_Yaw = 0f;           // �¿� ȸ��
    private float m_Pitch = 20f;        // ���� ȸ�� (�⺻ ���� �ణ ����)

    public float mouseSensitivity = 3f; // ���콺 ����
    public float minPitch = -30f;       // �Ʒ��� ȸ�� ����
    public float maxPitch = 60f;        // ���� ȸ�� ����

    public float minZoom = 2f;
    public float maxZoom = 10f;
    public float zoomSpeed = 2f;

    public void SetPlayer(Transform target)
    {
        Debug.Log("SetPlayer ȣ���: " + target.name);
        m_Target = target;
        // ó�� ī�޶� ��ġ �ʱ�ȭ
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
            Debug.LogWarning("ī�޶� ����� ����! (m_Target is null)");
            return;
        }
        Debug.Log("ī�޶� LateUpdate ���� �� - ��� ��ġ: " + m_Target.position);

        // 1. ���콺 ȸ�� ����
        m_Yaw += m_MouseDelta.x * mouseSensitivity;
        m_Pitch -= m_MouseDelta.y * mouseSensitivity;
        m_Pitch = Mathf.Clamp(m_Pitch, minPitch, maxPitch);

        // 2. ��ũ�� ��
        m_Distance -= m_Scroll * zoomSpeed;
        m_Distance = Mathf.Clamp(m_Distance, minZoom, maxZoom);

        // 3. ���� ī�޶� ��ġ ���
        Quaternion rotation = Quaternion.Euler(m_Pitch, m_Yaw, 0f);
        Vector3 offset = rotation * new Vector3(0f, 0f, -m_Distance);
        Vector3 cameraPosition = m_Target.position + offset + Vector3.up * 2f; // ī�޶� ���� ��¦

        transform.position = cameraPosition;
        transform.LookAt(m_Target.position + Vector3.up * 2f);
    }
}
