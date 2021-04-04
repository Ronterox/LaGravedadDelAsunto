using General.Minigames;
using General.Utilities;
using Inventory_System;
using Managers;
using Plugins.Tools;
using UnityEngine;

namespace Minigames
{
    public class CookingQTEInteractable : GUIInteractable
    {
        public CookingQTE quickTimeEvent;
        public Item ingredientToCook;

        public override void Interact()
        {
            if (GameManager.Instance.inventory.Has(ingredientToCook)) base.Interact();
            else Debug.Log($"Player doesn't have ingredient {ingredientToCook.itemName}".ToColorString("red"));
        }

        private void OnEnable() => quickTimeEvent.onQTEStop.AddListener(UseIngredient);

        private void OnDisable() => quickTimeEvent.onQTEStop.RemoveListener(UseIngredient);

        private void UseIngredient() => GameManager.Instance.inventory.Remove(ingredientToCook);

        public override void OnInterfaceOpen() => quickTimeEvent.StartQuickTimeEvent();

        public override void OnInterfaceClose() { }

        protected override void OnEnterTrigger(Collider other) { }

        protected override void OnExitTrigger(Collider other) { }
    }
}
