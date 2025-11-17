using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TestMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform cameraTransform;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 카메라 기준 방향
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // 입력 방향 (카메라 기준)
        Vector3 moveDir = (forward * v + right * h).normalized;

        if (moveDir.magnitude >= 0.1f)
        {
            // 이동
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);

            // 플레이어 회전 (카메라 방향 쪽 보기)
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 0.15f);
        }
    }
}
