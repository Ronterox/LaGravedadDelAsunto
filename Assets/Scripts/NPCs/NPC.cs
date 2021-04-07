using General.Utilities;
using Managers;
using Questing_System;
using UnityEngine;
using UnityEngine.Events;

namespace NPCs
{
    public abstract class NPC : Interactable
    {
        [Space] public ScriptableNPC npcScriptable;

        [Space] public UnityEvent onCampaignCompletedInteraction;

        public Transform textPosition;

        public bool infiniteCompletedEventCall;
        private bool m_CalledCampaignEventOnce;

        protected abstract void OnCampaignCompletedInteraction(Campaign campaign);

        protected abstract void OnInteractionRangeEnter(Campaign campaign);

        protected abstract void OnInteractionRangeExit(Campaign campaign);

        protected abstract void OnInteraction(Campaign campaign);

        protected override void OnEnterTrigger(Collider other) => OnInteractionRangeEnter(GameManager.Instance.questManager.GetCampaign(npcScriptable.campaignID));

        protected override void OnExitTrigger(Collider other) => OnInteractionRangeExit(GameManager.Instance.questManager.GetCampaign(npcScriptable.campaignID));

        public override void Interact()
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
            else if (!npcCampaign.IsStarted) GameManager.Instance.questManager.StartNewCampaign(npcScriptable.campaignID);
            OnInteraction(npcCampaign);
        }

        public void Say(string dialogueID) => GameManager.Instance.dialogueManager.Type(npcScriptable.GetDialogue(dialogueID).line, textPosition.position);
    }
}
