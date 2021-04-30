using General.Utilities;
using Inventory_System;
using Managers;
using Plugins.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Minigames
{
    public class CookingQTEInteractable : GUIInteractable
    {
        [Space]
        public Item ingredientToCook;
        public UnityAction onWrongPress, onCorrectPress;
        
        [Header("Feedback")]
        public TMP_Text tmpText;

        public override void Interact()
        {
            if (GameManager.Instance.inventory.Has(ingredientToCook)) base.Interact();
            else GameManager.Instance.dialogueManager.TypeInto(tmpText, $"You don't have ingredient {ingredientToCook.itemName} yet!".ToColorString("red"));
        }

        public override void OnInterfaceOpen(GameObject gui)
        {
            var quickTimeEvent = gui.GetComponentInChildren<QuickTimeEvent>();

            quickTimeEvent.onCorrectPressEvent.AddListener(onCorrectPress);
            quickTimeEvent.onWrongPressEvent.AddListener(onWrongPress);

            quickTimeEvent.onQTEStop.AddListener(() =>
            {
                GameManager.Instance.inventory.Remove(ingredientToCook);
                GUIManager.Instance.CloseGUIMenu();
            });

            quickTimeEvent.StartQuickTimeEvent();
        }

        public override void OnInterfaceClose(GameObject gui) { }

        protected override void OnEnterTrigger(Collider other) { }

        protected override void OnExitTrigger(Collider other) { }
    }
}
