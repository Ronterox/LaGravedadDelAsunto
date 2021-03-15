using System;
using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace NPCs
{
    public abstract class NPC : MonoBehaviour
    {
        [Space] public ScriptableNPC npcScriptable;

        [Space] public UnityEvent onCampaignCompleted;

        protected void Awake()
        {
            if(!GameManager.Instance.campaigns.Contains(npcScriptable.campaign)) GameManager.Instance.campaigns.Add(npcScriptable.campaign);
        }

        public void StartCampaign() => npcScriptable.campaign.StartCampaignQuest(0);

        protected abstract void OnCampaignCompleted();

        public virtual void Interact()
        {
            if (npcScriptable.campaign.IsCompleted)
            {
                OnCampaignCompleted();
                onCampaignCompleted.Invoke();
            }
            else if (!npcScriptable.campaign.started) StartCampaign();
        }
    }
}
