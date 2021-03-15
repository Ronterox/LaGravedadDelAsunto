using UnityEngine;

namespace Questing_System
{
    [CreateAssetMenu(fileName = "New Campaign Quest", menuName = "Penguins Mafia/Campaign Quest")]
    public class CampaignQuest : ScriptableObject
    {
        [HideInInspector] public ScriptableQuest currentQuest;
        [HideInInspector] public QuestState questState = QuestState.NotStarted;

        [Header("Required")]
        public ScriptableQuest mainQuest;
        
        [Header("If you fail the quest yo go to...")]
        public ScriptableQuest badQuest;
        
        [Header("If you complete the quest yo go to...")]
        public ScriptableQuest goodQuest;

        public void UpdateState()
        {
            if (questState == QuestState.NotStarted || !currentQuest) return;

            if (!currentQuest.quest.isCompleted && !currentQuest.quest.isFailed) return;
            
            if (currentQuest.quest.isFinalQuest)
            {
                questState = QuestState.Completed;
                currentQuest = null;   
            }
            else
            {
                currentQuest = currentQuest.quest.isCompleted ? goodQuest : badQuest;
                currentQuest.quest.StartQuest();
            }
        }

        public void StartQuest() => (currentQuest = mainQuest).quest.StartQuest();
    }
}
