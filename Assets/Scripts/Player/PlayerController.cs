using Cinemachine;
using Plugins.Tools;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : Singleton<PlayerController>
    {
        public CinemachineFreeLook mainCamera;
        
        private PlayerInput m_Input;
        private CharacterController m_CharCtrl;
        
        public Transform cam;
        public float speed = 6f;
        
        public float turnSmoothTime = 0.1f;
        private float m_TurnSmoothVelocity;

        public float gravity;

        public float jumpForce;
        private float m_VerticalSpeed;

        private bool m_IsGrounded, m_CanJump;

        private const float STICKING_GRAVITY_PROPORTION = 3;
        private const float JUMP_ABORT_SPEED = 3;

        protected override void Awake()
        {
            base.Awake();
            m_CharCtrl = GetComponent<CharacterController>();
            m_Input = GetComponent<PlayerInput>();
        }

        private void FixedUpdate()
        {
            ForwardMovement();
            CalculateVerticalMovement();
        }

        private void OnAnimatorMove()
        {
            Vector3 movement = transform.forward * Time.deltaTime;

            movement += m_VerticalSpeed * Vector3.up * Time.deltaTime;

            m_CharCtrl.Move(movement);

            m_IsGrounded = m_CharCtrl.isGrounded;
        }

        private void CalculateVerticalMovement()
        {
            if (!m_Input.JumpInput && m_IsGrounded) m_CanJump = true;

            if (m_IsGrounded)
            {
                m_VerticalSpeed = -gravity * STICKING_GRAVITY_PROPORTION;

                if (!m_Input.JumpInput || !m_CanJump) return;
                m_VerticalSpeed = jumpForce;
                m_IsGrounded = false;
                m_CanJump = false;
            }
            else
            {
                if (!m_Input.JumpInput && m_VerticalSpeed > 0.0f) m_VerticalSpeed -= JUMP_ABORT_SPEED * Time.deltaTime;

                if (Mathf.Approximately(m_VerticalSpeed, 0f)) m_VerticalSpeed = 0f;

                m_VerticalSpeed -= gravity * Time.deltaTime;
            }
        }

        private void ForwardMovement()
        {
            var direction = new Vector3(m_Input.MoveInput.x, 0f, m_Input.MoveInput.y);
            
            if (!(direction.magnitude >= 0.1f)) return;
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_TurnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            m_CharCtrl.Move(speed * Time.deltaTime * moveDir.normalized);
        }
    }
}
