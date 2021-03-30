using Inventory_System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Questing_System.Quests
{
    public class CookingQuest : Quest
    {
        public Item currentPlate;

        [Header("Visual Feedback")]
        public Image plateImage;
        public TMP_Text plateProgressText;

        [Header("Settings")]
        public float secondsToCook;
        private float m_Timer;

        private int m_PlateProgress;
        private int m_PlatesCooked, m_PlatesBurned;

        private bool m_TimerStarted;

        private const int COOKED_FOOD_HP = 100, BURNED_FOOD_HP = -100;
        private const int COOKED_LIMIT = 3, BURNED_LIMIT = 2;

        private void Update()
        {
            if (!m_TimerStarted) return;
            if (m_Timer >= secondsToCook)
            {
                m_Timer = 0;
                CookPlate(-100);
            }
            else m_Timer += Time.deltaTime;
        }

        public void StartCooking()
        {
            m_TimerStarted = true;
            m_Timer = 0;
        }

        public void StopCooking() => m_TimerStarted = false;

        public void SelectPlate(Item plate)
        {
            currentPlate = plate;

            plateImage.sprite = currentPlate.icon;
            ProgressTextUpdate();

            StartCooking();
        }

        protected override void OnceQuestIsDoneGood() => StopCooking();

        protected override void OnceQuestIsDoneBad() => StopCooking();

        protected override void OnceQuestStarted() => ShowMenuOptions();

        public void ShowMenuOptions()
        {
            
        }

        private void UpdateQuestState(bool foodBurned, bool foodCooked)
        {
            if (foodBurned || foodCooked)
            {
                m_PlateProgress = 0;
                if (foodBurned)
                {
                    m_PlatesBurned++;
                    if (m_PlatesBurned >= BURNED_LIMIT) EndQuestNegative();
                }
                else
                {
                    m_PlatesCooked++;
                    if (m_PlatesCooked >= COOKED_LIMIT) EndQuestPositive();
                }
            }
            ProgressTextUpdate();
        }

        private void ProgressTextUpdate() => plateProgressText.text = 100 - Mathf.Abs(m_PlateProgress) + (m_PlateProgress < 0? " left to be burned!" : "left to be cooked!");

        public void CookPlate(int cook)
        {
            m_PlateProgress += cook;

            UpdateQuestState(m_PlateProgress <= BURNED_FOOD_HP, m_PlateProgress >= COOKED_FOOD_HP);
        }
    }
}
