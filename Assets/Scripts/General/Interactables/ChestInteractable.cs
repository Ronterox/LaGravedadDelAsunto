using System;
using General.Utilities;
using Inventory_System;
using Managers;
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
            GameManager.Instance.inventory.SpawnItem(items.GetRandom(), transform.position);
            ArchievementsManager.Instance.UpdateAchievement("achievement4", 1);
            Action deactivate = () => gameObject.SetActive(false);
            deactivate.DelayAction(2f);
        }

        protected override void OnEnterTrigger(Collider other) => GameManager.Instance.dialogueManager.Type("Press \"E\" to interact!", helpMessagePosition.position);

        protected override void OnExitTrigger(Collider other) { }

        public DataSettings GetDataSettings() => dataSettings;

        public void SetDataSettings(ushort dataId, DataSettings.PersistenceType persistenceType) { }

        public Data SaveData() => new Data<bool>(isUnlock);

        public void LoadData(Data data) => isUnlock = ((Data<bool>)data).value;
    }
}
