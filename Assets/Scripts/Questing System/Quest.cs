namespace Questing_System
{
    public enum QuestState
    {
        Completed,
        OnGoing,
        Failed,
        NotStarted
    }
    
    public abstract class Quest
    {
        public QuestState questState = QuestState.NotStarted;

        public bool isFinalQuest;
        public bool isCompleted => questState == QuestState.Completed;
        public bool isFailed => questState == QuestState.Failed;

        protected abstract void OnceQuestIsCompleted();

        protected abstract void OnceQuestIsFailed();

        protected abstract void OnceQuestStarted();

        public void StartQuest()
        {
            questState = QuestState.OnGoing;
            OnceQuestStarted();
        }

        public void CompleteQuest()
        {
            questState = QuestState.Completed;
            OnceQuestIsCompleted();
            //increment karma, by event maybe
        }

        public void FailQuest()
        {
            questState = QuestState.Failed;
            OnceQuestIsFailed();
            //decrement karma, by event maybe
        }
    }
}
