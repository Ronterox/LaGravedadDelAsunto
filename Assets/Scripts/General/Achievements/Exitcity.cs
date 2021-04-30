using Managers;
using UnityEngine;

namespace General.Achievements
{
    public class Exitcity : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") )
            {
                ArchievementsManager.Instance.UpdateAchievement("achievement1", 1);
                gameObject.SetActive(false);
            }
        }
    }
}
