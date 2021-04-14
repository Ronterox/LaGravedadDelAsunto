using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUI
{

    public class TravellingUI : MonoBehaviour
    {
        public void Pause()
        {
            GUIManager.Instance.RemoveUIInstantly(gameObject);
            GameManager.Instance.PauseGame();           
        }

        public void OpenInventory()
        {
            GUIManager.Instance.RemoveUIInstantly(gameObject);
            GameManager.Instance.inventory.OpenInventory();
        }
    }
}
