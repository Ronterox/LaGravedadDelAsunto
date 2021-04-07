using System.Collections.Generic;
using Cameras;
using Managers;
using Player;
using Plugins.Tools;
using UnityEngine;

namespace Inventory_System
{
    public class Inventory : MonoBehaviour
    {
        public List<Item> items = new List<Item>();

        [Header("Drop Settings")]
        public float force;
        public float offset;

        private int m_InventorySpace = 20;
        private bool m_InInventory;

        private InventorySlot[] m_InventorySlots;

        public void InitializeInventory(GameObject inventory)
        {
            m_InventorySlots = inventory.GetComponentsInChildren<InventorySlot>();
            m_InventorySpace = m_InventorySlots.Length;
            UpdateUI();
        }

        private void Update()
        {
            if (!PlayerInput.Instance.Inventory) return;

            if (m_InInventory) GUIManager.Instance.CloseGUIMenu();
            else GUIManager.Instance.OpenInventory();
            
            m_InInventory = !m_InInventory;
        }

        private void UpdateUI()
        {
            for (var i = 0; i < m_InventorySlots.Length; i++)
            {
                if (i < items.Count) m_InventorySlots[i].AddItem(items[i]);
                else m_InventorySlots[i].ClearSlot();
            }
        }

        public bool Add(Item item)
        {
            if (items.Count >= m_InventorySpace)
            {
                Debug.Log($"Inventory is full couldn't add item {item.itemName}".ToColorString("red"));
                return false;
            }
            items.Add(item);
            if(m_InInventory) UpdateUI();
            return true;
        }

        public bool Has(Item item) => items.Contains(item);

        public void Remove(Item item)
        {
            items.Remove(item);
            if(m_InInventory) UpdateUI();
        }

        public void Drop(Item item)
        {
            Vector3 direction = UtilityMethods.GetRandomDirection(true, false);
            GameObject obj = Instantiate(item.itemRef, CameraManager.Instance.playerTransform.position + direction * offset, Quaternion.identity);
            obj.GetComponent<Rigidbody>()?.AddForce(direction * force, ForceMode.Impulse);
            Remove(item);
        }
    }
}
