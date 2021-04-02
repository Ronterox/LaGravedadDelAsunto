using Inventory_System;
using Plugins.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GUI
{
    public class PlatesMenu : UICarousel
    {
        public void SetupCarousel(Item[] plates, UnityAction<Item> onClickAction)
        {
            for (var i = 0; i < plates.Length; i++)
            {
                int positionCopy = i;
                CreateElement(i, i == 0).Setup(plates[i]).onClick.AddListener(delegate { onClickAction.Invoke(plates[positionCopy]); });
            }

            EventSystem.current.SetSelectedGameObject(SelectedElement.gameObject);
            Debug.Log("Selected => " + EventSystem.current.currentSelectedGameObject);
        }
    }
}
