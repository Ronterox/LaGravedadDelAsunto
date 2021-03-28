namespace General.Minigames
{
    public class KitchenInteractable : Interactable
    {
        public PickIngredientsMinigame pickIngredientsMinigame;
        public override void Interact()
        {
            base.Interact();
            pickIngredientsMinigame.EnterMinigame();
        }
    }
}
