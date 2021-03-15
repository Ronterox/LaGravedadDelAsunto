using Plugins.Tools;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Requirements")]
        public Transform mainCamera;

        private PlayerInput m_Input;
        private CharacterController m_CharCtrl;

        [Header("Movement")]
        public float speed = 6f;
        
        public float jumpForce;
        private float m_VerticalSpeed;
        
        public float gravity;

        [Header("Rotation")]
        public float turnSmoothTime = 0.1f;
        private float m_TurnSmoothVelocity;

        private bool m_IsGrounded, m_CanJump;

        private const float STICKING_GRAVITY_PROPORTION = 3;
        private const float JUMP_ABORT_SPEED = 10;

        private void Awake()
        {
            
            m_CharCtrl = GetComponent<CharacterController>();
            m_Input = GetComponent<PlayerInput>();
        }

        private void FixedUpdate()
        {
            CalculateForwardMovement();
            CalculateVerticalMovement();
        }

        private void OnAnimatorMove()
        {
            m_CharCtrl.Move(m_VerticalSpeed * Time.deltaTime * Vector3.up);

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

        private void CalculateForwardMovement()
        {
            if (m_Input.MoveInput == Vector2.zero) return;

            float targetAngle = Mathf.Atan2(m_Input.MoveInput.x, m_Input.MoveInput.y).ToRadians() + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_TurnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            m_CharCtrl.Move(speed * Time.deltaTime * moveDir);
        }
    }
}
