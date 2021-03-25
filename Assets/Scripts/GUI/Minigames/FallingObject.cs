using UnityEngine;

namespace GUI.Minigames
{
    public class FallingObject : MonoBehaviour
    {
        public RectTransform rectTransform;
        public float speed = 10f;
        public Vector3 direction = Vector3.down;
        private Vector3 movement;

        private void OnEnable() => movement = direction.normalized * speed;

        private void Update() => rectTransform.transform.Translate(movement);
    }
}
