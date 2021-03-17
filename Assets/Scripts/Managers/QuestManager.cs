using System.Collections.Generic;
using Plugins.Tools;
using Questing_System;

namespace Managers
{
    //TODO: Comment all methods of the quest system
    public class QuestManager : Singleton<QuestManager>
    {
        public Campaign[] allCampaigns;
        private readonly Dictionary<string, Campaign> m_Campaigns = new Dictionary<string, Campaign>();
        public List<Campaign> onGoingCampaigns;

        protected override void Awake()
        {
            base.Awake();
            foreach (Campaign campaign in allCampaigns) m_Campaigns.Add(campaign.id, campaign);
        }

        public void UpdateCampaigns()
        {
            onGoingCampaigns.ForEach(campaign => campaign.UpdateCampaign());
            RemoveCompletedCampaigns();
        }

        public void RemoveCompletedCampaigns()
        {
            foreach (Campaign campaign in onGoingCampaigns.ToArray())
            {
                if (campaign.IsCompleted) onGoingCampaigns.Remove(campaign);
            }
        }

        public void StartNewCampaign(string campaignID)
        {
            if (!m_Campaigns.TryGetValue(campaignID, out Campaign result)) return;
            result.StartCampaignQuest(0);
            onGoingCampaigns.Add(result);
        }

        public Campaign GetCampaign(string id) => m_Campaigns[id];
    }
}