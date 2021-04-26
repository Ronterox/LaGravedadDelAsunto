using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArchievementsManager : MonoBehaviour
{
    public static ArchievementsManager archievementsManagerInstance;

    [System.Serializable]
    public class Achievement
    {
        public Sprite image;
        public string title;
        public string description;
        public string id;
        public int current;
        public int goal;
        public bool unlocked;
     
    }
    [SerializeField]
    public Achievement[] achievements;

    public GameObject achievemntobj;

    private void Awake()
    {
        archievementsManagerInstance = this;
    }


    public void updateAchievement(string id,int value)
    {
        Achievement achievement = achievements.FirstOrDefault(x => x.id == id);
        if (!achievement.unlocked)
        {
            achievement.current += value;
            if (achievement.current >= achievement.goal)
            {
                achievement.current = achievement.goal;
                achievement.unlocked = true;
                StartCoroutine(ShowAchievement(achievement));
            }
        }
    }

    IEnumerator ShowAchievement(Achievement achievement)
    {
        achievemntobj.SetActive(true);
        Image icon = achievemntobj.transform.Find("Image").GetComponent<Image>();
        icon.sprite = achievement.image;

        TextMeshProUGUI title = achievemntobj.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        title.text = achievement.title;

       TextMeshProUGUI description= achievemntobj.transform.Find("Description").GetComponent<TextMeshProUGUI>();
        description.text = achievement.description;
        yield return new WaitForSeconds(5);
        achievemntobj.SetActive(false);
    }



}
