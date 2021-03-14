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
        float turnSmoothVelocity;

        public float gravity;

        public float maxForwardSpeed;
        private float m_ForwardSpeed, m_DesiredForwardSpeed;

        public float jumpForce;
        private float m_VerticalSpeed;

        public float maxTurnSpeed, minTurnSpeed;

        private Quaternion m_TargetRotation;

        private bool m_IsGrounded, m_CanJump;

        private const float GROUND_ACCELERATION = 10;
        private const float GROUND_DECELERATION = 10;

        private const float STICKING_GRAVITY_PROPORTION = 3;
        private const float JUMP_ABORT_SPEED = 3;

        private const float INVERSE_DIRECTION_TOLERANCE = -0.8f;
        private const float AIRBORNE_TURN_SPEED_PROPORTION = 2f;

        protected override void Awake()
        {
            base.Awake();
            m_CharCtrl = GetComponent<CharacterController>();
            m_Input = GetComponent<PlayerInput>();
        }

        private void FixedUpdate()
        {
            // CalculateForwardMovement();
            ForwardMovement();
            CalculateVerticalMovement();
           // SetTargetRotation();
           // UpdateOrientation();
        }
        private void Update()
        {
     
           
        }

        private void OnAnimatorMove()
        {
            Vector3 movement = m_ForwardSpeed * transform.forward * Time.deltaTime;

            movement += m_VerticalSpeed * Vector3.up * Time.deltaTime;

            m_CharCtrl.Move(movement);

            m_IsGrounded = m_CharCtrl.isGrounded;
        }

        private void CalculateForwardMovement()
        {
            Vector2 moveInput = m_Input.MoveInput;
            if (moveInput.sqrMagnitude > 1f) moveInput.Normalize();

            m_DesiredForwardSpeed = moveInput.magnitude * maxForwardSpeed;

            float acceleration = moveInput != Vector2.zero ? GROUND_ACCELERATION : GROUND_DECELERATION;

            m_ForwardSpeed = Mathf.MoveTowards(m_ForwardSpeed, m_DesiredForwardSpeed, acceleration * Time.deltaTime);
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
                if (!m_Input.JumpInput && m_VerticalSpeed > 0.0f)
                    // ... decrease Ellen's vertical speed.
                    // This is what causes holding jump to jump higher that tapping jump.
                    m_VerticalSpeed -= JUMP_ABORT_SPEED * Time.deltaTime;

                if (Mathf.Approximately(m_VerticalSpeed, 0f)) m_VerticalSpeed = 0f;

                m_VerticalSpeed -= gravity * Time.deltaTime;
            }
        }

        private void ForwardMovement()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                m_CharCtrl.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }

        private void SetTargetRotation()
        {
            Vector2 moveInput = m_Input.MoveInput;
            Vector3 localMovementDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

            Vector3 forward = (Quaternion.Euler(0f, mainCamera.m_XAxis.Value, 0f) * Vector3.forward).normalized;
            forward.y = 0f;

            Quaternion targetRotation = Quaternion.LookRotation(Vector3.Dot(localMovementDirection, Vector3.forward) < INVERSE_DIRECTION_TOLERANCE ? -forward :
                                                                    Quaternion.FromToRotation(Vector3.forward, localMovementDirection) * forward);
            m_TargetRotation = targetRotation;
        }

        private void UpdateOrientation()
        {
            var localInput = new Vector3(m_Input.MoveInput.x, 0f, m_Input.MoveInput.y);
            float groundedTurnSpeed = Mathf.Lerp(maxTurnSpeed, minTurnSpeed, m_ForwardSpeed / m_DesiredForwardSpeed);
            float actualTurnSpeed = m_IsGrounded ? groundedTurnSpeed : Vector3.Angle(transform.forward, localInput).ToRadians() * AIRBORNE_TURN_SPEED_PROPORTION * groundedTurnSpeed;

            transform.rotation = m_TargetRotation = Quaternion.RotateTowards(transform.rotation, m_TargetRotation, actualTurnSpeed * Time.deltaTime);
        }
    }
}
