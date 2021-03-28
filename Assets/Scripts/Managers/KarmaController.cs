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
        public CanvasGroup karmaBarCanvasGroup;

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

            karma += increment;
            if (karma > maxKarmaValue) karma = maxKarmaValue;
            else if (karma < -maxKarmaValue) karma = -maxKarmaValue;
            
            karmaBar.gameObject.SetActive(true);
            
            GameManager.Instance.guiManager.AnimateAlpha(karmaBarCanvasGroup, 1f, () => m_CurrentCoroutine = StartCoroutine(KarmaCoroutine()));
        }

        private IEnumerator KarmaCoroutine()
        {
            while (Math.Abs(karmaBar.value - karma) > 0.01f)
            {
                karmaBar.value = Mathf.Lerp(karmaBar.value, karma, lerpSpeed);
                print(Mathf.Lerp(karmaBar.value, karma, lerpSpeed));
                print(karma);
                yield return m_WaitForSeconds;
            }

            GameManager.Instance.guiManager.AnimateAlpha(karmaBarCanvasGroup, 0f, () => karmaBar.gameObject.SetActive(false));
        }
    }
}
