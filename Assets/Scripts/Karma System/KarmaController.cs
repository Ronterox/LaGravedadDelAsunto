using UnityEngine;
using UnityEngine.UI;

namespace Karma_System
{
    public class KarmaController : MonoBehaviour
    {
        public int karma = 50;
        public Slider karmaBar;

        private void Start() => karmaBar.value = karma;

        //Only for test if works after implemented.
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X)) ChangeKarma(-1);
            if (Input.GetKeyDown(KeyCode.Z)) ChangeKarma(+1);
        }

        public void ChangeKarma(int increment) => karmaBar.value = karma += increment;
    }
}
