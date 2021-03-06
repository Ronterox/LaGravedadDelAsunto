using System;
using Managers;
using Plugins.Tools;
using Plugins.Tools.Events;
using UnityEngine;

namespace GUI
{
    public class PointerManager : Singleton<PointerManager>, MMEventListener<MMGameEvent>
    {
        public KeyCode cursorKey;
        public GameObject buttonsGUI;
        
        private GameObject m_AuxiliarButtonGUI;
        
        private LevelLoadManager m_LevelLoadManager;

        public void OnEnable() => this.MMEventStartListening();

        public void OnDisable() => this.MMEventStopListening();

        private void Start() => m_LevelLoadManager = LevelLoadManager.Instance;

        private void LateUpdate()
        {
            if (m_LevelLoadManager.SceneIsGUI) return;
            
            if (Input.GetKeyUp(cursorKey) && m_AuxiliarButtonGUI)
            {
                GUIManager.Instance.RemoveUI(m_AuxiliarButtonGUI);
                SetCursorActive(false);
            }
            if (GameManager.Instance.GameIsPaused || GUIManager.Instance.IsGuiOpened) return;

            if (!Input.GetKeyDown(cursorKey)) return;
            
            m_AuxiliarButtonGUI = GUIManager.Instance.InstantiateUI(buttonsGUI);
            SetCursorActive();
        }

        public void SetCursorActive(bool active = true)
        {
            Cursor.visible = active;
            Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
        }

        public void OnMMEvent(MMGameEvent eventType)
        {
            if (eventType.Equals(MMGameEvent.LOAD)) SetCursorActive(LevelLoadManager.Instance.SceneIsGUI);
        }
    }
}
