using Inventory_System;
using Managers;
using UnityEngine;

namespace Demo
{
    public class TutorialSword : PickUpItem
    {
        public GameObject toDestroy;
        public GameObject toActivate;

        public override void Interact()
        {
            base.Interact();
            toActivate.SetActive(true);
            Destroy(toDestroy);
            ArchievementsManager.Instance.UpdateAchievement("tutorial", 1);
        }
    }
}
