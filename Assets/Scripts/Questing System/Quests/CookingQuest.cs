using General.Utilities;
using GUI.Minigames.Cook_Plate;
using Managers;
using Minigames;
using Plugins.Tools;
using TMPro;
using UnityEngine;

namespace Questing_System.Quests
{
    public class CookingQuest : Quest
    {
        public GameObject platesMenuGameObject;
        [Space]
        public FoodPlate[] availablePlates;
        public CookingQTEInteractable[] cookingQteInteractables;

        [Header("Visual Feedback")]
        public GameObject plateCardTemplate;
        private ImageTextCard m_PlateCard;
        [Space]
        public TMP_Text timerText;

        [Header("Settings")]
        public float secondsToCook = 180f;
        private float m_Timer;

        private int m_PlateProgress;
        private int m_PlatesCooked, m_PlatesBurned;

        private PlatesMenu m_PlatesMenu;
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

            if (timerText) timerText.text = $"{m_Timer / 60 % 60:00}:{m_Timer % 60:00}";
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
            SetPlateActive();
        }

        private void SetPlateActive(bool setActive = true)
        {
            if (setActive)
            {
                m_PlateCard = Instantiate(plateCardTemplate).GetComponent<ImageTextCard>();

                m_PlateCard.image.sprite = availablePlates[m_PlateIndex].icon;
                ProgressTextUpdate();

                GUIManager.AnimateAlpha(m_PlateCard.canvasGroup, 1f);
            }
            else GUIManager.AnimateAlpha(m_PlateCard.canvasGroup, 0f, default,() => Destroy(m_PlateCard.gameObject));
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
            SetPlateActive(false);
        }

        public void CookPlateAction() => CookPlate(COOK_INCREMENT);

        public void BurnPlateAction() => CookPlate(-COOK_INCREMENT);

        public void SelectPlate(int position)
        {
            m_PlateIndex = position;

            FoodPlate selectedPlate = availablePlates[m_PlateIndex];

            //Assigns random ingredients to cook for each interactable
            int ingredientsQuantity = selectedPlate.ingredients.Length, index = Random.Range(0, ingredientsQuantity);
            foreach (CookingQTEInteractable cookingQteInteractable in cookingQteInteractables)
            {
                cookingQteInteractable.ingredientToCook = selectedPlate.ingredients[index];
                index.ChangeValueLimited(1, ingredientsQuantity);
            }

            GUIManager.Instance.CloseGUIMenu();
        }

        protected override void OnceQuestIsDoneGood() => StopCooking();

        protected override void OnceQuestIsDoneBad() => StopCooking();

        protected override void OnceQuestStarted() =>
            GUIManager.Instance.OpenGUIMenu(platesMenuGameObject, gui =>
            {
                m_PlatesMenu = gui.GetComponent<PlatesMenu>();
                m_PlatesMenu.SetupCarousel(availablePlates, SelectPlate);
            }, x => StartCooking());

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

        private void ProgressTextUpdate() => m_PlateCard.tmpText.text = 100 - Mathf.Abs(m_PlateProgress) + (m_PlateProgress < 0 ? "% left to be burned!" : "% left to be cooked!");

        public void CookPlate(int cook)
        {
            m_PlateProgress += cook;

            UpdateQuestState(m_PlateProgress <= BURNED_FOOD_HP, m_PlateProgress >= COOKED_FOOD_HP);
        }
    }
}
