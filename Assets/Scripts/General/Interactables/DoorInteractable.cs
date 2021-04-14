using General.Utilities;
using Managers;
using Plugins.Properties;
using UnityEngine;

namespace General.Interactables
{
    public class DoorInteractable : Interactable
    {
        [Scene] public string roomScene; 
        public Transform helpMessagePosition;

        public override void Interact() => LevelLoadManager.Instance.LoadScene(roomScene);

        protected override void OnEnterTrigger(Collider other) => GameManager.Instance.dialogueManager.Type("Press \"E\" to interact!", helpMessagePosition.position);

        protected override void OnExitTrigger(Collider other) { }
    }
}
