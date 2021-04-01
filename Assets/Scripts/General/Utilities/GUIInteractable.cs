using Managers;
using Player;
using UnityEngine;

namespace General.Utilities
{
    public abstract class GUIInteractable : Interactable
    {
        public CanvasGroup guiCanvasGroup;
        private bool m_GUIStarted;

        public override void Interact() => OpenInterface();

        public abstract void OnInterfaceOpen();

        public abstract void OnInterfaceClose();

        public void OpenInterface() => SetInterfaceActive(true);

        public void ExitInterface() => SetInterfaceActive(false);

        private void SetInterfaceActive(bool setActive)
        {
            GameManager.Instance.guiManager.AnimateAlpha(guiCanvasGroup, setActive ? 1f : 0, null,() =>
            {
                guiCanvasGroup.interactable = setActive;
                PlayerController.Instance.BlockMovement(setActive);

                if (setActive) OnInterfaceOpen();
                else OnInterfaceClose();
            });
            
            m_GUIStarted = setActive;
        }

        protected override void Update()
        {
            base.Update();
            if (m_GUIStarted && PlayerInput.Instance.Pause) ExitInterface();
        }
    }
}
