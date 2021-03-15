using UnityEngine;
using UnityEngine.Events;

namespace Questing_System
{
    public enum QuestState
    {
        NotStarted,
        Completed,
        OnGoing,
        Failed
    }
    
    public abstract class Quest : MonoBehaviour
    {
        [Space] public QuestState questState = QuestState.NotStarted;

        public bool isFinalQuest;
        public bool isCompleted => questState == QuestState.Completed;
        public bool isFailed => questState == QuestState.Failed;

        [Header("Events")]
        public UnityEvent onQuestCompleted;
        public UnityEvent onQuestFailed;
        public UnityEvent onQuestStarted;

        [Header("Quest Rewards")]
        public int positiveKarma;
        public int negativeKarma;

        protected abstract void OnceQuestIsCompleted();

        protected abstract void OnceQuestIsFailed();

        protected abstract void OnceQuestStarted();

        public void StartQuest()
        {
            questState = QuestState.OnGoing;
            onQuestCompleted?.Invoke();
            OnceQuestStarted();
        }

        public void CompleteQuest()
        {
            questState = QuestState.Completed;
            onQuestFailed?.Invoke();
            OnceQuestIsCompleted();
            //increment karma, by event maybe
        }

        public void FailQuest()
        {
            questState = QuestState.Failed;
            onQuestStarted?.Invoke();
            OnceQuestIsFailed();
            //decrement karma, by event maybe
        }
    }
}
