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

        protected override void OnCampaignCompletedInteraction(Campaign campaign) => SayRandomThing();

        protected override void OnInteractionRangeEnter(Campaign campaign) => SayRandomThing();

        protected override void OnInteractionRangeExit(Campaign campaign) => SayRandomThing();

        protected override void OnInteraction(Campaign campaign)
        {
            if (GameManager.Instance.questManager.onGoingCampaigns.Count > 0)
            {
                foreach (Campaign questManagerONGoingCampaign in GameManager.Instance.questManager.onGoingCampaigns) CheckCampaign(questManagerONGoingCampaign);
            }
            else SayRandomThing();
        }

        private void CheckCampaign(Campaign campaign)
        {
            Quest currentQuest = campaign.GetCurrentQuest();
            switch (currentQuest.questID)
            {
                case GATHER_HUNTING: 
                    //DO something
                    break;
            }
        }

        private void SayRandomThing() => Say(dialogueIds[Random.Range(0, dialogueIds.Count)]);
    }
}
