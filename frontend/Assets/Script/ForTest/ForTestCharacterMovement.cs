using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class ForTestCharacterMovement : MonoBehaviour
{
    [Header("이동 세팅")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("카메라 세팅")]
    public float rotationSmoothTime = 0.1f;

    [Header("Animator 파라미터 이름")]
    public string horizontalID = "Hor";
    public string verticalID = "Vert";
    public string stateID = "State";
    public string jumpID = "IsJump";

    [Header("UI 연결")]
    public GameObject studyPanel; // Inspector에서 StudyPanel 드래그해서 연결

    private CharacterController controller;
    private Animator animator;
    private Transform cameraTransform;

    private Vector3 velocity;
    private bool isGrounded;
    private float turnSmoothVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        cameraTransform = Camera.main != null ? Camera.main.transform : null;

        // 시작할 때 패널 꺼두기
        if (studyPanel != null)
            studyPanel.SetActive(false);
    }

    private void Update()
    {
        if (cameraTransform == null) return;

        HandleMovement();
        HandleStudyPanel();
    }

    private void HandleMovement()
    {
        // 땅 체크
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // 입력
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(h, 0f, v).normalized;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // 애니메이션 파라미터
        animator.SetFloat(horizontalID, h);
        animator.SetFloat(verticalID, v);
        animator.SetFloat(stateID, isRunning ? 1f : 0f);
        animator.SetBool(jumpID, !isGrounded);

        if (direction.magnitude >= 0.1f)
        {
            // 카메라 기준 방향 회전
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // 이동
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            float speed = isRunning ? runSpeed : walkSpeed;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // 점프
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 중력
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleStudyPanel()
    {
        if (Input.GetKeyDown(KeyCode.E) && studyPanel != null)
        {
            studyPanel.SetActive(true); // E 키로 켜기
        }
    }
}
