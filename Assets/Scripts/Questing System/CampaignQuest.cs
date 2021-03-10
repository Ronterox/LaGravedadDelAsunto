namespace Questing_System
{
    public class CampaignQuest : Quest
    {
        public Quest mainQuest;
        public Quest badQuest, goodQuest;

        public override void OnceQuestIsCompleted() { }

        public override void OnceQuestIsFailed() { }

        public override void OnceQuestStarted() => mainQuest.StartQuest();
    }
}
