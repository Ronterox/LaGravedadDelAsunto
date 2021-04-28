using System.Collections;
using System.Linq;
using Plugins.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class ArchievementsManager : Singleton<ArchievementsManager>
    {
        [System.Serializable]
        public struct Achievement
        {
            public string id;
            public Sprite image;
            public string title, description;
            public int current, goal;
            public bool unlocked;
        }

        [SerializeField]
        public Achievement[] achievements;

        public GameObject achievementObj;

        public void UpdateAchievement(string id, int value)
        {
            Achievement achievement = achievements.FirstOrDefault(x => x.id == id);

            if (achievement.unlocked) return;

            achievement.current += value;

            if (achievement.current < achievement.goal) return;

            achievement.current = achievement.goal;
            achievement.unlocked = true;
            
            StartCoroutine(ShowAchievement(achievement, 3));
        }

        private IEnumerator ShowAchievement(Achievement achievement, float seconds)
        {
            achievementObj.SetActive(true);
            var icon = achievementObj.transform.Find("Image").GetComponent<Image>();
            icon.sprite = achievement.image;

            var title = achievementObj.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            title.text = achievement.title;

            var description = achievementObj.transform.Find("Description").GetComponent<TextMeshProUGUI>();
            description.text = achievement.description;
            yield return new WaitForSeconds(seconds);
            achievementObj.SetActive(false);
        }


    }
}
