using General.Utilities;
using Inventory_System;
using Managers;
using NPCs;
using UnityEngine;

namespace Questing_System.Quests
{
    public class HuntCollectQuest : Quest
    {
        public int quantityIngredientsToComplete;
        
        [Header("Required Items")]
        public Item[] goodIngredients;
        public Item[] badIngredients;

        private int m_GoodIngredientsCount, m_BadIngredientsCount;

        public Interactable npc;
        
        protected override void OnceQuestIsDoneGood() => npc.onInteraction -= CheckInventory;

        protected override void OnceQuestIsDoneBad() => npc.onInteraction -= CheckInventory;

        protected override void OnceQuestStarted() => npc.onInteraction += CheckInventory;

        public void CheckInventory()
        {
            Inventory inventory = GameManager.Instance.inventory;
            m_GoodIngredientsCount = 0;
            m_BadIngredientsCount = 0;
            
            foreach (Item goodIngredient in goodIngredients)
            {
                if (inventory.Has(goodIngredient)) m_GoodIngredientsCount++;
            }
            
            foreach (Item badIngredient in badIngredients)
            {
                if (inventory.Has(badIngredient)) m_BadIngredientsCount++;
            }

            if (m_GoodIngredientsCount >= quantityIngredientsToComplete || m_BadIngredientsCount >= quantityIngredientsToComplete)
            {
                EndQuest(m_GoodIngredientsCount > m_BadIngredientsCount? QuestEndType.DoneGood : QuestEndType.DoneBad);
            }
        }
    }
}
