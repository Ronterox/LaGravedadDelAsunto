using Plugins.Tools;
using Questing_System;
using UnityEngine;
using UnityEngine.Events;

namespace NPCs
{
    public struct NPCRequestCompleted
    {
        public Quest quest;
        public QuestState questState;

        public NPCRequestCompleted(Quest quest, QuestState questState)
        {
            this.quest = quest;
            this.questState = questState;
        }
    }

    public abstract class NPC : MonoBehaviour, MMEventListener<NPCRequestCompleted>
    {
        [Header("NPC")] public string m_NpcName;
        [Space][TextArea] public string m_Description;
        
        [Space] public Campaign campaign;

        [Space] public UnityEvent onCampaignCompleted;

        protected NPCRequestCompleted lastQuest;

        public void StartCampaign() => campaign.StartCampaignQuest(0);

        protected abstract void OnCampaignCompleted();

        public virtual void Interact()
        {
            if (campaign.isCompleted)
            {
                OnCampaignCompleted();
                onCampaignCompleted.Invoke();
            }
            else if (!campaign.started) StartCampaign();
            else if (lastQuest.quest)
            {
                MMEventManager.TriggerEvent(new QuestCompleted(lastQuest.quest, lastQuest.questState));
                lastQuest.quest = null;
            }
        }

        public void OnMMEvent(NPCRequestCompleted eventType) => lastQuest = eventType;
    }
}
