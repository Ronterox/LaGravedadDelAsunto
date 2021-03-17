using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Questing_System
{
    [System.Serializable]
    public class Campaign
    {
        public string id;
        [Space] public CampaignQuest[] campaignQuests;
        private int m_CurrentQuestIndex;

        public QuestState campaignResult;

        private int m_FailedCounter, m_CompletedCounter;

        [Header("Final Events")]
        public UnityEvent onCampaignCompleted;

        public UnityEvent onCampaignFailed;

        public UnityEvent onCampaignNeutralEnding;

        public bool Started => campaignQuests[0].questState != QuestState.NotStarted;
        public bool IsCompleted => m_CurrentQuestIndex >= campaignQuests.Length;
        public bool IsOnGoing => Started && !IsCompleted;

        public void StartCampaignQuest(int index) => campaignQuests[index].StartQuest();

        public void UpdateCampaign()
        {
            CampaignQuest campaignQuest = GetCurrentCampaignQuest();
            campaignQuest.UpdateState();

            if (campaignQuest.questState == QuestState.Completed) m_CompletedCounter++;
            else if (campaignQuest.questState == QuestState.Failed) m_FailedCounter++;
            else return;

            if (++m_CurrentQuestIndex < campaignQuests.Length) StartCampaignQuest(m_CurrentQuestIndex);
            else CompleteCampaign();
        }

        public void CompleteCampaign()
        {
            Debug.Log(m_FailedCounter + " and " + m_CompletedCounter);
            if (m_FailedCounter == m_CompletedCounter) campaignResult = QuestState.NeutralEnding;
            else if (m_FailedCounter > m_CompletedCounter) campaignResult = QuestState.Failed;
            else campaignResult = QuestState.Completed;
            QuestManager.Instance.onGoingCampaigns.Remove(this);
        }

        public CampaignQuest GetCurrentCampaignQuest() => campaignQuests[IsCompleted ? campaignQuests.Length - 1 : m_CurrentQuestIndex];

        public Quest GetCurrentQuest() => campaignQuests[IsCompleted ? campaignQuests.Length - 1 : m_CurrentQuestIndex].currentQuest;
    }

}
