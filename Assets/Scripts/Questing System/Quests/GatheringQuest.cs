using Managers;
using UnityEngine;

namespace Questing_System.Quests
{
    public class GatheringQuest : Quest
    {
        [Header("Gathering Quest Settings")]
        public int toGetGatheringQuantity;
        protected int m_TotalDestroyed;
        
        public int toDestroyGatheringQuantity;
        protected int m_TotalGathered;

        public void Gather()
        {
            if (++m_TotalGathered == toGetGatheringQuantity) GameManager.Instance.UpdateCampaigns();
        }

        public void DestroyGathered()
        {
            if(--m_TotalDestroyed == toDestroyGatheringQuantity) GameManager.Instance.UpdateCampaigns();
        }

        protected override void OnceQuestIsCompleted() { }

        protected override void OnceQuestIsFailed() { }

        protected override void OnceQuestStarted() { }
    }
}
