using Managers;
using Plugins.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Karma_System
{
    public class Karmabar : MonoBehaviour
    {
        public Slider slider;

        [Header("Animations Settings")]
        public float secondsBetweenBarMove = 1f;
        public float lerpSpeed;

        private WaitForSeconds m_WaitForSeconds;

        private void Awake() => m_WaitForSeconds = new WaitForSeconds(secondsBetweenBarMove);

        public void SetKarmaBar(int min, int max, int value = 0)
        {
            slider.minValue = min;
            slider.maxValue = max;
            slider.value = value;
        }

        public void AnimateKarma(int from, int to)
        {
            slider.value = from;

            bool WhileCondition() => !slider.value.Approximates(to);

            void LoopAction() => slider.value = Mathf.Lerp(slider.value, to, lerpSpeed);

            void OnceFinishAnimation() => GUIManager.Instance.RemoveUI(gameObject);

            StartCoroutine(UtilityMethods.FunctionCycleCoroutine(WhileCondition, LoopAction, m_WaitForSeconds, () => Destroy(gameObject, 5f), OnceFinishAnimation));
        }
    }
}
