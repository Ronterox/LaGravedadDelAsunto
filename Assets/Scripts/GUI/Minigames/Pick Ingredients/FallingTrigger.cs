using UnityEngine;
using UnityEngine.Events;

namespace GUI.Minigames.Pick_Ingredients
{
    public class FallingTrigger : FallingObject
    {
        public UnityEvent onTriggerEnter;

        private void OnTriggerEnter2D(Collider2D other) { if (other.CompareTag("Player")) onTriggerEnter?.Invoke(); }
    }
}
