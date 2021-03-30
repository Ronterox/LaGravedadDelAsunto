using General.Minigames;
using General.Utilities;
using UnityEngine;

namespace Minigames
{
    public class CookingQTEInteractable : GUIInteractable
    {
        public CookingQTE quickTimeEvent;

        private void OnEnable() => quickTimeEvent.onQTEStop.AddListener(ExitInterface);

        private void OnDisable() => quickTimeEvent.onQTEStop.RemoveListener(ExitInterface);

        public override void OnInterfaceOpen() => quickTimeEvent.StartQuickTimeEvent();

        public override void OnInterfaceClose() { }

        protected override void OnEnterTrigger(Collider other) { }

        protected override void OnExitTrigger(Collider other) { }
    }
}
