using Plugins.Audio;
using UnityEngine;

namespace Demo
{
    public class OnTriggerMusic : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) => SoundManager.Instance.ResumeBackgroundMusic();
    }
}
