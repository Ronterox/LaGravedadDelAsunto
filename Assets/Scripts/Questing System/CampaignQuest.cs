using UnityEngine;

namespace Questing_System
{
    [System.Serializable]
    public class CampaignQuest
    {
        public Quest currentQuest;
        private QuestState questState = QuestState.NotStarted;
        public QuestEndType questEndType = QuestEndType.NeutralEnding;

        [Header("Required")]
        public Quest mainQuest;

        [Header("If you fail the quest you go to...")]
        public Quest badQuest;

        [Header("If you complete the quest you go to...")]
        public Quest goodQuest;

        private int m_DoneBadCounter, m_DoneGoodCounter;

        public bool IsCompleted => questState == QuestState.Completed;

        public bool IsStarted => questState != QuestState.NotStarted;

        public void UpdateState()
        {
            if (questState == QuestState.Completed) return;

            switch (currentQuest.questEndType)
            {
                case QuestEndType.DoneGood:
                    m_DoneGoodCounter++;
                    AssignNextQuest(goodQuest);
                    break;
                case QuestEndType.DoneBad:
                    m_DoneBadCounter++;
                    AssignNextQuest(badQuest);
                    break;
                case QuestEndType.NeutralEnding: break;
                default: return;
            }
        }

        private void AssignNextQuest(Quest nextQuest)
        {
            if (mainQuest.isFinalQuest) UpdateStateQuest();
            else if ((mainQuest = nextQuest).startsInstantly) mainQuest.StartQuest();
        }

        private void UpdateStateQuest()
        {
            if (m_DoneBadCounter == m_DoneGoodCounter) questEndType = QuestEndType.NeutralEnding;
            else questEndType = m_DoneGoodCounter > m_DoneBadCounter ? QuestEndType.DoneGood : QuestEndType.DoneBad;
            questState = QuestState.Completed;
        }

        public void StartQuest()
        {
            (currentQuest = mainQuest).StartQuest();
            questState = QuestState.OnGoing;
        }
    }
}
