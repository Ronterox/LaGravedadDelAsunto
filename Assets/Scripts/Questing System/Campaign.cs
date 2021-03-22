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

        public QuestEndType campaignResult;

        private int m_BadQuestCounter, m_GoodQuestCounter;

        [Header("Final Events")]
        public UnityEvent onCampaignCompleted;

        public UnityEvent onCampaignFailed;

        public UnityEvent onCampaignNeutralEnding;

        public bool IsStarted => campaignQuests[0].IsStarted;
        public bool IsCompleted => m_CurrentQuestIndex >= campaignQuests.Length;
        public bool IsOnGoing => IsStarted && !IsCompleted;

        public void StartCampaignQuest(int index) => campaignQuests[index].StartQuest();

        public void UpdateCampaign()
        {
            if(IsCompleted) return;
            
            CampaignQuest campaignQuest = GetCurrentCampaignQuest();
            campaignQuest.UpdateState();

            if (!campaignQuest.IsCompleted) return;
            
            switch (campaignQuest.questEndType)
            {
                case QuestEndType.DoneGood: m_GoodQuestCounter++; break;
                case QuestEndType.DoneBad: m_BadQuestCounter++; break;
                case QuestEndType.NeutralEnding: break;
                default: return;
            }
            
            if (++m_CurrentQuestIndex < campaignQuests.Length) StartCampaignQuest(m_CurrentQuestIndex);
            else CompleteCampaign();
        }

        public void CompleteCampaign()
        {
            if (m_BadQuestCounter == m_GoodQuestCounter) campaignResult = QuestEndType.NeutralEnding;
            else campaignResult = m_BadQuestCounter > m_GoodQuestCounter? QuestEndType.DoneBad : QuestEndType.DoneGood;
        }

        public CampaignQuest GetCurrentCampaignQuest() => campaignQuests[IsCompleted ? campaignQuests.Length - 1 : m_CurrentQuestIndex];

        public Quest GetCurrentQuest() => campaignQuests[IsCompleted ? campaignQuests.Length - 1 : m_CurrentQuestIndex].currentQuest;
    }

}
