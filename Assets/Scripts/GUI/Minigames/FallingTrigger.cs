using UnityEngine;
using UnityEngine.Events;

namespace GUI.Minigames
{
    public class FallingTrigger : FallingObject
    {
        public UnityEvent onTriggerEnter;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                onTriggerEnter?.Invoke();
                print("Touched Player!");
            }
        }
    }
}
