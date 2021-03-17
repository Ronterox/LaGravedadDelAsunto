using Managers;
using UnityEngine;

namespace Questing_System.Quests
{
    public class GatheringQuest : Quest
    {
        [Header("Gathering Quest Settings")]
        public GameObject collectable;
        public Transform[] spawnPositions;

        [Space] public int toGetGatheringQuantity;
        protected int m_TotalDestroyed;

        public int toDestroyGatheringQuantity;
        protected int m_TotalGathered;

        public void Gather()
        {
            if (++m_TotalGathered == toGetGatheringQuantity) CompleteQuest();
        }

        public void DestroyGathered()
        {
            if (++m_TotalDestroyed == toDestroyGatheringQuantity) FailQuest();
        }

        protected override void OnceQuestIsCompleted() { }

        protected override void OnceQuestIsFailed() { }

        protected override void OnceQuestStarted()
        {
            foreach (Transform spawnPosition in spawnPositions) Instantiate(collectable, spawnPosition.position, Quaternion.identity, spawnPosition).SetActive(true);
        }
    }
}
