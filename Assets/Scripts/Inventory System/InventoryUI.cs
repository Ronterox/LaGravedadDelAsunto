using Managers;
using Player;
using UnityEngine;

namespace Inventory_System
{
    public class InventoryUI : MonoBehaviour
    {
        public GameObject inventoryUi;
        public Transform itemsParent;
        private bool m_InInventory;

        private InventorySlot[] m_InventorySlots;
        private Inventory m_InventoryRef;

        private void Start()
        {
            m_InventorySlots = itemsParent.GetComponentsInChildren<InventorySlot>();

            m_InventoryRef = GameManager.Instance.inventory;
            m_InventoryRef.onItemChangedCallback += UpdateUi;
            m_InventoryRef.space = m_InventorySlots.Length;
        }

        private void Update()
        {
            if (PlayerInput.Instance.Inventory)
            {
                m_InInventory = !m_InInventory;
                inventoryUi.SetActive(m_InInventory);
            }
        }

        private void UpdateUi()
        {
            for (var i = 0; i < m_InventorySlots.Length; i++)
            {
                if (i < m_InventoryRef.items.Count) m_InventorySlots[i].AddItem(m_InventoryRef.items[i]);
                else m_InventorySlots[i].ClearSlot();
            }
        }
    }
}
