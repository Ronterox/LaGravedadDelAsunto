using Plugins.Tools;
using UnityEngine;
using UnityEngine.Events;

namespace Questing_System
{
    [System.Serializable]
    public enum QuestState
    {
        Completed,
        OnGoing,
        Failed
    }

    public readonly struct QuestCompleted
    {
        public readonly Quest quest;
        public readonly QuestState newQuestState;
        public QuestCompleted(Quest quest, QuestState questState)
        {
            this.quest = quest;
            newQuestState = questState;
        }
    }
    
    public abstract class Quest : MonoBehaviour, MMEventListener<QuestCompleted>
    {
        public CampaignQuest parentQuest;
        public QuestState questState;
        
        public UnityEvent onQuestStart;
        public UnityEvent onQuestCompleted;
        public UnityEvent onQuestFailed;

        public int karmaWon, karmaLost;
        
        public bool isFinalQuest;
        public bool isCompleted => questState == QuestState.Completed;
        public bool isFailed => questState == QuestState.Failed;

        protected abstract void OnceQuestIsCompleted();

        protected abstract void OnceQuestIsFailed();

        protected abstract void OnceQuestStarted();

        public void StartQuest()
        {
            questState = QuestState.OnGoing;
            onQuestStart.Invoke();
            OnceQuestStarted();
        }

        public void CompleteQuest()
        {
            questState = QuestState.Completed;
            onQuestCompleted.Invoke();
            OnceQuestIsCompleted();
            if(isFinalQuest) parentQuest?.CompleteCampaignQuest();
            //increment karma, by event maybe
        }

        public void FailQuest()
        {
            questState = QuestState.Failed;
            onQuestFailed.Invoke();
            OnceQuestIsFailed();
            if(isFinalQuest) parentQuest?.CompleteCampaignQuest();
            //decrement karma, by event maybe
        }

        public void OnMMEvent(QuestCompleted eventType)
        {
            if (eventType.quest != this) return;
            if(eventType.newQuestState == QuestState.Completed) CompleteQuest();
            else FailQuest();
        }
    }
}
