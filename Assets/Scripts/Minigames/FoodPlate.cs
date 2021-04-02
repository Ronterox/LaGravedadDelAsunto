using System.Linq;
using General;
using Inventory_System;
using Managers;
using UnityEngine;

namespace Minigames
{
    [CreateAssetMenu(fileName = "New food plate", menuName = "Penguins Mafia/Items/Food Plate")]
    public class FoodPlate : Item
    {
        [Header("Food Plate Settings")]
        public StatusEffect statusEffect;
        public Item[] ingredients;

        public bool isPlateIngredient(Item ingredient) => ingredient.itemType == ItemType.Ingredient && ingredients.Any(item => item == ingredient);

        public override void Use()
        {
            statusEffect.ApplyStatus();
            GameManager.Instance.inventory.Remove(this);
        }
    }
}
