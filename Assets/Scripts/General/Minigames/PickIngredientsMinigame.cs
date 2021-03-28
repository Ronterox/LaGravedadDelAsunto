using System;
using System.Collections;
using Player;
using UnityEngine;

namespace General.Minigames
{
    public class PickIngredientsMinigame : MonoBehaviour
    {
        public CanvasGroup minigameCanvasGroup;
        [Range(0, 1)] public float animationSpeed;

        private bool m_MinigameStarted;
        
        public void EnterMinigame()
        {
            StartCoroutine(AlphaCoroutine(1, true));
            m_MinigameStarted = true;
        }

        public void ExitMinigame()
        {
            StartCoroutine(AlphaCoroutine(0, false));
            m_MinigameStarted = false;
        }

        private void Update()
        {
            if (m_MinigameStarted && PlayerInput.Instance.Pause) ExitMinigame();
        }

        private IEnumerator AlphaCoroutine(float objectiveAlpha, bool isInteractable)
        {
            while (!Mathf.Approximately(minigameCanvasGroup.alpha, objectiveAlpha))
            {
                minigameCanvasGroup.alpha = Mathf.Lerp(minigameCanvasGroup.alpha, objectiveAlpha, animationSpeed);
                yield return null;
            }
            minigameCanvasGroup.interactable = isInteractable;
            PlayerController.Instance.BlockMovement(isInteractable);
        }
    }
}
