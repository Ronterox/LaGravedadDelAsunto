using Questing_System;
using UnityEngine;

namespace NPCs
{
    public class RandomNpc : NPC
    {
        protected override void OnCampaignCompletedInteraction(Campaign campaign) => SayRandomThing();

        protected override void OnInteractionRangeEnter(Campaign campaign) => SayRandomThing();

        protected override void OnInteractionRangeExit(Campaign campaign) => SayRandomThing();

        protected override void OnInteraction(Campaign campaign)
        {
            print("Hello");
            SayRandomThing();
        }

        private void SayRandomThing() => Say(npcScriptable.dialoguesIds[Random.Range(0, npcScriptable.dialoguesIds.Count)]);
    }
}
