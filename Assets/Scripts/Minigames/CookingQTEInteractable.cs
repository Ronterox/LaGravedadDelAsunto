using General.Minigames;
using General.Utilities;
using UnityEngine;

namespace Minigames
{
    public class CookingQTEInteractable : GUIInteractable
    {
        public CookingQTE quickTimeEvent;

        public override void OnInterfaceOpen() => quickTimeEvent.StartQuickTimeEvent();

        public override void OnInterfaceClose() => quickTimeEvent.StopQuickTimeEvent();

        protected override void OnEnterTrigger(Collider other) { }

        protected override void OnExitTrigger(Collider other) { }
    }
}
