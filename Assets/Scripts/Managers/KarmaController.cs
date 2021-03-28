using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Managers
{
    public class KarmaController : MonoBehaviour
    {
        public int maxKarmaValue = 50;
        public int karma;

        public Slider karmaBar;
        public CanvasGroup karmabarCanvasGroup;

        private WaitForSeconds m_WaitForSeconds;
        private Coroutine m_CurrentCoroutine;

        public float secondsBetweenBarMove = 1f;
        public float lerpSpeed;

        private void Awake()
        {
            karmaBar.minValue = -maxKarmaValue;
            karmaBar.maxValue = maxKarmaValue;
            karmaBar.value = karma;

            m_WaitForSeconds = new WaitForSeconds(secondsBetweenBarMove);
        }

        public void ChangeKarma(int increment)
        {
            if (m_CurrentCoroutine != null) StopCoroutine(m_CurrentCoroutine);
            m_CurrentCoroutine = StartCoroutine(KarmaCoroutine(increment, lerpSpeed));
        }

        private IEnumerator KarmaCoroutine(int increment, float lerp)
        {
            karmaBar.gameObject.SetActive(true);
            GameManager.Instance.guiManager.AnimateAlpha(karmabarCanvasGroup, 1f);

            int endValue = karma += increment;

            while (Math.Abs(karmaBar.value - endValue) > 0.01f)
            {
                karmaBar.value = Mathf.Lerp(karmaBar.value, endValue, lerp);
                yield return m_WaitForSeconds;
            }

            GameManager.Instance.guiManager.AnimateAlpha(karmabarCanvasGroup, 0f, () => karmaBar.gameObject.SetActive(false));
        }
    }
}
