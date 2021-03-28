using System;
using System.Collections;
using General.ObjectPooling;
using Player;
using UnityEngine;

namespace General.Minigames
{
    public class PickIngredientsMinigame : MonoBehaviour
    {
        public CanvasGroup minigameCanvasGroup;
        public Transform playerTransform;
        
        [Space] public RandomAutoObjectPooler[] autoSpawnersGameObject;
        [Range(0, 1)] public float alphaAnimationSpeed;

        private Coroutine m_CurrentCoroutine;
        private Vector3 playerStartPos;

        private bool m_MinigameStarted;

        private void Start() => playerStartPos = playerTransform.position;

        public void EnterMinigame()
        {
            StopCurrentCoroutine();
            m_CurrentCoroutine = StartCoroutine(AlphaCoroutine(1f, true));
            playerTransform.position = playerStartPos;
            m_MinigameStarted = true;
        }

        public void ExitMinigame()
        {
            StopCurrentCoroutine();
            m_CurrentCoroutine = StartCoroutine(AlphaCoroutine(0f, false));
            m_MinigameStarted = false;
        }

        private void StopCurrentCoroutine() { if(m_CurrentCoroutine != null) StopCoroutine(m_CurrentCoroutine); }

        private void Update()
        {
            if (m_MinigameStarted && PlayerInput.Instance.Pause) ExitMinigame();
        }

        private void SetSpawnersActive(bool active)
        {
            foreach (RandomAutoObjectPooler o in autoSpawnersGameObject)
            {
                o.gameObject.SetActive(active);
                if(active) o.RestartObjectsPosition();
            }
        }

        private IEnumerator AlphaCoroutine(float objectiveAlpha, bool isInteractable)
        {
            while (Math.Abs(minigameCanvasGroup.alpha - objectiveAlpha) > 0.01f)
            {
                print(minigameCanvasGroup.alpha);
                minigameCanvasGroup.alpha = Mathf.Lerp(minigameCanvasGroup.alpha, objectiveAlpha, alphaAnimationSpeed);
                yield return null;
            }
            minigameCanvasGroup.interactable = isInteractable;
            PlayerController.Instance.BlockMovement(isInteractable);
            SetSpawnersActive(isInteractable);
            m_CurrentCoroutine = null;
        }
    }
}
