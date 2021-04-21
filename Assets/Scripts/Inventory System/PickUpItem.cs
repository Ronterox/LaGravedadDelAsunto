using General.Utilities;
using Managers;
using Plugins.Audio;
using UnityEngine;
using Ragdoll;

namespace Inventory_System
{
    public class PickUpItem : Interactable
    {
        public Item item;
        private RagdollScript ragdollref;

        [Header("Sfx")]
        public AudioClip pickSfx;

        private void Awake()
        {
            ragdollref = GetComponent<RagdollScript>();
        }

        public override void Interact() => PickUp();

        protected override void OnEnterTrigger(Collider other) { }

        protected override void OnExitTrigger(Collider other) { }

        private void PickUp()
        {
            SoundManager.Instance.PlaySound(pickSfx, transform.position);
            if (GameManager.Instance.inventory.Add(item))
            {
                ragdollref.enableRagdoll(true);
                Destroy(gameObject);
            }
        }
    }
}
