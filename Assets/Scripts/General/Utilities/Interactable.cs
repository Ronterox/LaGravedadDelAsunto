using Player;
using UnityEngine;

namespace General.Utilities
{
    public abstract class Interactable : MonoBehaviour
    {
        public int numberOfInteractions = 1;
        public bool infiniteInteractions;

        private int m_InteractTimes;
        private bool m_PlayerOnRange;

        public abstract void Interact();

        private void IncrementInteraction()
        {
            if (infiniteInteractions || m_InteractTimes++ < numberOfInteractions) Interact();
        }

        protected abstract void OnEnterTrigger(Collider other);

        protected abstract void OnExitTrigger(Collider other);

        protected virtual void Update()
        {
            if (m_PlayerOnRange && PlayerInput.Instance.Interact) IncrementInteraction();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            m_PlayerOnRange = true;
            m_InteractTimes = 0;
            OnEnterTrigger(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            m_PlayerOnRange = false;
            OnExitTrigger(other);
        }
    }
}