using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -5);
    public float rotationSpeed = 120f;

    private float yaw;
    private float pitch;
    private bool inputLocked = false;

    public void Initalize(Transform player)
    {
        target = player;
        LockCursor(); // 기본 잠금
        yaw = 0;
        pitch = 15f;
    }

    void LateUpdate()
    {
        if (target == null || inputLocked) return;

        // 마우스 입력으로 회전
        yaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -30f, 60f);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = target.position + rotation * offset;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }

    // ✅ 커서 잠금/해제 메서드
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inputLocked = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        inputLocked = true;
    }
}
