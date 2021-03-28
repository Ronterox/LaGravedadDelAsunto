using System.Collections;
using Player;
using UnityEngine;

namespace General.Utilities
{
    public abstract class GUIInteractable : Interactable
    {
        public CanvasGroup guiCanvasGroup;
        [Range(0, 1)] public float alphaAnimationSpeed;

        private Coroutine m_CurrentCoroutine;
        private bool m_GUIStarted;

        public override void Interact() => OpenInterface();

        public abstract void OnInterfaceOpen();

        public abstract void OnInterfaceClose();

        public void OpenInterface() => SetInterfaceActive(true);

        public void ExitInterface() => SetInterfaceActive(false);

        private void SetInterfaceActive(bool setActive)
        {
            StopCurrentCoroutine();
            m_CurrentCoroutine = StartCoroutine(AlphaCoroutine(setActive ? 1f : 0, setActive));
            m_GUIStarted = setActive;
        }

        private void StopCurrentCoroutine()
        {
            if (m_CurrentCoroutine != null) StopCoroutine(m_CurrentCoroutine);
        }

        protected override void Update()
        {
            base.Update();
            if (m_GUIStarted && PlayerInput.Instance.Pause) ExitInterface();
        }

        private IEnumerator AlphaCoroutine(float objectiveAlpha, bool isInteractable)
        {
            while (Mathf.Abs(guiCanvasGroup.alpha - objectiveAlpha) > 0.01f)
            {
                guiCanvasGroup.alpha = Mathf.Lerp(guiCanvasGroup.alpha, objectiveAlpha, alphaAnimationSpeed);
                yield return null;
            }
            guiCanvasGroup.interactable = isInteractable;
            PlayerController.Instance.BlockMovement(isInteractable);
            m_CurrentCoroutine = null;

            if (isInteractable) OnInterfaceOpen();
            else OnInterfaceClose();
        }
    }
}
