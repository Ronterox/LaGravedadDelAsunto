using System.Collections.Generic;

namespace Questing_System
{
    [System.Serializable]
    public class Campaign
    {
        public List<CampaignQuest> campaignQuests;
        private int m_CurrentQuestIndex;
        
        public bool started;
        public bool isCompleted => m_CurrentQuestIndex >= campaignQuests.Count;
        public bool isOnGoing => started && !isCompleted;

        public void StartCampaignQuest(int index)
        {
            campaignQuests.ForEach(x => x.campaign = this);
            campaignQuests[index].StartQuest();
        }

        //Maybe call this by an quest completed event
        public void UpdateCampaign()
        { 
            m_CurrentQuestIndex++;

            if (m_CurrentQuestIndex < campaignQuests.Count) StartCampaignQuest(m_CurrentQuestIndex);
            else CompleteCampaign();
        }

        public void CompleteCampaign() { }

        public Quest GetCurrentQuest() => campaignQuests[m_CurrentQuestIndex].currentQuest;
    }

}
