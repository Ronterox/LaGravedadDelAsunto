namespace Questing_System
{
    [System.Serializable]
    public class CampaignQuest
    {
        public Campaign campaign;
        
        public Quest currentQuest;
        public Quest mainQuest, badQuest, goodQuest;

        public void StartQuest() => (currentQuest = mainQuest).StartQuest();

        public void CompleteCampaignQuest() => campaign.UpdateCampaign();
    }
}
