using Managers;
using Questing_System;

namespace NPCs
{
    public class BambinoNPC : NPC
    {
        public const string MAIN_QUEST_ID = "gathering";
        public const string GOOD_QUEST_ID = "bambino_cooking";

        protected override void OnQuestCompletedInteraction(Quest quest) { }

        protected override void OnInteractionRangeEnter(Quest quest) => Say(quest.IsStarted ? "How are you?" : "Greetings");

        protected override void OnInteractionRangeExit(Quest quest) => Say(quest.IsStarted ? "Cya" : "...");

        protected override void OnInteraction(Quest quest)
        {
            if (!quest.IsOnGoing)
            {
                switch (m_InteractTimes)
                {
                    case 1: Say("Welcome"); break;
                    case 2: Say("Quest 1"); break;
                    case 3:
                        GameManager.Instance.questManager.StartNewQuest(npcScriptable.questID);
                        m_InteractTimes = 0;
                        break;
                }
            }
            else
            {
                if (quest.questID.Equals(MAIN_QUEST_ID))
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
