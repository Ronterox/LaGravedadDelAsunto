using Plugins.Tools;
using UnityEngine;

namespace Player
{
    public class PlayerInput : Singleton<PlayerInput>
    {
        public bool playerControllerInputBlocked;

        public float zoomSpeed = 5f;

        private float m_ScrollWheelMovement;
        
        private Vector2 m_Movement;
        private Vector2 m_Camera;

        private bool m_Jump;
        private bool m_Interact;
        
        private bool m_ExternalInputBlocked;

        public Vector2 MoveInput => playerControllerInputBlocked || m_ExternalInputBlocked ? Vector2.zero : m_Movement;

        public Vector2 CameraInput => playerControllerInputBlocked || m_ExternalInputBlocked ? Vector2.zero : m_Camera;

        public bool JumpInput => m_Jump && !playerControllerInputBlocked && !m_ExternalInputBlocked;

        public bool Interact => m_Interact && !playerControllerInputBlocked && !m_ExternalInputBlocked;

        public bool IsScrollingUp => m_ScrollWheelMovement > 0;

        public bool IsScrollingDown => m_ScrollWheelMovement < 0;

        public bool Pause { get; private set; }

        private const float k_InteractInputDuration = 0.03f;

        private void Update()
        {
            m_Movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            m_Camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            m_Jump = Input.GetButton("Jump");

            m_Interact = Input.GetButtonDown("Fire1");
            Pause = Input.GetButtonDown("Submit");

            m_ScrollWheelMovement = Input.GetAxis("Mouse ScrollWheel");
        }

        public bool HasControl() => !m_ExternalInputBlocked;

        public void BlockInput() => m_ExternalInputBlocked = true;

        public void UnlockedInput() => m_ExternalInputBlocked = false;
    }
}
