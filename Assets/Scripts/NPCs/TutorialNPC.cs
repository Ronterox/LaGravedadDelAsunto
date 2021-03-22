using Questing_System;

namespace NPCs
{
    public class TutorialNPC : NPC
    {
        private const string GATHERING_QUEST_ID = "gathering";

        private void Awake() => Say("greetings");

        protected override void OnCampaignCompletedInteraction(Campaign campaign)
        {
            if (campaign.campaignResult == QuestEndType.NeutralEnding) Say("neutral ending");
            else Say(campaign.campaignResult == QuestEndType.DoneGood ? "good ending" : "bad ending");
        }

        protected override void OnInteractionRangeEnter(Campaign campaign) => Say("interaction enter");

        protected override void OnInteractionRangeExit(Campaign campaign) => Say("interaction exit");

        protected override void OnInteraction(Campaign campaign)
        {
            Quest currentQuest = campaign.GetCurrentQuest();

            if (!currentQuest.questID.Equals(GATHERING_QUEST_ID)) return;

            if (currentQuest.IsJustStarted()) Say("look around");
            else if (currentQuest.IsOnGoing) Say("collect");
        }

    }
}
