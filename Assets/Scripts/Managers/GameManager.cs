using System.Collections.Generic;
using Plugins.Tools;
using Questing_System;

namespace Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public Dictionary<string, Campaign> campaigns;
        public List<Campaign> onGoingCampaings;

        public void UpdateCampaigns() { foreach (KeyValuePair<string, Campaign> campaign in campaigns) campaign.Value.UpdateCampaign(); }

        public void StartNewCampaign(string campaignID)
        {
            if (!campaigns.TryGetValue(campaignID, out Campaign result)) return;
            onGoingCampaings.Add(result);
            result.StartCampaignQuest(0);
        }

        public Campaign GetCampaign(string id) => campaigns[id];
    }
}
