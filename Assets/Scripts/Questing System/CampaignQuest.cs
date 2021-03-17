using UnityEngine;

namespace Questing_System
{
    [System.Serializable]
    public class CampaignQuest
    {
        public Quest currentQuest;
        public QuestState questState = QuestState.NotStarted;

        [Header("Required")]
        public Quest mainQuest;

        [Header("If you fail the quest you go to...")]
        public Quest badQuest;

        [Header("If you complete the quest you go to...")]
        public Quest goodQuest;

        private int m_FailedCounter, m_CompletedCounter;

        public void UpdateState()
        {
            if (questState != QuestState.NotStarted || questState != QuestState.OnGoing) return;

            if (currentQuest.isFinalQuest)
            {
                if (currentQuest.IsCompleted) m_CompletedCounter++;
                else m_FailedCounter++;
                
                if (m_FailedCounter == m_CompletedCounter) questState = QuestState.NeutralEnding;
                else questState = m_CompletedCounter > m_FailedCounter ? QuestState.Completed : QuestState.Failed;
                return;
            }

            if (currentQuest.IsCompleted)
            {
                currentQuest = goodQuest;
                m_CompletedCounter++;
            }
            else
            {
                currentQuest = badQuest;
                m_FailedCounter++;
            }
            currentQuest.StartQuest();
        }

        public void StartQuest()
        {
            (currentQuest = mainQuest).StartQuest();
            questState = QuestState.OnGoing;
        }
    }
}
