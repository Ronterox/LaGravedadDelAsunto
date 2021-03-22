using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class KarmaController : MonoBehaviour
    {
        public int maxKarmaValue = 50;
        
        public int karma;
        public Slider karmaBar;
        
        private void Awake()
        {
            karmaBar.minValue = -maxKarmaValue;
            karmaBar.maxValue = maxKarmaValue;
            karmaBar.value = karma;
        }

        public void ChangeKarma(int increment) => karmaBar.value = karma += increment;
    }
}
