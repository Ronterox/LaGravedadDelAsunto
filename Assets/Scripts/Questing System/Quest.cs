using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Questing_System
{
    public enum QuestState
    {
        NotStarted,
        Completed,
        NeutralEnding,
        OnGoing,
        Failed
    }

    [System.Serializable]
    public struct QuestEvents
    {
        public UnityEvent onQuestCompleted;
        public UnityEvent onQuestFailed;
        public UnityEvent onQuestStarted;
    }
    
    public abstract class Quest : MonoBehaviour
    {
        public string questID;
        public QuestInfo questInfo;
        [Space] public QuestState questState = QuestState.NotStarted;

        public bool isFinalQuest;
        public bool IsCompleted => questState == QuestState.Completed;
        public bool IsFailed => questState == QuestState.Failed;
        public bool IsOnGoing => questState == QuestState.OnGoing;

        private bool m_JustStarted;

        public QuestEvents events;

        protected abstract void OnceQuestIsCompleted();

        protected abstract void OnceQuestIsFailed();

        protected abstract void OnceQuestStarted();

        public void StartQuest()
        {
            gameObject.SetActive(true);
            questState = QuestState.OnGoing;
            events.onQuestStarted?.Invoke();
            OnceQuestStarted();
            m_JustStarted = true;
        }

        public void CompleteQuest()
        {
            questState = QuestState.Completed;
            events.onQuestCompleted?.Invoke();
            OnceQuestIsCompleted();
            //increment karma, by event maybe
            QuestManager.Instance.UpdateCampaigns();
            gameObject.SetActive(false);
            m_JustStarted = false;
        }

        public void FailQuest()
        {
            questState = QuestState.Failed;
            events.onQuestFailed?.Invoke();
            OnceQuestIsFailed();
            //decrement karma, by event maybe
            QuestManager.Instance.UpdateCampaigns();
            gameObject.SetActive(false);
            m_JustStarted = false;
        }

        public bool IsJustStarted()
        {
            if (!m_JustStarted) return false;
            m_JustStarted = false;
            return true;
        }
    }
}
