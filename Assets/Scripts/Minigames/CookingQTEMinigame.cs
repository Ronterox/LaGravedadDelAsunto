using General.Minigames;
using General.Utilities;
using UnityEngine;

namespace Minigames
{
    public class CookingQTEMinigame : Interactable
    {
        public CookingQTE quickTimeEvent;

        public override void Interact() => quickTimeEvent.StartQuickTimeEvent();

        protected override void OnEnterTrigger(Collider other) { }

        protected override void OnExitTrigger(Collider other) { }
    }
}
