using Managers;
using UnityEngine;

namespace Questing_System
{
    [System.Serializable]
    public class Campaign
    {
        public string id;
        [Space] public CampaignQuest[] campaignQuests;
        private int m_CurrentQuestIndex;

        public QuestState campaignResult;

        //TODO: count failed missions and completed ones, to guess the result of the campaign
        private int m_FailedCounter, m_CompletedCounter;

        public bool Started => campaignQuests[0].questState != QuestState.NotStarted;
        public bool IsCompleted => m_CurrentQuestIndex >= campaignQuests.Length;
        public bool IsOnGoing => Started && !IsCompleted;

        public void StartCampaignQuest(int index) => campaignQuests[index].StartQuest();

        public void UpdateCampaign()
        {
            CampaignQuest currentCampaignQuest = campaignQuests[m_CurrentQuestIndex];

            currentCampaignQuest.UpdateState();

            if (currentCampaignQuest.questState != QuestState.Completed && currentCampaignQuest.questState != QuestState.Failed) return;

            if (++m_CurrentQuestIndex < campaignQuests.Length) StartCampaignQuest(m_CurrentQuestIndex);
            else CompleteCampaign();
        }

        public void CompleteCampaign()
        {
            //Read upper TODO to change this
            //campaignResult = campaignQuests[m_CurrentQuestIndex].questState;
            QuestManager.Instance.onGoingCampaigns.Remove(this);
        }

        public Quest GetCurrentQuest() => campaignQuests[IsCompleted ? campaignQuests.Length - 1 : m_CurrentQuestIndex].currentQuest;
    }

}
