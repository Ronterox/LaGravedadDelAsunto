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

        protected abstract void OnCampaignCompleted(Campaign campaign);

        protected abstract void OnInteractionRangeEnter();

        protected abstract void OnInteractionRangeExit();

        protected abstract void OnInteraction(QuestState lastQuestState, Quest quest);

        private void Interact()
        {
            Campaign npcCampaign = QuestManager.Instance.GetCampaign(npcScriptable.campaignID);
            Quest currentQuest = npcCampaign.GetCurrentQuest();
            QuestState lastQuestState = currentQuest.questState;
            if (npcCampaign.IsCompleted)
            {
                OnCampaignCompleted(npcCampaign);
                onCampaignCompleted.Invoke();
            }
            else if (!npcCampaign.Started) QuestManager.Instance.StartNewCampaign(npcScriptable.campaignID);
            OnInteraction(lastQuestState, currentQuest);
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
