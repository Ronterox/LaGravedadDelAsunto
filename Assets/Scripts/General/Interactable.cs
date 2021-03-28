using Player;
using UnityEngine;

namespace General
{
    public abstract class Interactable : MonoBehaviour
    {
        public int numberOfInteractions = 1;
        private int interactTimes;
        private bool m_PlayerOnRange;

        public abstract void Interact();

        private void IncrementInteraction()
        {
            interactTimes++;
            Interact();
        }

        protected abstract void OnEnterTrigger(Collider other);

        protected abstract void OnExitTrigger(Collider other);

        private void Update()
        {
            if (m_PlayerOnRange && interactTimes < numberOfInteractions && PlayerInput.Instance.Interact) IncrementInteraction();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            m_PlayerOnRange = true;
            interactTimes = 0;
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
