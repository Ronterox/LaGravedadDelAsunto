using Player;
using Plugins.Tools;
using UnityEngine;

namespace General.Utilities
{
    public abstract class Interactable : MonoBehaviour
    {
        public int numberOfInteractionsOnPlace = 1;
        public bool infiniteInteractions;

        protected int m_InteractTimes;

        public bool IsPlayerOnRange { get; private set; }

        protected virtual void Awake() => gameObject.MoveToScene("Interactables Scene");

        public abstract void Interact();

        private void IncrementInteraction()
        {
            if (infiniteInteractions || m_InteractTimes++ < numberOfInteractionsOnPlace) Interact();
        }
        
        //Fix parameters of inspector, i don't like interactions on place name

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
