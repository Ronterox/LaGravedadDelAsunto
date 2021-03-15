using System.Collections.Generic;
using Plugins.Tools;
using Questing_System;

namespace Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public Campaign[] allCampaigns;
        private readonly Dictionary<string, Campaign> campaigns = new Dictionary<string, Campaign>();
        public List<Campaign> onGoingCampaigns;

        protected override void Awake()
        {
            base.Awake();
            foreach (Campaign campaign in allCampaigns) campaigns.Add(campaign.id, campaign);
        }

        public void UpdateCampaigns() { foreach (KeyValuePair<string, Campaign> campaign in campaigns) campaign.Value.UpdateCampaign(); }

        public void StartNewCampaign(string campaignID)
        {
            if (!campaigns.TryGetValue(campaignID, out Campaign result)) return;
            result.StartCampaignQuest(0);
            onGoingCampaigns.Add(result);
        }

        public Campaign GetCampaign(string id) => campaigns[id];
    }
}
