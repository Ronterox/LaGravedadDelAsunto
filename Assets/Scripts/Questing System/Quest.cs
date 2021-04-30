using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Questing_System
{
    public enum QuestState
    {
        NotStarted,
        OnGoing,
        Completed
    }

    public enum QuestEndType
    {
        DoneGood,
        DoneBad,
        NeutralEnding
    }

    /// <summary>
    /// Group of unity event methods
    /// </summary>
    [System.Serializable]
    public struct QuestEvents
    {
        public UnityEvent onQuestDoneGood;
        public UnityEvent onQuestDoneBad;
        public UnityEvent onQuestStarted;
    }
    
    public abstract class Quest : MonoBehaviour
    {
        public string questID;
        public QuestInfo questInfo;
        [Space] 
        [HideInInspector]
        public QuestState questState = QuestState.NotStarted;
        [HideInInspector]
        public QuestEndType questEndType = QuestEndType.NeutralEnding;

        public bool IsCompleted => questState == QuestState.Completed;
        public bool IsOnGoing => questState == QuestState.OnGoing;
        public bool IsStarted => questState != QuestState.NotStarted;

        private bool m_JustStarted;

        public QuestEvents events;

        protected virtual void OnEnable()
        {
            events.onQuestStarted.AddListener(OnceQuestStarted);
            events.onQuestDoneBad.AddListener(OnceQuestIsDoneBad);
            events.onQuestDoneGood.AddListener(OnceQuestIsDoneGood);
        }

        protected virtual void OnDisable()
        {
            events.onQuestStarted.RemoveListener(OnceQuestStarted);
            events.onQuestDoneBad.RemoveListener(OnceQuestIsDoneBad);
            events.onQuestDoneGood.RemoveListener(OnceQuestIsDoneGood);
        }

        /// <summary>
        /// Called once quest completed with DoneGood Ending
        /// </summary>
        protected abstract void OnceQuestIsDoneGood();

        /// <summary>
        /// Called once quest completed with DoneBad Ending
        /// </summary>
        protected abstract void OnceQuestIsDoneBad();

        /// <summary>
        /// Called at the start of the quest
        /// </summary>
        protected abstract void OnceQuestStarted();

        /// <summary>
        /// Starts the quest and initializes the corresponding starting methods
        /// </summary>
        public void StartQuest()
        {
            gameObject.SetActive(true);
            questState = QuestState.OnGoing;
            events.onQuestStarted?.Invoke();
            m_JustStarted = true;
        }

        /// <summary>
        /// Ends the quest with a being good ending
        /// </summary>
        public void EndQuestPositive() => EndQuest(QuestEndType.DoneGood);
        
        /// <summary>
        /// Ends the quest with a being bad ending
        /// </summary>
        public void EndQuestNegative() => EndQuest(QuestEndType.DoneBad);

        /// <summary>
        /// Ends the quest by passing it the ending type, and proceeds to call ending related methods
        /// </summary>
        /// <param name="endingType">type of completion of the quest</param>
        public void EndQuest(QuestEndType endingType)
        {
            if(questState == QuestState.Completed) return;
            
            questState = QuestState.Completed;
            questEndType = endingType;
            
            if (endingType == QuestEndType.DoneGood)
            {
                events.onQuestDoneGood?.Invoke();
                GameManager.Instance.karmaController.ChangeKarma(questInfo.positiveKarma);
            }
            else
            {
                events.onQuestDoneBad?.Invoke();
                GameManager.Instance.karmaController.ChangeKarma(-questInfo.negativeKarma);
            }
            
            GameManager.Instance.questManager.UpdateQuests();
            ArchievementsManager.Instance.UpdateAchievement("achievement2", 1);
            gameObject.SetActive(false);
            m_JustStarted = false;
        }

        /// <summary>
        /// Checks if the quest just started, and changes the just started boolean to false
        /// </summary>
        /// <returns></returns>
        public bool IsJustStarted()
        {
            if (!m_JustStarted) return false;
            m_JustStarted = false;
            return true;
        }
    }
}
