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
        private QuestState questState = QuestState.NotStarted;
        [HideInInspector]
        public QuestEndType questEndType = QuestEndType.NeutralEnding;

        public bool isFinalQuest;
        public bool IsCompleted => questState == QuestState.Completed;
        public bool IsOnGoing => questState == QuestState.OnGoing;

        private bool m_JustStarted;

        public QuestEvents events;

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
            OnceQuestStarted();
            m_JustStarted = true;
        }

        public void EndQuestPositive() => EndQuest(QuestEndType.DoneGood);
        public void EndQuestNegative() => EndQuest(QuestEndType.DoneBad);

        /// <summary>
        /// Ends the quest by passing it the ending type, and proceeds to call ending related methods
        /// </summary>
        /// <param name="endingType">Type of completion of the quest</param>
        public void EndQuest(QuestEndType endingType)
        {
            if(questState == QuestState.Completed) return;
            
            questState = QuestState.Completed;
            questEndType = endingType;
            
            if (endingType == QuestEndType.DoneGood)
            {
                events.onQuestDoneGood?.Invoke();
                OnceQuestIsDoneGood();
                GameManager.Instance.karmaController.ChangeKarma(questInfo.positiveKarma);
            }
            else
            {
                events.onQuestDoneBad?.Invoke();
                OnceQuestIsDoneBad();
                GameManager.Instance.karmaController.ChangeKarma(-questInfo.negativeKarma);
            }
            
            GameManager.Instance.questManager.UpdateCampaigns();
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
