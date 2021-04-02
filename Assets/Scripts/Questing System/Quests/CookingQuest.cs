using GUI.Minigames.Cook_Plate;
using Inventory_System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Questing_System.Quests
{
    public class CookingQuest : Quest
    {
        public PlatesMenu platesMenu;
        public Item[] availablePlates;

        [Header("Visual Feedback")]
        public Image plateImage;
        public TMP_Text plateProgressText;

        [Header("Settings")]
        public float secondsToCook = 180f;
        private float m_Timer;

        private int m_PlateProgress;
        private int m_PlatesCooked, m_PlatesBurned;

        private bool m_GameStarted;
        private int m_PlateIndex;

        private const int COOKED_FOOD_HP = 100, BURNED_FOOD_HP = -100;
        private const int COOKED_LIMIT = 3, BURNED_LIMIT = 2;

        private void Update()
        {
            if (!m_GameStarted) return;
            if (m_Timer >= secondsToCook)
            {
                m_Timer = 0;
                CookPlate(-100);
            }
            else m_Timer += Time.deltaTime;
        }

        public void StartCooking()
        {
            m_GameStarted = true;
            m_Timer = 0;
        }

        public void StopCooking() => m_GameStarted = false;

        public void SelectPlate(int position)
        {
            m_PlateIndex = position;

            plateImage.sprite = availablePlates[position].icon;
            ProgressTextUpdate();

            StartCooking();
        }

        protected override void OnceQuestIsDoneGood() => StopCooking();

        protected override void OnceQuestIsDoneBad() => StopCooking();
        
        protected override void OnceQuestStarted() => platesMenu.SetupCarousel(availablePlates, SelectPlate);

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
                    if (!GameManager.Instance.inventory.Add(availablePlates[m_PlateIndex])) GameManager.Instance.inventory.Drop(availablePlates[m_PlateIndex]);
                    if (m_PlatesCooked >= COOKED_LIMIT) EndQuestPositive();
                }
            }
            ProgressTextUpdate();
        }

        private void ProgressTextUpdate() => plateProgressText.text = 100 - Mathf.Abs(m_PlateProgress) + (m_PlateProgress < 0 ? " left to be burned!" : "left to be cooked!");

        public void CookPlate(int cook)
        {
            m_PlateProgress += cook;

            UpdateQuestState(m_PlateProgress <= BURNED_FOOD_HP, m_PlateProgress >= COOKED_FOOD_HP);
        }
    }
}
