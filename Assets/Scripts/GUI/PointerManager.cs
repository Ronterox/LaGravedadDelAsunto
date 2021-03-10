using UnityEngine;

namespace GUI
{
    public class PointerManager : MonoBehaviour
    {
        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt)) SetCursorActive(false);
            else if (Input.GetKeyUp(KeyCode.LeftAlt)) SetCursorActive();
        }

        private void SetCursorActive(bool confine = true)
        {
            Cursor.visible = !confine;
            Cursor.lockState = confine ? CursorLockMode.Confined : CursorLockMode.None;
        }
    }
}
