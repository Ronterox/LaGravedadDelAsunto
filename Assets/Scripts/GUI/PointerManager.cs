using UnityEngine;

namespace GUI
{
    public class PointerManager : MonoBehaviour
    {
        public KeyCode showPointerKey;
        
        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void LateUpdate()
        {
            if (Input.GetKeyDown(showPointerKey)) SetCursorActive();
            else if (Input.GetKeyUp(showPointerKey)) SetCursorActive(false);
        }

        private void SetCursorActive(bool active = true)
        {
            Cursor.visible = active;
            Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
