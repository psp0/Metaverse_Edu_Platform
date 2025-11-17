using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("이동 세팅")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;        // 점프 위로 이동 속도
    public float gravity = -9.81f;
    public float jumpCooldown = 1f;     // 점프 후 재점프 대기시간(초)

    [Header("카메라 세팅")]
    public float rotationSmoothTime = 0.1f;

    [Header("Animator 파라미터 이름")]
    public string speedID = "Speed";
    public string jumpTriggerID = "JumpTrigger";

    private CharacterController controller;
    private Animator animator;
    private Transform cameraTransform;
    private PhotonView view;

    private Vector3 velocity;
    private float turnSmoothVelocity;

    private bool canJump = true;   // 점프 가능 여부
    private bool isJumping = false;
    public bool JumpSet = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        view = GetComponent<PhotonView>();

        cameraTransform = Camera.main != null ? Camera.main.transform : null;
    }

    private void Update()
    {
        if (!view.IsMine) return;

        HandleMovement();
        HandleJumpInput();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(h, 0f, v).normalized;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float animSpeed = direction.magnitude > 0 ? (isRunning ? 1f : 0.5f) : 0f;
        animator.SetFloat(speedID, animSpeed, 0.1f, Time.deltaTime);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            float speed = isRunning ? runSpeed : walkSpeed;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    private void HandleJumpInput()
    {
        if (JumpSet == true)
        {

            // 점프 입력
            if (Input.GetButtonDown("Jump") && canJump)
            {
                canJump = false;
                isJumping = true;

                // 위로 속도 부여
                velocity.y = jumpForce;
                animator.SetTrigger(jumpTriggerID);

                // 쿨타임 시작
                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }
    }

    private void ApplyGravity()
    {
        // 중력 적용 (지면 판정 대신 일정 속도 감소)
        if (isJumping)
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            // y가 음수로 일정 이상 떨어지면 착지 처리
            if (velocity.y < -2f)
            {
                isJumping = false;
                velocity.y = -2f;
            }
        }
    }

    private void ResetJump()
    {
        canJump = true; // 쿨타임 끝나면 점프 가능
    }
}
