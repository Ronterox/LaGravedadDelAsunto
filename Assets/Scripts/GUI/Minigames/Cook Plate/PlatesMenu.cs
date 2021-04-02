using Minigames;
using Plugins.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GUI.Minigames.Cook_Plate
{
    public class PlatesMenu : UICarousel
    {
        //TODO: try to reuse same 3 plates for shown of all, has to remove use of lambda to be able to remove listener
        public void SetupCarousel(FoodPlate[] plates, UnityAction<int> onClickAction)
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
