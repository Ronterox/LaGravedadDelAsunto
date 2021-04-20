using System.Collections.Generic;
using Managers;
using Questing_System;
using UnityEngine;

namespace NPCs
{
    public class RandomNpc : NPC
    {
        public List<string> dialogueIds = new List<string>();

        private const string GATHER_HUNTING = "gather_hunting";

        protected override void Awake()
        {
            base.Awake();
            
            foreach (DialogueGroup dialogueGroup in npcScriptable.dialogues)
                foreach (Dialogue dialogue in dialogueGroup.dialogues) dialogueIds.Add(dialogue.id);
        }

        protected override void OnQuestCompletedInteraction(Quest quest) => SayRandomThing();

        protected override void OnInteractionRangeEnter(Quest quest) => SayRandomThing();

        protected override void OnInteractionRangeExit(Quest quest) => SayRandomThing();

        protected override void OnInteraction(Quest quest)
        {
            if (GameManager.Instance.questManager.onGoingQuests.Count > 0)
            {
                foreach (Quest questManagerONGoingQuest in GameManager.Instance.questManager.onGoingQuests) CheckQuest(questManagerONGoingQuest);
            }
            else SayRandomThing();
        }

        private void CheckQuest(Quest quest)
        {
            switch (quest.questID)
            {
                case GATHER_HUNTING: 
                    //DO something
                    break;
            }
        }

        private void SayRandomThing() => Say(dialogueIds[Random.Range(0, dialogueIds.Count)]);
    }
}
