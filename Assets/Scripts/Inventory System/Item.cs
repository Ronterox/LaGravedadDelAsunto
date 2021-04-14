using UnityEngine;

namespace Inventory_System
{
    [CreateAssetMenu(fileName = "New item", menuName = "Penguins Mafia/Items/Item")]
    public class Item : ScriptableObject
    {
        [Header("Item Settings")]
        public string itemName = "New Item";
        public ItemType itemType;

        [Space]
        public Sprite icon;
        public GameObject itemRef;

        public virtual void Use() => Debug.Log("used");
    }

    public enum ItemType { Ingredient, Weapon, Consumable }

}
