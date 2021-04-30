using Player;
using Plugins.Tools;
using UnityEngine;

namespace General.Utilities
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Interactable : MonoBehaviour
    {
        [Header("Interaction Settings")]
        public int numberOfInteractionsOnPlace = 1;
        public bool infiniteInteractions;

        protected int m_InteractTimes;

        public delegate void OnInteractionEvent(Interactable interactable);
        public event OnInteractionEvent onInteraction;

        public bool IsPlayerOnRange { get; private set; }

        protected virtual void Awake() => gameObject.MoveToScene("Interactables Scene");

        public abstract void Interact();

        private void IncrementInteraction()
        {
            if (infiniteInteractions || m_InteractTimes++ < numberOfInteractionsOnPlace)
            {
                onInteraction?.Invoke(this);
                Interact();
            }
        }
        
        protected abstract void OnEnterTrigger(Collider other);

        protected abstract void OnExitTrigger(Collider other);

        protected virtual void Update()
        {
            if (IsPlayerOnRange && PlayerInput.Instance.Interact) IncrementInteraction();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            IsPlayerOnRange = true;
            m_InteractTimes = 0;
            OnEnterTrigger(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            IsPlayerOnRange = false;
            OnExitTrigger(other);
        }
    }
}
