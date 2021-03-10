using UnityEngine;
using UnityEngine.Events;

namespace Questing_System
{
    public enum QuestState
    {
        Completed,
        OnGoing,
        Failed
    }
    public abstract class Quest : MonoBehaviour
    {
        public Quest parentQuest;
        public QuestState questState;
        
        public UnityEvent onQuestStart;
        public UnityEvent onQuestCompleted;
        public UnityEvent onQuestFailed;

        public int karmaWon, karmaLost;
        
        public bool isFinalQuest;

        public bool isCompleted => questState == QuestState.Completed;
        public bool isFailed => questState == QuestState.Failed;
        public abstract void OnceQuestIsCompleted();
        public abstract void OnceQuestIsFailed();
        public abstract void OnceQuestStarted();

        public void StartQuest()
        {
            questState = QuestState.OnGoing;
            onQuestStart.Invoke();
            OnceQuestIsCompleted();
        }

        public void CompleteQuest()
        {
            questState = QuestState.Completed;
            onQuestCompleted.Invoke();
            OnceQuestStarted();
            if(isFinalQuest && parentQuest) parentQuest.CompleteQuest();
            //increment karma, by event maybe
        }

        public void FailQuest()
        {
            questState = QuestState.Failed;
            onQuestFailed.Invoke();
            OnceQuestIsFailed();
            if(isFinalQuest && parentQuest) parentQuest.FailQuest();
            //decrement karma, by event maybe
        }
    }
}
