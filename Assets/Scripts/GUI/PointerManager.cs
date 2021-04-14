using Managers;
using Plugins.Tools;
using UnityEngine;

namespace GUI
{
    public class PointerManager : Singleton<PointerManager>, MMEventListener<MMGameEvent>
    {
        public KeyCode cursorKey;
        public GameObject buttonsGUI;
        private GameObject m_AuxiliarButtonGUI;

        public void OnEnable() => this.MMEventStartListening();

        public void OnDisable() => this.MMEventStopListening();

        private void LateUpdate()
        {
            if (Input.GetKeyDown(cursorKey)) SetCursorActive();
            else if (Input.GetKeyUp(cursorKey)) SetCursorActive(false);
        }

        public void SetCursorActive(bool active = true)
        {
            if (GameManager.Instance.GameIsPaused)
            {
                return;
            }
            Cursor.visible = active;
            Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;            
            if (active)
            {                          
                m_AuxiliarButtonGUI = GUIManager.Instance.InstantiateUI(buttonsGUI);
            }
            else
            {
                GUIManager.Instance.RemoveUI(m_AuxiliarButtonGUI);
            }
        }

        public void OnMMEvent(MMGameEvent eventType)
        {
            if (eventType.Equals(MMGameEvent.LOAD)) SetCursorActive(LevelLoadManager.Instance.SceneIsGUI);
        }
    }
}
