using General;
using Managers;

namespace Inventory_System
{
    public class PickUpItem : Interactable
    {

        public Item item;
        public override void Interact()
        {
            base.Interact();
            PickUp();
        }

        private void PickUp()
        { 
            if(GameManager.Instance.inventory.Add(item)) Destroy(gameObject);
        }
    }
}
