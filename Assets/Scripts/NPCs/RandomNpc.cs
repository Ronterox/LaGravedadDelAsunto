using Questing_System;
using UnityEngine;

namespace NPCs
{
    public class RandomNpc : NPC
    {
        protected override void OnCampaignCompletedInteraction(Campaign campaign) { }

        protected override void OnInteractionRangeEnter(Campaign campaign) { }

        protected override void OnInteractionRangeExit(Campaign campaign) { }

        protected override void OnInteraction(Campaign campaign) => Say(npcScriptable.dialoguesIds[Random.Range(0, npcScriptable.dialoguesIds.Count)]);
    }
}
