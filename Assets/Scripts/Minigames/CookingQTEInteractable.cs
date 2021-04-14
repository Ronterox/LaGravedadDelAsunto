using General.Minigames;
using General.Utilities;
using Inventory_System;
using Managers;
using Plugins.Tools;
using UnityEngine;
using UnityEngine.Events;

namespace Minigames
{
    public class CookingQTEInteractable : GUIInteractable
    {
        [Space]
        public Item ingredientToCook;
        public UnityAction onWrongPress, onCorrectPress;

        public override void Interact()
        {
            if (GameManager.Instance.inventory.Has(ingredientToCook)) base.Interact();
            else Debug.Log($"Player doesn't have ingredient {ingredientToCook.itemName}".ToColorString("red"));
        }

        public override void OnInterfaceOpen(GameObject gui)
        {
            var quickTimeEvent = gui.GetComponentInChildren<CookingQTE>();

            quickTimeEvent.onCorrectPressEvent.AddListener(onCorrectPress);
            quickTimeEvent.onWrongPressEvent.AddListener(onWrongPress);

            quickTimeEvent.onQTEStop.AddListener(() => GameManager.Instance.inventory.Remove(ingredientToCook));

            quickTimeEvent.StartQuickTimeEvent();
        }

        public override void OnInterfaceClose(GameObject gui) { }

        protected override void OnEnterTrigger(Collider other) { }

        protected override void OnExitTrigger(Collider other) { }
    }
}
