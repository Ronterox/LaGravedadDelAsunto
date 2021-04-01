using Inventory_System;
using Plugins.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GUI
{
    public class PlatesMenu : UICarousel
    {
        //TODO: Pass plates through minigame, and just call method, and then delete the custom editor
        public Item[] plates;
        public void SetupCarousel(UnityAction<Item> onClickAction)
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
