using Questing_System;
using UnityEngine;

namespace NPCs
{
    public class NPC : MonoBehaviour
    {
        private string m_Name, m_Description;
        public Campaign campaign;

        public void StartCampaign() => campaign.StartCampaignQuest();

        public void Interact()
        {
            if (campaign.isCompleted)
            {
                
            }
            else if(!campaign.started) StartCampaign();
        }
    }
}
