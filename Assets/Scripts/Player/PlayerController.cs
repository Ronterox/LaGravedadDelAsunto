using Plugins.Tools;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : Singleton<PlayerController>
    {
        [Header("Requirements")]
        public Transform mainCamera;

        private PlayerInput m_Input;
        private CharacterController m_CharCtrl;

        private Animator m_Animator;

        [Header("Movement")]
        public float speed = 6f;
        public float sprintMultiplier = 2f;

        public float jumpForce;
        private float m_VerticalSpeed;

        public float gravity;

        [Header("Rotation")]
        public float turnSmoothTime = 0.1f;
        private float m_TurnSmoothVelocity;

        private bool m_IsGrounded, m_CanJump, m_WasSprinting;

        private readonly int SPEED_ANIMATION_HASH = Animator.StringToHash("Speed");
        private readonly int JUMP_ANIMATION_HASH = Animator.StringToHash("Jump");
        private readonly int FALLING_ANIMATION_HASH = Animator.StringToHash("IsFalling");
        readonly int m_HashStateTime = Animator.StringToHash("StateTime");

        private const float STICKING_GRAVITY_PROPORTION = 3;
        private const float JUMP_ABORT_SPEED = 10;

        private bool IsMoving => m_Input.MoveInput != Vector2.zero;

        private bool m_MovementBlocked;

        protected override void Awake()
        {
            base.Awake();
            m_CharCtrl = GetComponent<CharacterController>();
            m_Input = GetComponent<PlayerInput>();
            m_Animator = GetComponent<Animator>();
        }

        public void BlockMovement(bool blockMovement) => m_MovementBlocked = blockMovement;

        private void FixedUpdate()
        {
            m_Animator.SetFloat(m_HashStateTime, Mathf.Repeat(m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f));
            if (m_MovementBlocked) return;
           
            AnimatePlayer();
            SetRotation();
            CalculateVerticalMovement();
        }

        private void OnAnimatorMove()
        {
            if(m_MovementBlocked) return;
            
            float stateSpeed = m_Input.IsWalking && m_IsGrounded ? speed * .5f :
                m_Input.SprintInput && m_IsGrounded || m_WasSprinting ? speed * sprintMultiplier : speed;

            Vector3 movement = IsMoving ? Time.deltaTime * stateSpeed * transform.forward : Vector3.zero;
            movement += m_VerticalSpeed * Time.deltaTime * Vector3.up;

            m_CharCtrl.Move(movement);

            m_IsGrounded = m_CharCtrl.isGrounded;
        }

        private void CalculateVerticalMovement()
        {
            if (!m_Input.JumpInput && m_IsGrounded) m_CanJump = true;

            if (m_IsGrounded)
            {
                if (m_WasSprinting) m_WasSprinting = false;

                m_VerticalSpeed = -gravity * STICKING_GRAVITY_PROPORTION;

                if (!m_Input.JumpInput || !m_CanJump) return;
                m_WasSprinting = m_Input.SprintInput;
                m_VerticalSpeed = jumpForce;
                m_IsGrounded = false;
                m_CanJump = false;
            }
            else
            {
                if (!m_Input.JumpInput && m_VerticalSpeed > 0) m_VerticalSpeed -= JUMP_ABORT_SPEED * Time.deltaTime;
                m_VerticalSpeed -= gravity * Time.deltaTime;
            }
        }

        private void SetRotation()
        {
            if (!IsMoving) return;

            float targetAngle = Mathf.Atan2(m_Input.MoveInput.x, m_Input.MoveInput.y).ToRadians() + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_TurnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        private void AnimatePlayer()
        {
            
            if (m_IsGrounded)
            {
                if (IsMoving)
                {
                    if (m_Input.IsWalking) m_Animator.SetFloat(SPEED_ANIMATION_HASH, 0.5f, 0.15f, Time.deltaTime);
                    else m_Animator.SetFloat(SPEED_ANIMATION_HASH, m_Input.SprintInput ? 1.5f : 1f, 0.15f, Time.deltaTime);
                }
                else m_Animator.SetFloat(SPEED_ANIMATION_HASH, 0, 0.15f, Time.deltaTime);

                if (m_CanJump && m_Input.JumpInput) m_Animator.SetTrigger(JUMP_ANIMATION_HASH);
            }
            m_Animator.SetBool(FALLING_ANIMATION_HASH, !m_IsGrounded);
        }
    }
}
