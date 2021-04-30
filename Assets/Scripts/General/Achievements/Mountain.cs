using Managers;
using UnityEngine;

namespace General.Achievements
{
    public class Mountain : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ArchievementsManager.Instance.UpdateAchievement("achievement2", 1);
                gameObject.SetActive(false);
            }
        }
    }
}
