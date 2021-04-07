using Managers;
using UnityEngine;

namespace General.Utilities
{
    public abstract class GUIInteractable : Interactable
    {
        [Space]
        public GameObject interfaceGameObject;

        public override void Interact() => OpenInterface();

        public abstract void OnInterfaceOpen(GameObject gui);

        public abstract void OnInterfaceClose(GameObject gui);

        public void OpenInterface() => GUIManager.Instance.OpenGUIMenu(interfaceGameObject, null, OnInterfaceOpen, OnInterfaceClose);
    }
}
