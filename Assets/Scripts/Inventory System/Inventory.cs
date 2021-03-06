using System;
using System.Collections.Generic;
using Cameras;
using Managers;
using Player;
using Plugins.Audio;
using Plugins.Persistence;
using Plugins.Tools;
using UnityEngine;

namespace Inventory_System
{
    public class Inventory : MonoBehaviour, IDataPersister
    {
        public List<Item> items = new List<Item>();

        [Header("Drop Settings")]
        public float dropForce;
        public float dropOffset;

        private int m_InventorySpace = 20;
        private bool m_InInventory;

        private InventorySlot[] m_InventorySlots;

        [Header("Sfx")]
        public AudioClip onChangeSound;

        public DataSettings dataSettings;

        public delegate void InventoryChangeEvent();

        public event InventoryChangeEvent onInventoryChanged;

        public void InitializeInventory(GameObject inventory)
        {
            m_InventorySlots = inventory.GetComponentsInChildren<InventorySlot>();
            m_InventorySpace = m_InventorySlots.Length;
            UpdateUI();
        }

        private void OnEnable() => PersistentDataManager.LoadPersistedData(this);

        private void OnDisable() => PersistentDataManager.SavePersistedData(this);

        private void Update()
        {
            if (!PlayerInput.Instance.Inventory) return;
            OpenInventory();
        }

        public void OpenInventory()
        {
            if (m_InInventory) GUIManager.Instance.CloseGUIMenu();
            else GUIManager.Instance.OpenInventory(() => m_InInventory = true, () => m_InInventory = false);
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
                Debug.Log($"Inventory is full couldn't add destroyable {item.itemName}".ToColorString("red"));
                return false;
            }
            items.Add(item);
            CheckForUpdate();
            return true;
        }

        public bool Has(Item item) => items.Contains(item);

        private void CheckForUpdate()
        {
            onInventoryChanged?.Invoke();
            SoundManager.Instance.PlayNonDiegeticSound(onChangeSound);
            if (m_InInventory) UpdateUI();
        }

        public void Remove(Item item)
        {
            items.Remove(item);
            CheckForUpdate();
        }

        public void Drop(Item item) => Drop(item, CameraManager.Instance.playerTransform.position);

        public void Drop(Item item, Vector3 position)
        {
            SpawnItem(item, position);
            Remove(item);
        }

        public void SpawnItem(Item item, Vector3 position)
        {
            Vector3 direction = UtilityMethods.GetRandomDirection(true, false);
            GameObject obj = Instantiate(item.itemRef, position + direction * dropOffset, Quaternion.identity);
            obj.GetComponentSafely<Rigidbody>().AddForce(direction * dropForce, ForceMode.Impulse);
        }

        public void SpawnItems(Item item, Vector3 position, int quantity)
        {
            for (var i = 0; i < quantity; i++) SpawnItem(item, position);
        }

        #region Persistence

        public DataSettings GetDataSettings() => dataSettings;

        public void SetDataSettings(ushort dataId, DataSettings.PersistenceType persistenceType)
        {
            dataSettings.dataId = dataId;
            dataSettings.type = persistenceType;
        }

        public Data SaveData() => new Data<List<Item>>(items);

        public void LoadData(Data data) => items = ((Data<List<Item>>)data).value;

        #endregion
    }
}
