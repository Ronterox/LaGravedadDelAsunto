using General.ObjectPooling;
using General.Utilities;
using UnityEngine;

namespace Minigames
{
    public class PickIngredientsMinigame : GUIInteractable
    {
        public Transform playerTransform;
        [Space] public RandomAutoObjectPooler[] autoSpawnersGameObject;

        private Vector3 m_PlayerStartPos;

        private void Start() => m_PlayerStartPos = playerTransform.position;

        private void SetSpawnersActive(bool active)
        {
            foreach (RandomAutoObjectPooler o in autoSpawnersGameObject)
            {
                o.gameObject.SetActive(active);
                if (active) o.RestartObjectsPosition();
            }
            playerTransform.gameObject.SetActive(active);
        }

        protected override void OnEnterTrigger(Collider other) { }

        protected override void OnExitTrigger(Collider other) { }

        public override void OnInterfaceOpen()
        {
            playerTransform.position = m_PlayerStartPos;
            SetSpawnersActive(true);
        }

        public override void OnInterfaceClose() => SetSpawnersActive(false);
    }
}
