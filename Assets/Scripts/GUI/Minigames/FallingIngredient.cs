namespace GUI.Minigames
{
    public class FallingIngredient : FallingTrigger
    {
        //public Item itemIngredient;
        protected override void OnEnable()
        {
            base.OnEnable();
            onTriggerEnter.AddListener(_GivePlayer);
        }

        private void OnDisable() => onTriggerEnter.RemoveListener(_GivePlayer);

        private void _GivePlayer()
        {
            //Called inventory a give the ingredient
            //GameManager.Instance.inventory.Add(itemIngredient);
            gameObject.SetActive(false);
        }
    }
}
