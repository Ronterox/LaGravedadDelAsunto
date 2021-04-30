using Inventory_System;
using Plugins.Audio;
using Questing_System;
using UnityEngine;

namespace NPCs
{
    public class Monster : NPC
    {
        public Item favoriteFood;
        public AudioClip monsterSound;
        
        protected override void OnQuestCompletedInteraction(Quest quest) { }

        protected override void OnInteractionRangeEnter(Quest quest) => SoundManager.Instance.PlaySound(monsterSound, transform.position, 1, true, 1, 10);

        protected override void OnInteractionRangeExit(Quest quest) => SoundManager.Instance.StopAllSfx();

        protected override void OnInteraction(Quest quest) { }
    }
}
