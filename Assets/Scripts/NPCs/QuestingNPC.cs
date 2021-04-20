using Managers;
using Questing_System;

namespace NPCs
{

    public class QuestingNPC : NPC
    {
        protected override void OnQuestCompletedInteraction(Quest quest) { }

        protected override void OnInteractionRangeEnter(Quest quest) => Say(quest.IsStarted ? "How are you?" : "Greetings");

        protected override void OnInteractionRangeExit(Quest quest) => Say(quest.IsStarted ? "Cya" : "...");

        protected override void OnInteraction(Quest quest)
        {
            QuestDialogueID questDialogueID = GetQuestDialogueID(quest.questState);

            Say(questDialogueID.dialogueID);
            CheckForAction(questDialogueID.action);
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
