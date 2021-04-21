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

            Say(questDialogueID.dialogueID);
            
            return questDialogueID;
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
