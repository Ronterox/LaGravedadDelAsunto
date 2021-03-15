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
        
        [Header("If you fail the quest yo go to...")]
        public Quest badQuest;
        
        [Header("If you complete the quest yo go to...")]
        public Quest goodQuest;

        public void UpdateState()
        {
            if (questState == QuestState.NotStarted || !currentQuest) return;

            if (!currentQuest.isCompleted && !currentQuest.isFailed) return;
            
            if (currentQuest.isFinalQuest)
            {
                questState = QuestState.Completed;
                currentQuest = null;   
            }
            else
            {
                currentQuest = currentQuest.isCompleted ? goodQuest : badQuest;
                currentQuest.StartQuest();
            }
        }

        public void StartQuest()
        {
            (currentQuest = mainQuest).StartQuest();
            questState = QuestState.OnGoing;
        }
    }
}
