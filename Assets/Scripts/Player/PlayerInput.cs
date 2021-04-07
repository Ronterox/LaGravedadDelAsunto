﻿using Plugins.Tools;
using UnityEngine;

namespace Player
{
    public class PlayerInput : Singleton<PlayerInput>
    {
        [Range(0f, 1.0f)]
        public float zoomSpeed = 5f;
        private float m_ScrollWheelMovement;

        public bool playerControllerInputBlocked;
        private bool m_ExternalInputBlocked;

        private Vector2 m_Movement;
        private Vector2 m_Camera;

        private bool m_Jump;
        private bool m_Interact;

        private bool m_Attack;
        private bool m_Sprint;

        private bool m_Walking;
        private bool m_Inventory;

        public Vector2 MoveInput => InputBlocked ? Vector2.zero : m_Movement;

        public Vector2 CameraInput => InputBlocked ? Vector2.zero : m_Camera;

        public bool InputBlocked => playerControllerInputBlocked || m_ExternalInputBlocked;

        public bool JumpInput => m_Jump && !InputBlocked;

        public bool Interact => m_Interact && !InputBlocked;

        public bool Inventory => m_Inventory && !InputBlocked;

        public bool Attack => m_Attack && !InputBlocked;

        public bool SprintInput => m_Sprint && !IsWalking && !InputBlocked;

        public bool IsWalking { get; private set; }

        public bool IsScrollingUp => m_ScrollWheelMovement > 0;

        public bool IsScrollingDown => m_ScrollWheelMovement < 0;

        public bool Pause;

        private void Update()
        {
            m_Movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            m_Camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            m_Jump = Input.GetButton("Jump");

            m_Interact = Input.GetButtonDown("Interact");
            m_Attack = Input.GetButtonDown("Fire1");
            Pause = Input.GetButtonDown("Pause");

            m_Sprint = Input.GetButton("Sprint");
            m_ScrollWheelMovement = Input.GetAxis("Mouse ScrollWheel");

            m_Walking = Input.GetKeyDown(KeyCode.LeftControl);
            if (m_Walking) IsWalking = !IsWalking;

            m_Inventory = Input.GetButtonDown("Inventory");
        }

        public bool HasControl() => !m_ExternalInputBlocked;

        public void BlockInput() => m_ExternalInputBlocked = true;

        public void UnlockInput() => m_ExternalInputBlocked = false;
    }
}
