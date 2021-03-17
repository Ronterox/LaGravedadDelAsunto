using Managers;
using Player;
using Questing_System;
using UnityEngine;
using UnityEngine.Events;

namespace NPCs
{
    public abstract class NPC : MonoBehaviour
    {
        [Space] public ScriptableNPC npcScriptable;

        [Space] public UnityEvent onCampaignCompleted;

        private bool m_PlayerOnRange;

        protected abstract void OnCampaignCompleted();
        protected abstract void OnInteractionRangeEnter();
        protected abstract void OnInteractionRangeExit();

        public virtual void Interact()
        {
            Campaign npcCampaign = GameManager.Instance.GetCampaign(npcScriptable.campaignID);
            if (npcCampaign.IsCompleted)
            {
                OnCampaignCompleted();
                onCampaignCompleted.Invoke();
            }
            else if (!npcCampaign.Started) GameManager.Instance.StartNewCampaign(npcScriptable.campaignID);
        }

        private void Update()
        {
            if(m_PlayerOnRange && PlayerInput.Instance.Interact) Interact();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            m_PlayerOnRange = true;
            OnInteractionRangeEnter();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            m_PlayerOnRange = false;
            OnInteractionRangeExit();
        }
    }
}
