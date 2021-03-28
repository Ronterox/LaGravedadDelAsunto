using General;
using General.Utilities;
using Managers;
using UnityEngine;

namespace Inventory_System
{
    public class PickUpItem : Interactable
    {
        public Item item;
        public override void Interact() => PickUp();

        protected override void OnEnterTrigger(Collider other) { }

        protected override void OnExitTrigger(Collider other) { }

        private void PickUp()
        {
            if (GameManager.Instance.inventory.Add(item)) Destroy(gameObject);
        }
    }
}
