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

        private Coroutine m_CurrentCoroutine;
        private WaitForSeconds m_WaitForSeconds;
        public float secondsBetweenBarMove = 1f;
        public float lerpSpeed=0;
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
            m_CurrentCoroutine = StartCoroutine(KarmaCoroutine(karmaBar,increment, lerpSpeed,true));
          
        }

        private IEnumerator KarmaCoroutine(Slider sliderKarma,int increment, float lerp, bool disappearAfter = false, float disappearDelay = 2f)
        {
            sliderKarma.gameObject.SetActive(true);
            var endValue = karma += increment;
            print(sliderKarma.maxValue);
            print(sliderKarma.minValue);

            while(sliderKarma.value!=endValue)
            {
                sliderKarma.value=Mathf.Lerp(sliderKarma.value,endValue,lerp);
              
                
                yield return m_WaitForSeconds;
            }
            if (!disappearAfter) yield break;
            yield return new WaitForSeconds(disappearDelay);
            sliderKarma.gameObject.SetActive(false);
        }
    }
}
