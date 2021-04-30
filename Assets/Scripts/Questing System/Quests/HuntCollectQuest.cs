using Inventory_System;
using Managers;
using Plugins.Tools;
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

        protected override void OnceQuestIsDoneGood() => GameManager.Instance.inventory.onInventoryChanged -= CheckInventory;

        protected override void OnceQuestIsDoneBad() => GameManager.Instance.inventory.onInventoryChanged -= CheckInventory;

        protected override void OnceQuestStarted() => GameManager.Instance.inventory.onInventoryChanged += CheckInventory;

        public void CheckInventory()
        {
            Inventory inventory = GameManager.Instance.inventory;
            m_GoodIngredientsCount = 0;
            m_BadIngredientsCount = 0;

            goodIngredients.ForEach(ingredient => { if (inventory.Has(ingredient)) m_GoodIngredientsCount++; });

            badIngredients.ForEach(ingredient => { if (inventory.Has(ingredient)) m_BadIngredientsCount++; });

            if (m_GoodIngredientsCount >= quantityIngredientsToComplete || m_BadIngredientsCount >= quantityIngredientsToComplete)
                EndQuest(m_GoodIngredientsCount > m_BadIngredientsCount ? QuestEndType.DoneGood : QuestEndType.DoneBad);
        }
    }
}
