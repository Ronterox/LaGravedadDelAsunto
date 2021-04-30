using Managers;
using Minigames;
using Plugins.Audio;
using Plugins.Persistence;
using Plugins.Properties;
using UnityEngine;

namespace General.Interactables
{
    public class DoorInteractable : QTEInteractable, IDataPersister
    {
        [Scene] public string roomScene;
        public Transform helpMessagePosition;

        public bool isUnlock;

        public DataSettings dataSettings;
        
        [Header("Audio")]
        public AudioClip openDoorSound;

        protected override void Awake()
        {
            base.Awake();
            dataSettings.GenerateId(gameObject);
        }

        public override void Interact()
        {
            if (isUnlock) LoadRoom();
            else base.Interact();
        }

        private void OnEnable() => PersistentDataManager.LoadPersistedData(this);

        private void OnDisable() => PersistentDataManager.SavePersistedData(this);

        protected override void OnEnterTrigger(Collider other) => GameManager.Instance.dialogueManager.Type(isUnlock? "Press \"E\" to interact!" : "Press \"E\" to lockpick the door!", helpMessagePosition.position);

        protected override void OnExitTrigger(Collider other) { }

        public override void OnCorrectPressEvent() { }

        public override void OnWrongPressEvent() { }

        public override void OnQTEStart() { }

        public override void OnQTEStop(int correctPresses, int wrongPresses)
        {
            isUnlock = correctPresses > wrongPresses;
            if (isUnlock) LoadRoom();
        }

        private void LoadRoom()
        {
            LevelLoadManager.Instance.LoadScene(roomScene);
            SoundManager.Instance.PlaySound(openDoorSound, transform.position, 1, false, 1, 10);
        }

        #region PERSISTENCE

        public DataSettings GetDataSettings() => dataSettings;

        public void SetDataSettings(ushort dataId, DataSettings.PersistenceType persistenceType)
        {
            dataSettings.dataId = dataId;
            dataSettings.type = persistenceType;
        }

        public Data SaveData() => new Data<bool>(isUnlock);

        public void LoadData(Data data) => isUnlock = ((Data<bool>)data).value;

        #endregion
    }
}
