using UnityEngine;

namespace Questing_System
{
    [System.Serializable]
    public class Campaign
    {
        [Space] public CampaignQuest[] campaignQuests;
        private int m_CurrentQuestIndex;

        [HideInInspector] public bool started;
        public bool IsCompleted => m_CurrentQuestIndex >= campaignQuests.Length;
        public bool IsOnGoing => started && !IsCompleted;

        public void StartCampaignQuest(int index) => campaignQuests[index].StartQuest();

        //Maybe call this by an quest completed event
        public void UpdateCampaign()
        {
            CampaignQuest currentCampaignQuest = campaignQuests[m_CurrentQuestIndex];
            
            currentCampaignQuest.UpdateState();
            
            if (currentCampaignQuest.questState != QuestState.Completed) return;

            if (++m_CurrentQuestIndex < campaignQuests.Length) StartCampaignQuest(m_CurrentQuestIndex);
            else CompleteCampaign();
        }

        public void CompleteCampaign() { }

        public Quest GetCurrentQuest() => campaignQuests[m_CurrentQuestIndex].currentQuest;
    }

}
