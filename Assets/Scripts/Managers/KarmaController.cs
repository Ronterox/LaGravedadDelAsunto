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

        //Only for test if works after implemented.
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X)) ChangeKarma(-1);
            if (Input.GetKeyDown(KeyCode.Z)) ChangeKarma(+1);
        }

        public void ChangeKarma(int increment) => karmaBar.value = karma += increment;
    }
}
