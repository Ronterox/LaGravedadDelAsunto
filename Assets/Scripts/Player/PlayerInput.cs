using Plugins.Tools;
using UnityEngine;

namespace Player
{
    public class PlayerInput : Singleton<PlayerInput>
    {
        [Range(0f, 1.0f)]
        public float zoomSpeed = 5f;
        private float m_ScrollWheelMovement;

        private Vector2 m_Movement;
        private Vector2 m_Camera;

        private bool m_Jump;
        private bool m_Interact;

        private bool m_Attack;
        private bool m_Sprint;

        private bool m_Walking;
        private bool m_Inventory;

        private bool m_Map;

        public Vector2 MoveInput => InputBlocked ? Vector2.zero : m_Movement;

        public Vector2 CameraInput => InputBlocked ? Vector2.zero : m_Camera;

        public bool InputBlocked { get; private set; }

        public bool JumpInput => m_Jump && !InputBlocked;

        public bool Interact => m_Interact && !InputBlocked;

        public bool Inventory => m_Inventory && !InputBlocked;

        public bool Attack => m_Attack && !InputBlocked;

        public bool SprintInput => m_Sprint && !IsWalking && !InputBlocked;

        public bool IsWalking { get; private set; }

        public bool IsScrollingUp => m_ScrollWheelMovement > 0;

        public bool IsScrollingDown => m_ScrollWheelMovement < 0;
        public bool Map => m_Map && !InputBlocked;
        
        public bool Pause { get; private set; }

        private const string INPUT_HORIZONTAL = "Horizontal";
        private const string INPUT_VERTICAL = "Vertical";
        private const string INPUT_MOUSE_X = "Mouse X";
        private const string INPUT_MOUSE_Y = "Mouse Y";
        private const string INPUT_JUMP = "Jump";
        private const string INPUT_INTERACT = "Interact";
        private const string INPUT_FIRE_1 = "Fire1";
        private const string INPUT_PAUSE = "Pause";
        private const string INPUT_SPRINT = "Sprint";
        private const string INPUT_SCROLLWHEEL = "Mouse ScrollWheel";
        private const string INPUT_INVENTORY = "Inventory";
        private const string INPUT_MAP = "Map";

        private void Update()
        {
            m_Movement.Set(Input.GetAxis(INPUT_HORIZONTAL), Input.GetAxis(INPUT_VERTICAL));
            m_Camera.Set(Input.GetAxis(INPUT_MOUSE_X), Input.GetAxis(INPUT_MOUSE_Y));
            m_Jump = Input.GetButton(INPUT_JUMP);

            m_Interact = Input.GetButtonDown(INPUT_INTERACT);
            m_Attack = Input.GetButtonDown(INPUT_FIRE_1);
            Pause = Input.GetButtonDown(INPUT_PAUSE);

            m_Sprint = Input.GetButton(INPUT_SPRINT);
            m_ScrollWheelMovement = Input.GetAxis(INPUT_SCROLLWHEEL);

            m_Walking = Input.GetKeyDown(KeyCode.LeftControl);
            if (m_Walking) IsWalking = !IsWalking;

            m_Inventory = Input.GetButtonDown(INPUT_INVENTORY);
            m_Map = Input.GetButtonDown(INPUT_MAP);
        }

        public bool HasControl() => !InputBlocked;

        public void BlockInput() => InputBlocked = true;

        public void UnlockInput() => InputBlocked = false;
    }
}
