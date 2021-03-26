using UnityEngine;

namespace Inventory_System
{
    [CreateAssetMenu(fileName = "New item", menuName = "Penguins Mafia/Item")]
    public class Item : ScriptableObject
    {
        public string itemName = "New Item";
        public Sprite icon = null;
        public GameObject itemRef;

        public void testMessage()
        {
            Debug.Log("Picking" + itemName);
        }

        public virtual void Use()
        {
            Debug.Log("used");
        }
    }
}

