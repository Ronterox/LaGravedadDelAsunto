using System.Collections.Generic;
using UnityEngine;

namespace Questing_System
{
    public class Campaign : MonoBehaviour
    {
        public List<CampaignQuest> campaignQuests;
        private int m_CurrentQuestIndex;
        
        public bool started;
        public bool isCompleted => m_CurrentQuestIndex >= campaignQuests.Count;
        public bool isOnGoing => started && !isCompleted;

        public void StartCampaignQuest() => campaignQuests[m_CurrentQuestIndex].StartQuest();

        //Maybe call this by an quest completed event
        public void UpdateCampaign()
        {
            Quest currentQuest = campaignQuests[m_CurrentQuestIndex];

            if (!currentQuest.isCompleted) return;
            m_CurrentQuestIndex++;

            if (m_CurrentQuestIndex < campaignQuests.Count) StartCampaignQuest();
            else CompleteCampaign();
        }

        public void CompleteCampaign() { }
    }

}
