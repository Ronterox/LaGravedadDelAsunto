using Inventory_System;
using Plugins.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GUI
{
    public class PlatesMenu : UICarousel
    {
        public Item[] plates;

        protected override void Start()
        {
            base.Start();
            SetupCarousel();
        }

        private void SetupCarousel()
        {
            for (var i = 0; i < plates.Length; i++) CreateElement(i, i == 0).Setup(plates[i]);

            EventSystem.current.SetSelectedGameObject(SelectedElement.gameObject);
            Debug.Log("Selected => " + EventSystem.current.currentSelectedGameObject);
        }
    }
}
