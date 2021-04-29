using General.Utilities;
using Managers;
using Plugins.Audio;
using UnityEngine;

namespace Inventory_System
{
    public class PickUpItem : Interactable
    {
        public Item item;

        [Header("Sfx")]
        public AudioClip pickSfx;

        public override void Interact() => PickUp();

        protected override void OnEnterTrigger(Collider other) { }

        protected override void OnExitTrigger(Collider other) { }

        private void PickUp()
        {
            if(pickSfx) SoundManager.Instance.PlaySound(pickSfx, transform.position);
            if (GameManager.Instance.inventory.Add(item)) gameObject.SetActive(false);
        }
    }
}
