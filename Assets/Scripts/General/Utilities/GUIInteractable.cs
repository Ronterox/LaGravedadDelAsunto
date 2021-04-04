using Managers;
using UnityEngine;

namespace General.Utilities
{
    public abstract class GUIInteractable : Interactable
    {
        public GameObject menuGameObject;

        public override void Interact() => OpenInterface();

        public abstract void OnInterfaceOpen();

        public abstract void OnInterfaceClose();

        public void OpenInterface() => GUIManager.Instance.OpenGUIMenu(menuGameObject, OnInterfaceOpen, OnInterfaceClose, false, false, true);
    }
}
