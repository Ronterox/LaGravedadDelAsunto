using System.Collections.Generic;
using System.Linq;
using Managers;
using Plugins.Tools;
using Questing_System;
using Random = UnityEngine.Random;

namespace NPCs
{
    public class RandomNpc : NPC
    {
        private readonly List<string> m_DialogueIds = new List<string>();

        protected override void Awake()
        {
            base.Awake();

            foreach (DialogueGroup dialogueGroup in npcScriptable.dialogues)
                foreach (Dialogue dialogue in dialogueGroup.dialogues)
                    m_DialogueIds.Add(dialogue.id);
        }

        protected override void OnQuestCompletedInteraction(Quest quest) => SayRandomThing();

        protected override void OnInteractionRangeEnter(Quest quest) => SayRandomThing();

        protected override void OnInteractionRangeExit(Quest quest) => SayRandomThing();

        protected override void OnInteraction(Quest quest)
        {
            QuestManager questManager = GameManager.Instance.questManager;
            Quest randomQuest = questManager.GetQuestRandom();
            
            if (questManager.onGoingQuests.Count > 0) Say(GetQuestRelatedDialogueID(randomQuest).dialogueID);
            else SayRandomThing();
        }

        public QuestDialogueID GetQuestRelatedDialogueID(Quest quest) => quest.questState switch
        {
            QuestState.NotStarted => notStartedDialogues.Where(questDialogue => questDialogue.questRelatedId.Equals(quest.questID)).ToArray().Shuffle().FirstOrDefault(), 
            QuestState.OnGoing => onGoingDialogues.Where(questDialogue => questDialogue.questRelatedId.Equals(quest.questID)).ToArray().Shuffle().FirstOrDefault(), 
            QuestState.Completed => completedDialogues.Where(questDialogue => questDialogue.questRelatedId.Equals(quest.questID)).ToArray().Shuffle().FirstOrDefault(), 
            _ => new QuestDialogueID()
        };

        private void SayRandomThing() => Say(m_DialogueIds[Random.Range(0, m_DialogueIds.Count)]);
    }
}
