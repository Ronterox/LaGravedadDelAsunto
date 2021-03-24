using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Inventory_System
{
    public class Inventory : MonoBehaviour
    {
        public delegate void OnItemChanged();

        public OnItemChanged onItemChangedCallback;
        public int space = 20;
        public List<Item> items = new List<Item>();

        public bool Add(Item item)
        {
            if (items.Count >= space)
            {
                Debug.Log("Inventory full");
                return false;
            }
            items.Add(item);
            onItemChangedCallback?.Invoke();
            return true;
        }

        public void Remove(Item item)
        {
            items.Remove(item);
            onItemChangedCallback?.Invoke();
        }

        public void Drop(Item item)
        {
            Instantiate(item, GameManager.Instance.playerPos.position, Quaternion.identity);
            Remove(item);
        }
    }
}
