using Minigames;
using Plugins.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.Minigames.Cook_Plate
{
    public class PlatesMenuElement : UICarouselElement
    {
        public FoodPlate plate;
        [Space]
        public Image plateImage;
        public TMP_Text plateName;

        public override UICarouselElement Setup(params object[] parameters)
        {
            if (parameters != null && parameters.Length > 0) plate = parameters[0] as FoodPlate;
            if (!plate) return this;
            if (plateImage) plateImage.sprite = plate.icon;
            if (plateName) plateName.text = plate.itemName;
            return this;
        }
    }
}
