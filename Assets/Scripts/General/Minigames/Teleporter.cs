using UnityEngine;

namespace General.Minigames
{
    public class Teleporter : MonoBehaviour
    {
        public Transform targetPosition;

        private void OnTriggerEnter2D(Collider2D other) => other.transform.position = targetPosition.position;
    }
}
