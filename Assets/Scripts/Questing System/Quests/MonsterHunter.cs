using General.Utilities;
using Inventory_System;
using Managers;
using NPCs;
using Plugins.Tools;

namespace Questing_System.Quests
{
    public class MonsterHunter : Quest
    {
        private int m_MonstersKilled, m_MonstersFeed;

        public Monster[] enemies;

        private void KillMonster()
        {
            m_MonstersKilled++;
            CheckQuestState();
        }

        private void FeedMonster(Interactable interactable)
        {
            if (interactable is Monster enemy)
            {
                Inventory inventory = GameManager.Instance.inventory;
                if (inventory.Has(enemy.favoriteFood))
                {
                    m_MonstersFeed++;
                    CheckQuestState();
                }
                else
                {
                    enemy.Write($"You don't have any {enemy.favoriteFood} on you!".ToColorString("red"));
                }
            }
        }

        protected override void OnceQuestIsDoneGood() => RemoveListeners();

        protected override void OnceQuestIsDoneBad() => RemoveListeners();

        protected override void OnceQuestStarted() => AddListeners();

        private void CheckQuestState()
        {
            if (m_MonstersKilled + m_MonstersFeed >= enemies.Length)
            {
                EndQuest(m_MonstersKilled > m_MonstersFeed? QuestEndType.DoneBad : QuestEndType.DoneGood);
            }
        }

        private void AddListeners() =>
            enemies.ForEach(x =>
            {
                x.damageable.myHealth.onDieEvent.AddListener(KillMonster);
                x.onInteraction += FeedMonster;
            });

        private void RemoveListeners() =>
            enemies.ForEach(x =>
            {
                x.damageable.myHealth.onDieEvent.RemoveListener(KillMonster);
                x.onInteraction -= FeedMonster;
            });
    }
}
