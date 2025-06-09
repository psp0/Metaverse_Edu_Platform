using UnityEngine;
using Photon.Pun;

namespace Controller
{
    [RequireComponent(typeof(CharacterMover))]
    public class MovePlayerInput : MonoBehaviourPun
    {
        [Header("Character")]
        [SerializeField] private string m_HorizontalAxis = "Horizontal";
        [SerializeField] private string m_VerticalAxis = "Vertical";
        [SerializeField] private string m_JumpButton = "Jump";
        [SerializeField] private KeyCode m_RunKey = KeyCode.LeftShift;

        [Header("Camera")]
        [SerializeField] private PlayerFollowCamera m_Camera;
        [SerializeField] private string m_MouseX = "Mouse X";
        [SerializeField] private string m_MouseY = "Mouse Y";
        [SerializeField] private string m_MouseScroll = "Mouse ScrollWheel";

        private CharacterMover m_Mover;
        private Vector2 m_Axis;
        private bool m_IsRun;
        private bool m_IsJump;
        private Vector2 m_MouseDelta;
        private float m_Scroll;

        private void Awake()
        {
            m_Mover = GetComponent<CharacterMover>();

            if (m_Camera == null)
                m_Camera = Camera.main?.GetComponent<PlayerFollowCamera>();

            if (m_Camera != null)
                m_Camera.SetPlayer(transform);
        }

        private void Update()
        {
            GatherInput();
            ApplyInput();
        }

        private void GatherInput()
        {
            m_Axis = new Vector2(Input.GetAxis(m_HorizontalAxis), Input.GetAxis(m_VerticalAxis));
            m_IsRun = Input.GetKey(m_RunKey);
            m_IsJump = Input.GetButton(m_JumpButton);
            m_MouseDelta = new Vector2(Input.GetAxis(m_MouseX), Input.GetAxis(m_MouseY));
            m_Scroll = Input.GetAxis(m_MouseScroll);
        }

        private void ApplyInput()
        {
            if (m_Mover != null && m_Camera != null)
            {
                // ī�޶� ���� ���� ���
                Vector3 camForward = m_Camera.transform.forward;
                Vector3 camRight = m_Camera.transform.right;

                camForward.y = 0f;
                camRight.y = 0f;
                camForward.Normalize();
                camRight.Normalize();

                // �Է¿� ���� ���� �̵� ���� (��Ʈ������ ����)
                Vector3 moveDir = (camForward * m_Axis.y + camRight * m_Axis.x);

                // ĳ���� �ٶ� ���� = ī�޶� ����
                Vector3 targetPos = transform.position + camForward;

                // �ִϸ��̼ǿ� 2D���� �״�� ����
                Vector2 inputDir = new Vector2(m_Axis.x, m_Axis.y);

                m_Mover.SetInput(in inputDir, in targetPos, m_IsRun, m_IsJump);
            }

            if (m_Camera != null)
                m_Camera.SetInput(in m_MouseDelta, m_Scroll);
        }


    }
}
