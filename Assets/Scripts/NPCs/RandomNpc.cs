using System.Collections.Generic;
using Questing_System;
using UnityEngine;

namespace NPCs
{
    public class RandomNpc : NPC
    {
        public List<string> dialogueIds = new List<string>();

        protected override void Awake()
        {
            base.Awake();
            
            foreach (DialogueGroup dialogueGroup in npcScriptable.dialogues)
                foreach (Dialogue dialogue in dialogueGroup.dialogues) dialogueIds.Add(dialogue.id);
        }

        protected override void OnCampaignCompletedInteraction(Campaign campaign) => SayRandomThing();

        protected override void OnInteractionRangeEnter(Campaign campaign) => SayRandomThing();

        protected override void OnInteractionRangeExit(Campaign campaign) => SayRandomThing();

        protected override void OnInteraction(Campaign campaign) => SayRandomThing();

        private void SayRandomThing() => Say(dialogueIds[Random.Range(0, dialogueIds.Count)]);
    }
}
