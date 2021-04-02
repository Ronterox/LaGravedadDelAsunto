using Inventory_System;
using Plugins.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GUI.Minigames.Cook_Plate
{
    public class PlatesMenu : UICarousel
    {
        public void SetupCarousel(Item[] plates, UnityAction<int> onClickAction)
        {
            for (var i = 0; i < plates.Length; i++)
            {
                int positionCopy = i;
                CreateElement(i, i == 0).Setup(plates[i]).onClick.AddListener(() => onClickAction.Invoke(positionCopy));
            }

            EventSystem.current.SetSelectedGameObject(SelectedElement.gameObject);
            Debug.Log("Selected => " + EventSystem.current.currentSelectedGameObject);
        }
    }
}
