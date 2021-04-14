using Questing_System;

namespace NPCs
{
    public class BambinoNPC : NPC
    {
        public const string MAIN_QUEST_ID = "";
        
        protected override void OnCampaignCompletedInteraction(Campaign campaign) { }

        protected override void OnInteractionRangeEnter(Campaign campaign) => Say("How are you?");

        protected override void OnInteractionRangeExit(Campaign campaign) => Say("Cya");

        protected override void OnInteraction(Campaign campaign)
        {
            if (!campaign.IsStarted)
            {
                switch (m_InteractTimes)
                {
                    case 1: Say("Hello"); break;
                    case 2: Say("Hello2"); break;
                    case 3: Say("Accept Mission"); break;
                    case 4: campaign.StartCampaignQuest(0); break;
                }
            }
            else
            {
                Quest current = campaign.GetCurrentQuest();
                if (current.questID.GetHashCode().Equals(MAIN_QUEST_ID.GetHashCode()))
                {
                    Say("Do the quest");
                }
            }
            
        }
    }
}
