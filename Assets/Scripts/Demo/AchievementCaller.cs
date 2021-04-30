using Managers;
using UnityEngine;

namespace Demo
{
    public class AchievementCaller : MonoBehaviour
    {
        public string id;
        public int increment;
        private void OnTriggerEnter(Collider other)
        {
            ArchievementsManager.Instance.UpdateAchievement(id, increment);
            gameObject.SetActive(false);
        }
    }
}
