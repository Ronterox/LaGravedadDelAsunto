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

        [Space] public UnityEvent onCampaignCompletedInteraction;

        public bool infiniteCompletedEventCall;
        private bool m_PlayerOnRange, m_CalledCampaignEventOnce;

        protected abstract void OnCampaignCompletedInteraction(Campaign campaign);

        protected abstract void OnInteractionRangeEnter();

        protected abstract void OnInteractionRangeExit();

        protected abstract void OnInteraction(Campaign campaign);

        private void Interact()
        {
            Campaign npcCampaign = GameManager.Instance.questManager.GetCampaign(npcScriptable.campaignID);
            if (npcCampaign.IsCompleted)
            {
                if (infiniteCompletedEventCall || !m_CalledCampaignEventOnce)
                {
                    m_CalledCampaignEventOnce = true;
                    OnCampaignCompletedInteraction(npcCampaign);
                    onCampaignCompletedInteraction?.Invoke();
                }
            }
            else if (!npcCampaign.Started) GameManager.Instance.questManager.StartNewCampaign(npcScriptable.campaignID);
            OnInteraction(npcCampaign);
        }

        private void Update()
        {
            if (m_PlayerOnRange && PlayerInput.Instance.Interact) Interact();
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
