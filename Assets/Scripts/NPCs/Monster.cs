using Inventory_System;
using Managers;
using Plugins.Audio;
using Plugins.Tools;
using Questing_System;
using UnityEngine;

namespace NPCs
{
    public class Monster : NPC
    {
        public Item favoriteFood;
        public AudioClip monsterSound;

        protected override void OnQuestCompletedInteraction(Quest quest) { }

        protected override void OnInteractionRangeEnter(Quest quest)
        {
            Write("I would really like right now a " + favoriteFood.itemName);
            SoundManager.Instance.PlaySound(monsterSound, transform.position, 1, true, 1, 10);
        }

        protected override void OnInteractionRangeExit(Quest quest) => SoundManager.Instance.StopAllSfx();

        protected override void OnInteraction(Quest quest)
        {
            if (!GameManager.Instance.inventory.Has(favoriteFood)) Write("You don't have my favorite food".ToColorString("red"));
            else
            {
                Write("I will take this " + favoriteFood.itemName + " from you, Thank YOU!");
                GameManager.Instance.inventory.Remove(favoriteFood);
            }
        }
    }
}
