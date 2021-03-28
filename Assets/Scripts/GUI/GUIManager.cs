using System;
using System.Collections;
using UnityEngine;

namespace GUI
{
    public class GUIManager : MonoBehaviour
    {
        [Range(0, 1f)] public float alphaAnimationSpeed;
        private Coroutine m_CurrentCoroutine;

        public void AnimateAlpha(CanvasGroup canvasGroup, float objectiveAlpha, Action onceFinishAnimation = null, Action onceStartAnimation = null)
        {
            if (m_CurrentCoroutine != null) StopCoroutine(m_CurrentCoroutine);
            m_CurrentCoroutine = StartCoroutine(AlphaCoroutine(canvasGroup, objectiveAlpha, onceFinishAnimation, onceStartAnimation));
        }

        private IEnumerator AlphaCoroutine(CanvasGroup canvasGroup, float objectiveAlpha, Action onceFinishAnimation, Action onceStartAnimation)
        {
            onceStartAnimation?.Invoke();
            while (Mathf.Abs(canvasGroup.alpha - objectiveAlpha) > 0.01f)
            {
                canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, objectiveAlpha, alphaAnimationSpeed);
                yield return null;
            }
            onceFinishAnimation?.Invoke();
        }
    }
}
