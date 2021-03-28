using Player;
using UnityEngine;

namespace General
{
    public class Interactable : MonoBehaviour
    {
        public int numberOfInteractions = 1;
        private int interactTimes;
        private bool m_PlayerOnRange;

        public virtual void Interact() => interactTimes++; 

        private void Update()
        {
            if (m_PlayerOnRange && interactTimes < numberOfInteractions && PlayerInput.Instance.Interact) Interact();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            m_PlayerOnRange = true;
            interactTimes = 0;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            m_PlayerOnRange = false;
        }
    }
}
