using UnityEngine;

namespace Inventory_System
{
    [CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        new public string name = "New Item";
        public Sprite icon = null;

        public void testMessage()
        {
            Debug.Log("Picking" + name);
        }

        public virtual void Use()
        {
            Debug.Log("used");
        }
    }
}

