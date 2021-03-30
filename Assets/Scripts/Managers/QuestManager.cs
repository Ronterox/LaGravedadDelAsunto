using System.Collections.Generic;
using Questing_System;
using UnityEngine;

namespace Managers
{
    public class QuestManager : MonoBehaviour
    {
        public Campaign[] allCampaigns;
        
        private readonly Dictionary<string, Campaign> m_Campaigns = new Dictionary<string, Campaign>();
        private readonly HashSet<Campaign> onGoingCampaigns = new HashSet<Campaign>();

        private void Awake() { foreach (Campaign campaign in allCampaigns) m_Campaigns.Add(campaign.id, campaign); }

        public void UpdateCampaigns()
        {
            foreach (Campaign onGoingCampaign in onGoingCampaigns) onGoingCampaign.UpdateCampaign();
            onGoingCampaigns.RemoveWhere(campaign => campaign.IsCompleted);
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
