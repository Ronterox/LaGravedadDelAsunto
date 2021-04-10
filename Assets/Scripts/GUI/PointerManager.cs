using System.Linq;
using Managers;
using Plugins.Properties;
using Plugins.Tools;
using UnityEngine;

namespace GUI
{
    public class PointerManager : Singleton<PointerManager>, MMEventListener<MMGameEvent>
    {
        public KeyCode cursorKey;

        public void OnEnable() => this.MMEventStartListening();

        public void OnDisable() => this.MMEventStopListening();

        private void LateUpdate()
        {
            if (Input.GetKeyDown(cursorKey)) SetCursorActive();
            else if (Input.GetKeyUp(cursorKey)) SetCursorActive(false);
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
