using System.Collections.Generic;
using Questing_System;
using UI;
using UnityEngine;

namespace Managers
{
    public class QuestManager : MonoBehaviour
    {
        [Header("Interface")]
        public GameObject questUIHolder;
        public GameObject questUITemplate;

        [Header("Campaigns")]
        public Campaign[] allCampaigns;

        private readonly Dictionary<string, Campaign> m_Campaigns = new Dictionary<string, Campaign>();
        private readonly HashSet<Campaign> onGoingCampaigns = new HashSet<Campaign>();

        private readonly List<GameObject> m_QuestUIGameObjects = new List<GameObject>();
        private GameObject m_InstanceQuestHolder;

        private void Awake()
        {
            foreach (Campaign campaign in allCampaigns) m_Campaigns.Add(campaign.id, campaign);
        }

        public void UpdateCampaigns()
        {
            foreach (Campaign onGoingCampaign in onGoingCampaigns) onGoingCampaign.UpdateCampaign();
            onGoingCampaigns.RemoveWhere(campaign => campaign.IsCompleted);

            m_QuestUIGameObjects.ForEach(ui => GUIManager.Instance.RemoveUI(ui));
            foreach (Campaign onGoingCampaign in onGoingCampaigns) AddQuestUI(onGoingCampaign.GetCurrentQuest());
        }

        public void StartNewCampaign(string campaignID)
        {
            if (!m_Campaigns.TryGetValue(campaignID, out Campaign result)) return;
            result.StartCampaignQuest(0);
            onGoingCampaigns.Add(result);

            AddQuestUI(result.GetCurrentQuest());
        }

        public void AddQuestUI(Quest quest)
        {
            if (!m_InstanceQuestHolder) m_InstanceQuestHolder = GUIManager.Instance.InstantiateUI(questUIHolder, .5f);
            GameObject inst = GUIManager.Instance.InstantiateUI(questUITemplate);

            var questUI = inst.GetComponent<QuestUI>();
            
            questUI.SetInfo(quest.questInfo.questName, quest.questInfo.questDescription);
            questUI.transform.parent = m_InstanceQuestHolder.transform;

            m_QuestUIGameObjects.Add(questUI.gameObject);
        }

        public Campaign GetCampaign(string id) => m_Campaigns[id];
    }
}
