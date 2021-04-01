using UnityEngine;

namespace Inventory_System
{
    [CreateAssetMenu(fileName = "New item", menuName = "Penguins Mafia/Item")]
    public class Item : ScriptableObject
    {
        public string itemName = "New Item";
        public ItemType itemType;

        public Sprite icon;
        public GameObject itemRef;

        public virtual void Use() => Debug.Log("used");
    }

    public enum ItemType { Ingredient, Weapon, Consumable }

}
