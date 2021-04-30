using System;
using General.Utilities;
using Inventory_System;
using Managers;
using Plugins.Audio;
using Plugins.Persistence;
using Plugins.Tools;
using UnityEngine;

namespace General.Interactables
{
    public class ChestInteractable : Interactable, IDataPersister
    {
        public Item[] items;
        [Space]
        public Transform helpMessagePosition;
        public bool isUnlock;

        public DataSettings dataSettings;
        
        [Header("Audio")]
        public AudioClip openChest;

        protected override void Awake()
        {
            base.Awake();
            dataSettings.GenerateId(gameObject);
        }

        private void OnEnable()
        {
            PersistentDataManager.LoadPersistedData(this);
            gameObject.SetActive(!isUnlock);
        }

        private void OnDisable() => PersistentDataManager.SavePersistedData(this);

        public override void Interact()
        {
            Vector3 position = transform.position;
            
            GameManager.Instance.inventory.SpawnItem(items.GetRandom(), position);
            ArchievementsManager.Instance.UpdateAchievement("achievement4", 1);
            
            Action deactivate = () => gameObject.SetActive(false);
            deactivate.DelayAction(2f);
            
            SoundManager.Instance.PlaySound(openChest, position, 1, false, 1, 10);
        }

        protected override void OnEnterTrigger(Collider other) => GameManager.Instance.dialogueManager.Type("Press \"E\" to interact!", helpMessagePosition.position);

        protected override void OnExitTrigger(Collider other) { }

        public DataSettings GetDataSettings() => dataSettings;

        public void SetDataSettings(ushort dataId, DataSettings.PersistenceType persistenceType) { }

        public Data SaveData() => new Data<bool>(isUnlock);

        public void LoadData(Data data) => isUnlock = ((Data<bool>)data).value;
    }
}
