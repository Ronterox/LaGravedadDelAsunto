using Managers;
using Questing_System;

namespace NPCs
{
    public class BambinoNPC : NPC
    {
        public const string MAIN_QUEST_ID = "gathering";
        public const string GOOD_QUEST_ID = "bambino_cooking";

        protected override void OnCampaignCompletedInteraction(Campaign campaign) { }

        protected override void OnInteractionRangeEnter(Campaign campaign) => Say(campaign.IsStarted ? "How are you?" : "Greetings");

        protected override void OnInteractionRangeExit(Campaign campaign) => Say(campaign.IsStarted ? "Cya" : "...");

        protected override void OnInteraction(Campaign campaign)
        {
            if (!campaign.IsStarted)
            {
                switch (m_InteractTimes)
                {
                    case 1: Say("My name"); break;
                    case 2: Say("Welcome"); break;
                    case 3:
                        Say("Quest 1");
                        GameManager.Instance.questManager.StartNewCampaign(npcScriptable.campaignID);
                        m_InteractTimes = 0;
                        break;
                }
            }
            else
            {
                Quest current = campaign.GetCurrentQuest();
                if (current.questID.Equals(MAIN_QUEST_ID))
                {
                    switch (m_InteractTimes)
                    {
                        case 1: Say("Go gather"); break;
                        case 2: Say("Animals drop"); break;
                        case 3: Say("Leave me"); break;
                    }
                }
            }
        }
    }
}
