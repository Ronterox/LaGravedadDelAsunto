using System.Linq;
using Managers;
using Plugins.Tools;
using Questing_System;

namespace NPCs
{
    public class RandomNpc : NPC
    {
        protected override void OnQuestCompletedInteraction(Quest quest) => SayRandomThing();

        protected override void OnInteractionRangeEnter(Quest quest) => SayRandomThing();

        protected override void OnInteractionRangeExit(Quest quest) => SayRandomThing();

        protected override void OnInteraction(Quest quest) => SayRandomThing();

        private void SayRandomThing()
        {
            QuestManager questManager = GameManager.Instance.questManager;
            Quest randomQuest = questManager.GetQuestRandom();
            
            Say(GetQuestRelatedDialogueID(randomQuest).dialogueID);
        }

        public QuestDialogueID GetQuestRelatedDialogueID(Quest quest) => quest.questState switch
        {
            QuestState.NotStarted => notStartedDialogues.Where(questDialogue => questDialogue.questRelatedId.Equals(quest.questID)).ToArray().Shuffle().FirstOrDefault(), 
            QuestState.OnGoing => onGoingDialogues.Where(questDialogue => questDialogue.questRelatedId.Equals(quest.questID)).ToArray().Shuffle().FirstOrDefault(), 
            QuestState.Completed => completedDialogues.Where(questDialogue => questDialogue.questRelatedId.Equals(quest.questID)).ToArray().Shuffle().FirstOrDefault(), 
            _ => new QuestDialogueID()
        };
    }
}
