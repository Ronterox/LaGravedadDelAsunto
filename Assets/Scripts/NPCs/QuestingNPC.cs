using Managers;
using Questing_System;

namespace NPCs
{

    public class QuestingNPC : NPC
    {
        protected override void OnQuestCompletedInteraction(Quest quest) { }

        protected override void OnInteractionRangeEnter(Quest quest) => SayQuestRelatedDialogue(quest);

        protected override void OnInteractionRangeExit(Quest quest) => SayQuestRelatedDialogue(quest);

        protected override void OnInteraction(Quest quest) => CheckForAction(SayQuestRelatedDialogue(quest).action);

        private QuestDialogueID SayQuestRelatedDialogue(Quest quest)
        {
            QuestDialogueID questDialogueID = GetQuestDialogueID(quest.questState);

            if(questDialogueID.dialogueID != null) Say(questDialogueID.dialogueID);
            
            return questDialogueID;
        }
        
        public QuestDialogueID GetQuestDialogueID(QuestState questState)
        {
            int interactions = m_InteractTimes > 0? m_InteractTimes - 1 : 0;
            return questState switch
            {
                QuestState.NotStarted => interactions < notStartedDialogues.Length ? notStartedDialogues[interactions] : new QuestDialogueID(),
                QuestState.OnGoing => interactions < onGoingDialogues.Length ? onGoingDialogues[interactions] : new QuestDialogueID(),
                _ => interactions < completedDialogues.Length ? completedDialogues[interactions] : new QuestDialogueID()
            };
        }

        private void CheckForAction(DialogueAction action)
        {
            switch (action)
            {
                case DialogueAction.StartQuest:
                    GameManager.Instance.questManager.StartNewQuest(questID);
                    m_InteractTimes = 0;
                    break;
                case DialogueAction.EndQuestGood:
                    GameManager.Instance.questManager.GetQuest(questID).EndQuestPositive();
                    m_InteractTimes = 0;
                    break;
                case DialogueAction.EndQuestBad:
                    GameManager.Instance.questManager.GetQuest(questID).EndQuestNegative();
                    m_InteractTimes = 0;
                    break;
            }
        }
    }
}
