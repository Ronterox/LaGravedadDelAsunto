using System.Collections.Generic;
using Cameras;
using Managers;
using Plugins.Tools;
using UnityEngine;

namespace Inventory_System
{
    public class Inventory : MonoBehaviour
    {
        public delegate void OnItemChanged();

        public OnItemChanged onItemChangedCallback;
        public int space = 20;
        public List<Item> items = new List<Item>();
        public float force;
        public float offset;


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
            Vector3 direction = UtilityMethods.GetRandomDirection(true, false);
            GameObject obj = Instantiate(item.itemRef, CameraManager.Instance.playerTransform.position + direction * offset, Quaternion.identity);
            obj.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
            Remove(item);
        }
    }
}
