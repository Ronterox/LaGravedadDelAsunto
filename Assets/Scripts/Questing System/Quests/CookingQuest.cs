using DG.Tweening;
using General.Utilities;
using GUI.Minigames.Cook_Plate;
using Managers;
using Minigames;
using Player;
using Plugins.DOTween.Modules;
using Plugins.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Questing_System.Quests
{
    public class CookingQuest : Quest
    {
        public PlatesMenu platesMenu;
        public FoodPlate[] availablePlates;

        public CookingQTEInteractable[] cookingQteInteractables;

        [Header("Visual Feedback")]
        public Image plateImage;
        public TMP_Text plateProgressText, timerText;

        [Header("Settings")]
        public float secondsToCook = 180f;
        private float m_Timer;

        private int m_PlateProgress;
        private int m_PlatesCooked, m_PlatesBurned;

        private bool m_GameStarted;
        private int m_PlateIndex;

        private const int COOKED_FOOD_HP = 100, BURNED_FOOD_HP = -100;
        private const int COOKED_LIMIT = 3, BURNED_LIMIT = 2;

        private const int COOK_INCREMENT = 10;

        private void Update()
        {
            if (!m_GameStarted) return;
            if (m_Timer <= 0)
            {
                m_Timer = secondsToCook;
                CookPlate(-100);
            }
            else m_Timer -= Time.deltaTime;

            if(timerText) timerText.text = $"{m_Timer / 60 % 60:00}:{m_Timer % 60:00}";
        }

        public void StartCooking()
        {
            m_GameStarted = true;
            m_Timer = secondsToCook;

            foreach (CookingQTEInteractable cookingQteInteractable in cookingQteInteractables)
            {
                QuickTimeEvent cookingQTE = cookingQteInteractable.quickTimeEvent;
                cookingQTE.onCorrectPressEvent.AddListener(CookPlateAction);
                cookingQTE.onWrongPressEvent.AddListener(BurnPlateAction);
            }
        }

        public void StopCooking()
        {
            m_GameStarted = false;

            foreach (CookingQTEInteractable cookingQteInteractable in cookingQteInteractables)
            {
                QuickTimeEvent cookingQTE = cookingQteInteractable.quickTimeEvent;
                cookingQTE.onCorrectPressEvent.RemoveListener(CookPlateAction);
                cookingQTE.onWrongPressEvent.RemoveListener(BurnPlateAction);
            }
        }

        public void CookPlateAction() => CookPlate(COOK_INCREMENT);

        public void BurnPlateAction() => CookPlate(-COOK_INCREMENT);

        public void SelectPlate(int position)
        {
            m_PlateIndex = position;

            FoodPlate selectedPlate = availablePlates[position];

            plateImage.sprite = selectedPlate.icon;
            ProgressTextUpdate();

            //TODO: change this to close of gui manager once gui manager is done
            var parentCanvas = platesMenu.gameObject.GetComponentInParent<CanvasGroup>();
            parentCanvas.interactable = false;

            parentCanvas.DOFade(0, .25f).OnComplete(() =>
            {
                //This is messed up lmao TODO: FIX THIS!
                plateImage.gameObject.transform.parent.gameObject.SetActive(true);
                platesMenu.gameObject.SetActive(false);

                parentCanvas.DOFade(1f, .25f);
                //

                PlayerInput.Instance.UnlockInput();
                StartCooking();
            });

            int ingredientsQuantity = selectedPlate.ingredients.Length, index = Random.Range(0, ingredientsQuantity);
            foreach (CookingQTEInteractable cookingQteInteractable in cookingQteInteractables)
            {
                cookingQteInteractable.ingredientToCook = selectedPlate.ingredients[index];
                index.ChangeValueLimited(1, ingredientsQuantity);
            }
        }

        protected override void OnceQuestIsDoneGood() => StopCooking();

        protected override void OnceQuestIsDoneBad() => StopCooking();

        protected override void OnceQuestStarted()
        {
            PlayerInput.Instance.BlockInput();
            platesMenu.SetupCarousel(availablePlates, SelectPlate);
        }

        private void UpdateQuestState(bool foodBurned, bool foodCooked)
        {
            if (foodBurned || foodCooked)
            {
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
                m_PlateProgress = 0;
                m_Timer = secondsToCook;
            }
            ProgressTextUpdate();
        }

        private void ProgressTextUpdate() => plateProgressText.text = 100 - Mathf.Abs(m_PlateProgress) + (m_PlateProgress < 0 ? "% left to be burned!" : "% left to be cooked!");

        public void CookPlate(int cook)
        {
            m_PlateProgress += cook;

            UpdateQuestState(m_PlateProgress <= BURNED_FOOD_HP, m_PlateProgress >= COOKED_FOOD_HP);
        }
    }
}
