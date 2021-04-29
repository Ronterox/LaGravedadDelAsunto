using System.Collections.Generic;
using System.Linq;
using General.Utilities;
using Managers;
using Minigames;
using NPCs;
using UI;
using UnityEngine;

namespace Questing_System.Quests
{
    [System.Serializable]
    public struct DeliveryNPC
    {
        public RandomNpc randomNpc;
        public Sprite npcImage;

        public FoodPlate foodPlate { get; set; }
    }

    public class DeliveringQuest : Quest
    {
        public FoodPlate[] foodPlates;
        public int numberOfDeliveries;
        [Space]
        public List<DeliveryNPC> randomNpcs;

        [Header("Feedback")]
        public GameObject npcCardTemplate;
        private GameObject m_CardInstance;

        private readonly List<DeliveryNPC> m_DeliveringNpcs = new List<DeliveryNPC>();
        private int m_CompletedDeliveries, m_FailedDeliveries;

        protected override void OnceQuestIsDoneGood() => CleanNPCs();

        protected override void OnceQuestIsDoneBad() => CleanNPCs();

        private void CleanNPCs()
        {
            m_DeliveringNpcs.ForEach(delivery => delivery.randomNpc.onInteraction -= ReceiveDelivery);
            m_DeliveringNpcs.Clear();
            randomNpcs.Clear();
        }

        protected override void OnceQuestStarted() => SelectRandom();

        private DeliveryNPC GetDeliveryNpc(RandomNpc npc) => randomNpcs.FirstOrDefault(deliveryNpc => deliveryNpc.randomNpc.Equals(npc));

        private void ReceiveDelivery(Interactable interactable)
        {
            if (!(interactable is RandomNpc npc)) return;

            DeliveryNPC delivery = GetDeliveryNpc(npc);

            if (GameManager.Instance.inventory.Has(delivery.foodPlate)) m_CompletedDeliveries++;
            else m_FailedDeliveries++;

            if (m_CompletedDeliveries >= numberOfDeliveries || m_FailedDeliveries >= numberOfDeliveries)
            {
                EndQuest(m_CompletedDeliveries > m_FailedDeliveries ? QuestEndType.DoneGood : QuestEndType.DoneBad);
            }
            else SelectRandom();

            delivery.randomNpc.onInteraction -= ReceiveDelivery;
        }

        private DeliveryNPC GetRandomDelivery()
        {
            int rng = Random.Range(0, randomNpcs.Count);
            DeliveryNPC npc = randomNpcs[rng];

            randomNpcs.RemoveAt(rng);
            m_DeliveringNpcs.Add(npc);

            npc.foodPlate = foodPlates[Random.Range(0, foodPlates.Length)];

            return npc;
        }

        private void SelectRandom()
        {
            DeliveryNPC npc = GetRandomDelivery();

            m_CardInstance = GUIManager.Instance.InstantiateUI(npcCardTemplate, false, .8f);
            var card = m_CardInstance.GetComponent<ImageTextCard>();

            card.Set(npc.npcImage, npc.randomNpc.npcName);

            npc.randomNpc.onInteraction += ReceiveDelivery;
        }
    }
}
