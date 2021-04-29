using Managers;
using Plugins.Tools;
using UnityEngine;

namespace General.Interactables
{
    public class VendorInteractable : ChestInteractable
    {
        [Header("Vendor Settings")]
        public int requiredKarma;

        public override void Interact()
        {
            GameManager game = GameManager.Instance;
            
            if (game.karmaController.karma >= requiredKarma) base.Interact();
            else game.dialogueManager.Type("Sorry but I don't feel like you have enough karma".ToColorString("red"), helpMessagePosition.position);
        }

        protected override void OnEnterTrigger(Collider other) => GameManager.Instance.dialogueManager.Type("Welcome, welcome!", helpMessagePosition.position);

        protected override void OnExitTrigger(Collider other) => GameManager.Instance.dialogueManager.Type("Goodbye!", helpMessagePosition.position);
    }
}
