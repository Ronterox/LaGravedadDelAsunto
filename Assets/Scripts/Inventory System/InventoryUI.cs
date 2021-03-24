using Managers;
using UnityEngine;

namespace Inventory_System
{
    public class InventoryUI : MonoBehaviour
    {
        private Inventory inventory;
        public Transform itemsParent;
        private InventorySlot[] slots;
        public GameObject inventoryUi;
        public bool inInventory;
        private void Start()
        {
            inventory = GameManager.Instance.inventory;
            inventory.onItemChangedCallback += UpdateUi;

            slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        }


        private void Update()
        {
            if (Input.GetButtonDown("Inventory"))
            {
                if (inInventory)
                {
                    inventoryUi.SetActive(false);
                    Time.timeScale = 1f;
                }
                else
                {
                    inventoryUi.SetActive(true);
                    Time.timeScale = 0f;
                }
                inInventory = !inInventory;
            }
        }

        private void UpdateUi()
        {
            for(var i = 0; i < slots.Length; i++)
            {
                if (i < inventory.items.Count)
                {
                    slots[i].AddItem(inventory.items[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }

        }
    }
}
