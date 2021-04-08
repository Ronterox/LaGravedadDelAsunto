using System;
using System.Collections.Generic;
using DG.Tweening;
using Player;
using Plugins.DOTween.Modules;
using Plugins.Tools;
using UnityEngine;

namespace Managers
{
    public class GUIManager : Singleton<GUIManager>
    {
        public Canvas mainCanvas;

        [Header("Menus GameObjects")]
        public GameObject pauseMenu;
        public GameObject inventoryUi;

        [Header("GUI Animation Settings")]
        [Range(0.5f, 3f)] public float alphaAnimationDuration;

        private CanvasGroup m_CurrentGUICanvasGroup;
        private GameObject m_CurrentGUI;

        private Action<GameObject> m_OnCloseGUI;
        private bool m_IsGuiOpened;

        private readonly List<GameObject> m_InstantiatedObjects = new List<GameObject>();

        private void Start()
        {
            if (mainCanvas) mainCanvas.gameObject.MoveToScene("GUI Scene");
            else Debug.Log("Main canvas is missing on GUI Manager!".ToColorString("cyan"));
        }

        private void Update()
        {
            if (m_IsGuiOpened && PlayerInput.Instance.Pause) CloseGUIMenu();
        }

        /// <summary>
        /// Static method to animated a canvas group, as a fade effect
        /// </summary>
        /// <param name="canvasGroup">the canvas group to be animated</param>
        /// <param name="objectiveAlpha">the objective to get to as de alpha value, it starts from the value it already has</param>
        /// <param name="animationDuration">the duration of the fading</param>
        /// <param name="onceFinishAnimation">once the animation is finished</param>
        public static void AnimateAlpha(CanvasGroup canvasGroup, float objectiveAlpha, float animationDuration = 0.5f, TweenCallback onceFinishAnimation = null) =>
            canvasGroup.DOFade(objectiveAlpha, animationDuration).OnComplete(onceFinishAnimation);

        /// <summary>
        /// Opens the gameObject passed as a gui on the main canvas
        /// </summary>
        /// <param name="menu">the gui to instantiate</param>
        /// <param name="beforeOpenGUI">just after instantiating the gui</param>
        /// <param name="onOpenGUI">after the gui opens with animation</param>
        /// <param name="onCloseGUI">after the gui is close</param>
        /// <param name="showPointer">shows the pointer when gui is opened</param>
        /// <param name="pauseTime">pauses the time after the gui is called</param>
        /// <param name="lockMovement">locks the movement of the player once the gui is opened</param>
        /// <param name="lockInput">locks the input of the player once the gui is opened</param>
        public void OpenGUIMenu(GameObject menu, Action<GameObject> beforeOpenGUI = null, Action<GameObject> onOpenGUI = null, Action<GameObject> onCloseGUI = null, bool showPointer = false, bool pauseTime = false, bool lockMovement = true, bool lockInput = false)
        {
            if (m_IsGuiOpened) return;

            m_CurrentGUI = Instantiate(menu, mainCanvas.transform);

            m_CurrentGUI.SetActive(true);

            beforeOpenGUI?.Invoke(m_CurrentGUI);

            m_CurrentGUICanvasGroup = m_CurrentGUI.GetComponent<CanvasGroup>();

            void Callback()
            {
                m_CurrentGUICanvasGroup.interactable = true;

                if (pauseTime) Time.timeScale = 0f;
                if (lockInput) PlayerInput.Instance.BlockInput();

                if (showPointer) GameManager.Instance.pointerManager.SetCursorActive();
                PlayerController.Instance.BlockMovement(lockMovement);

                onOpenGUI?.Invoke(m_CurrentGUI);
            }

            if (m_CurrentGUICanvasGroup && !pauseTime) AnimateAlpha(m_CurrentGUICanvasGroup, 1f, alphaAnimationDuration, Callback);
            else Callback();

            m_OnCloseGUI = onCloseGUI;

            m_IsGuiOpened = true;
        }


