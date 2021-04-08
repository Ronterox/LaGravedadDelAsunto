using UnityEngine;

namespace GUI
{
    public class PointerManager : MonoBehaviour
    {
        public KeyCode cursorKey;
        
        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

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
    }
}
