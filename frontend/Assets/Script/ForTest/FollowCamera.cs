using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 7f;
    public float height = 3f;
    public float sensitivity = 3f;

    private float yaw = 0f;
    private float pitch = 15f;

    private void LateUpdate()
    {
        if (target == null) return;

        // 마우스로 회전 입력
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, 5f, 60f);

        // 회전 계산
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // 위치 갱신
        Vector3 targetPos = target.position - rotation * Vector3.forward * distance + Vector3.up * height;
        transform.position = targetPos;

        // 타겟 바라보기
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
