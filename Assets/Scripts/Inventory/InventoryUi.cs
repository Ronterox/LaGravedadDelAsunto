using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    Inventory inventory;
    public Transform itemsParent;
    InventorySlot[] slots;
    public GameObject inventoryUi;
    public static bool inInventory = false;
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUi;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

 
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (inInventory)
            {
                inventoryUi.SetActive(false);
                Time.timeScale = 1f;
                inInventory = false;
            }
            else
            {
                inventoryUi.SetActive(true);
                Time.timeScale = 0f;
                inInventory = true;
                
            }
         
        }
    }

    void UpdateUi()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }

    }
}
