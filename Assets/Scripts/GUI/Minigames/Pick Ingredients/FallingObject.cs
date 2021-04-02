using UnityEngine;

namespace GUI.Minigames.Pick_Ingredients
{
    public class FallingObject : MonoBehaviour
    {
        public float speed = 10f;
        public Vector3 direction = Vector3.down;
        private Vector3 movement;

        protected virtual void OnEnable() => movement = direction.normalized * speed;

        private void Update() => transform.Translate(movement);
    }
}
