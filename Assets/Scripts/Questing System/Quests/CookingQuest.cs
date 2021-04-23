using GUI.Minigames.Cook_Plate;
using Managers;
using Minigames;
using Plugins.Tools;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

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
        public GameObject timerGameObject;
        private GameObject m_TimerInstance;

        [Header("Settings")]
        public float secondsToCook = 180f;

        private int m_PlateProgress;
        private int m_PlatesCooked, m_PlatesBurned;

        private PlatesMenu m_PlatesMenu;

        private int m_PlateIndex = -1;

        private const int COOKED_FOOD_HP = 100, BURNED_FOOD_HP = -100;
        private const int COOKED_LIMIT = 3, BURNED_LIMIT = 2;

        private const int COOK_INCREMENT = 10;

        public void StartCooking()
        {
            foreach (CookingQTEInteractable qte in cookingQteInteractables)
            {
                qte.onCorrectPress = CookPlateAction;
                qte.onWrongPress = BurnPlateAction;   
            }
            SetTimerActive();
            SetPlateActive();
        }

        private void SetPlateActive(bool setActive = true)
        {
            if (setActive)
            {
                m_PlateCard = GUIManager.Instance.InstantiateUI(plateCardTemplate, .8f).GetComponent<ImageTextCard>();

                m_PlateCard.image.sprite = availablePlates[m_PlateIndex].icon;
                m_PlateCard.canvasGroup.interactable = false;

                ProgressTextUpdate();
            }
            else GUIManager.Instance.RemoveUI(m_PlateCard.gameObject);
        }

        private void SetTimerActive(bool setActive = true)
        {
            if (setActive)
            {
                m_TimerInstance = GUIManager.Instance.InstantiateUI(timerGameObject);

                var timerUI = m_TimerInstance.GetComponent<TimerUI>();

                timerUI.events.onTimerEnd.AddListener(() => CookPlate(-100));

                timerUI.timerTime = secondsToCook;

                timerUI.resetOnEnd = true;

                timerUI.StartTimer();
            }
            else GUIManager.Instance.RemoveUI(m_TimerInstance);
        }

        public void StopCooking()
        {
            m_PlateIndex = -1;
            SetTimerActive(false);
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

        protected override void OnceQuestStarted() => ShowSelectPlateMenu();

        public void ShowSelectPlateMenu()
        {
            void SetPlatesToSelect(GameObject gui)
            {
                m_PlatesMenu = gui.GetComponent<PlatesMenu>();
                m_PlatesMenu.SetupCarousel(availablePlates, SelectPlate);
            }

            void CheckIfPlateSelected(GameObject x)
            {
                if (m_PlateIndex != -1) StartCooking();
            }

            GUIManager.Instance.OpenGUIMenu(platesMenuGameObject, new UIOptions(null, SetPlatesToSelect, CheckIfPlateSelected));
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
