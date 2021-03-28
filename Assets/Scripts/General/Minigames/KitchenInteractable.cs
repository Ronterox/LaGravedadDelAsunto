using Plugins.Tools;
using UnityEngine;

namespace General.Minigames
{
    public class KitchenInteractable : Interactable
    {
        public PickIngredientsMinigame pickIngredientsMinigame;
        public override void Interact() => pickIngredientsMinigame.EnterMinigame();

        protected override void OnEnterTrigger(Collider other) { }

        protected override void OnExitTrigger(Collider other) { }
    }
}
