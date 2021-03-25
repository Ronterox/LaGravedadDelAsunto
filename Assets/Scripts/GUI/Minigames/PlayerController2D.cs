using Player;
using UnityEngine;

namespace GUI.Minigames
{
    public class PlayerController2D : MonoBehaviour
    {
        public float movementSpeed = 2f;
        
        private PlayerInput m_Input;
        private Vector3 movement;
        private void Start() => m_Input = PlayerInput.Instance;

        private void Update() => transform.Translate(movement);

        private void FixedUpdate() => CalculateMovement();

        private void CalculateMovement()
        {
            movement = m_Input.MoveInput;
            movement *= movementSpeed;
        }
    }
}
