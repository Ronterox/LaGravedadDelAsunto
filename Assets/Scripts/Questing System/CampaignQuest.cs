using UnityEngine;

namespace Questing_System
{
    [System.Serializable]
    public class CampaignQuest
    {
        [HideInInspector] public Campaign campaign;
        
        public Quest currentQuest;
        
        [Header("Required")]
        public Quest mainQuest, badQuest, goodQuest;

        public void StartQuest()
        {
            mainQuest.parentQuest = this;
            if(badQuest) badQuest.parentQuest = this;
            if(goodQuest) goodQuest.parentQuest = this;
            
            (currentQuest = mainQuest).StartQuest();
        } 

        public void CompleteCampaignQuest() => campaign.UpdateCampaign();
    }
}