        /// <summary>
        /// Opens the gameObject passed as a gui on the main canvas
        /// </summary>
        /// <param name="menu">the gui to instantiate</param>
        /// <param name="beforeOpenGUI">just after instantiating the gui</param>
        /// <param name="onOpenGUI">after the gui opens with animation</param>
        /// <param name="onCloseGUI">after the gui is close</param>
        /// <param name="showPointer">shows the pointer when gui is opened</param>
        /// <param name="pauseTime">pauses the time after the gui is called</param>
        /// <param name="lockMovement">locks the movement of the player once the gui is opened</param>
        /// <param name="lockInput">locks the input of the player once the gui is opened</param>
        public void OpenGUIMenu(GameObject menu, Action beforeOpenGUI = null, Action onOpenGUI = null, Action onCloseGUI = null, bool showPointer = false, bool pauseTime = false, bool lockMovement = true, bool lockInput = false) => OpenGUIMenu(menu, x => beforeOpenGUI?.Invoke(), x => onOpenGUI?.Invoke(), x => onCloseGUI?.Invoke(), showPointer, pauseTime, lockMovement, lockInput);

        /// <summary>
        /// Closes the last gui opened
        /// </summary>
        /// <param name="animate">whether to animate with fade the gui to close</param>
        public void CloseGUIMenu(bool animate = true)
        {
            if (!m_IsGuiOpened) return;

            Time.timeScale = 1f;

            void OnceFinishAnimation()
            {
                m_CurrentGUICanvasGroup.interactable = false;

                PlayerInput.Instance.UnlockInput();

                PlayerController.Instance.BlockMovement(false);
                GameManager.Instance.pointerManager.SetCursorActive(false);

                Destroy(m_CurrentGUI);

                m_OnCloseGUI?.Invoke(m_CurrentGUI);
                m_OnCloseGUI = null;

                m_IsGuiOpened = false;
            }

            if (animate) AnimateAlpha(m_CurrentGUICanvasGroup, 0f, alphaAnimationDuration, OnceFinishAnimation);
            else OnceFinishAnimation();
        }

        /// <summary>
        /// Instantiates a game object on the main canvas
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public GameObject InstantiateUIInstantly(GameObject obj)
        {
            GameObject instance;
            m_InstantiatedObjects.Add(instance = Instantiate(obj, mainCanvas.transform));
            return instance;
        }

        /// <summary>
        /// Instantiates a game object on the main canvas with an animation
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="targetAlpha">target alpha of the animation</param>
        /// <param name="animationDuration">optional</param>
        /// <param name="onAnimationEnd">optional event</param>
        /// <returns></returns>
        public GameObject InstantiateUI(GameObject obj, float targetAlpha = 1f, float animationDuration = 0.5f, TweenCallback onAnimationEnd = null)
        {
            GameObject instance = InstantiateUIInstantly(obj);
            
            var canvasGroup = instance.GetComponent<CanvasGroup>();
            
            if (canvasGroup) AnimateAlpha(canvasGroup, targetAlpha, animationDuration, onAnimationEnd);
            else onAnimationEnd?.Invoke();
            
            return instance;
        }

        /// <summary>
        /// Removes a game object from the main canvas
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void RemoveUIInstantly(GameObject obj)
        {
            bool exist = m_InstantiatedObjects.Remove(obj);
            if (exist) Destroy(obj);
        }

        /// <summary>
        /// Removes a game object from the main canvas with an animation
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="animationDuration"></param>
        /// <param name="onceFinishAnimation"></param>
        /// <returns></returns>
        public void RemoveUI(GameObject obj, float animationDuration = 0.5f, TweenCallback onceFinishAnimation = null)
        {
            var canvasGroup = obj.GetComponent<CanvasGroup>();

            onceFinishAnimation += () => RemoveUIInstantly(obj);

            if (canvasGroup) AnimateAlpha(canvasGroup, 0f, animationDuration, onceFinishAnimation);
            else onceFinishAnimation();
        }

        /// <summary>
        /// Opens the pause menu
        /// </summary>
        /// <param name="onOpenGUI"></param>
        /// <param name="onCloseGUI"></param>
        public void OpenPauseMenu(Action onOpenGUI, Action onCloseGUI) => OpenGUIMenu(pauseMenu, null, x => onOpenGUI?.Invoke(), x => onCloseGUI?.Invoke(), true, true, false, true);

        /// <summary>
        /// Opens the inventory
        /// </summary>
        /// <param name="onOpenGUI"></param>
        /// <param name="onCloseGUI"></param>
        public void OpenInventory(Action onOpenGUI, Action onCloseGUI) => OpenGUIMenu(inventoryUi, GameManager.Instance.inventory.InitializeInventory, x => onOpenGUI?.Invoke(), x => onCloseGUI?.Invoke(), true, false, false);
    }
}
