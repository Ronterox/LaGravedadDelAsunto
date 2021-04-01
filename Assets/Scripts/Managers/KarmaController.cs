using UnityEngine;
using UnityEngine.UI;
using Plugins.Tools;

namespace Managers
{
    public class KarmaController : MonoBehaviour
    {
        public int maxKarmaValue = 50;
        public int karma;

        public Slider karmaBar;
        public CanvasGroup karmaBarCanvasGroup;

        [Header("Animations Settings")]
        public float secondsBetweenBarMove = 1f;
        public float lerpSpeed;
        
        private WaitForSeconds m_WaitForSeconds;
        private Coroutine m_CurrentCoroutine;

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

            GameManager.Instance.guiManager.AnimateAlpha(karmaBarCanvasGroup, 1f, null, AnimateKarmaChange);
        }

        private void AnimateKarmaChange() =>
            m_CurrentCoroutine = StartCoroutine(
                UtilityMethods.FunctionCycleCoroutine(
                    () => !karmaBar.value.Approximates(karma),
                    () => karmaBar.value = Mathf.Lerp(karmaBar.value, karma, lerpSpeed),
                    m_WaitForSeconds, null,
                    () => GameManager.Instance.guiManager.AnimateAlpha(karmaBarCanvasGroup, 0f, null, () => karmaBar.gameObject.SetActive(false)))
            );
    }
}
