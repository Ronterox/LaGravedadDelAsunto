using Managers;
using Minigames;
using Plugins.Properties;
using UnityEngine;

namespace General.Interactables
{
    public class DoorInteractable : QTEInteractable
    {
        [Scene] public string roomScene;
        public Transform helpMessagePosition;

        public bool isUnlock;

        public override void Interact()
        {
            if (isUnlock) LevelLoadManager.Instance.LoadScene(roomScene);
            else base.Interact();
        }

        protected override void OnEnterTrigger(Collider other) => GameManager.Instance.dialogueManager.Type("Press \"E\" to interact!", helpMessagePosition.position);

        protected override void OnExitTrigger(Collider other) { }

        public override void OnCorrectPressEvent() { }

        public override void OnWrongPressEvent() { }

        public override void OnQTEStart() { }

        public override void OnQTEStop(int correctPresses, int wrongPresses)
        {
            isUnlock = correctPresses > wrongPresses;
            if (isUnlock) LevelLoadManager.Instance.LoadScene(roomScene);
        }
    }
}
