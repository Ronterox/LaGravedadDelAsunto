using Questing_System;

namespace NPCs
{
    public class TutorialNPC : NPC
    {
        private const string GATHERING_QUEST_ID = "gathering";

        protected override void Awake()
        {
            base.Awake();
            Say("greetings");
        }

        protected override void OnQuestCompletedInteraction(Quest quest)
        {
            if (quest.questEndType == QuestEndType.NeutralEnding) Say("neutral ending");
            else Say(quest.questEndType == QuestEndType.DoneGood ? "good ending" : "bad ending");
        }

        protected override void OnInteractionRangeEnter(Quest quest) => Say("interaction enter");

        protected override void OnInteractionRangeExit(Quest quest) => Say("interaction exit");

        protected override void OnInteraction(Quest quest)
        {

            if (quest && !quest.questID.Equals(GATHERING_QUEST_ID)) return;

            if (quest.IsJustStarted()) Say("look around");
            else if (quest.IsOnGoing) Say("collect");
        }

    }
}
