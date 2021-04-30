using System;
using General.Utilities;
using Managers;
using Plugins.Audio;
using Plugins.Persistence;
using UnityEngine;

namespace Inventory_System
{
    public class PickUpItem : Interactable, IDataPersister
    {
        public Item item;

        [Header("Sfx")]
        public AudioClip pickSfx;

        public override void Interact() => PickUp();

        public DataSettings dataSettings;

        public bool isPicked;

        private void OnEnable()
        {
            PersistentDataManager.LoadPersistedData(this);
            gameObject.SetActive(!isPicked);
        }

        private void OnDisable() => PersistentDataManager.SavePersistedData(this);

        protected override void OnEnterTrigger(Collider other) { }

        protected override void OnExitTrigger(Collider other) { }

        private void PickUp()
        {
            if (pickSfx) SoundManager.Instance.PlaySound(pickSfx, transform.position);
            if (GameManager.Instance.inventory.Add(item)) gameObject.SetActive(false);
        }

        public DataSettings GetDataSettings() => dataSettings;

        public void SetDataSettings(ushort dataId, DataSettings.PersistenceType persistenceType) { }

        
        public Data SaveData() => new Data<bool>(isPicked);

        public void LoadData(Data data) => isPicked = ((Data<bool>)data).value;
    }
}
