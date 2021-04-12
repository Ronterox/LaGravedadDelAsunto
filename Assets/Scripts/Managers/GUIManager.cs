using System;
using System.Collections.Generic;
using DG.Tweening;
using GUI;
using Player;
using Plugins.DOTween.Modules;
using Plugins.Tools;
using UnityEngine;

namespace Managers
{
    public struct UIOptions
    {
        public readonly bool showPointer, pauseTime, lockMovement, lockInput;

        public Action<GameObject> beforeOpenGUI, onOpenGUI, onCloseGUI;

        public void SetActions(Action<GameObject> beforeOpen, Action<GameObject> onOpen, Action<GameObject> onClose)
        {
            beforeOpenGUI = beforeOpen;
            onOpenGUI = onOpen;
            onCloseGUI = onClose;
        }
        public UIOptions(bool showPointer, bool pauseTime, bool lockMovement, bool lockInput)
        {
            this.showPointer = showPointer;
            this.pauseTime = pauseTime;
            this.lockMovement = lockMovement;
            this.lockInput = lockInput;

            beforeOpenGUI = null;
            onOpenGUI = null;
            onCloseGUI = null;
        }

        public UIOptions(Action<GameObject> beforeOpenGUI, Action<GameObject> onOpenGUI, Action<GameObject> onCloseGUI)
        {
            this.beforeOpenGUI = beforeOpenGUI;
            this.onOpenGUI = onOpenGUI;
            this.onCloseGUI = onCloseGUI;

            showPointer = false;
            pauseTime = false;
            lockMovement = true;
            lockInput = false;
        }
    }

    public class GUIManager : PersistentSingleton<GUIManager>
    {
        public GameObject mainCanvas;

        [Header("Menus GameObjects")]
        public GameObject pauseMenu;
        public GameObject inventoryUi;

        [Header("GUI Animation Settings")]
        [Range(0.1f, 1f)] public float alphaAnimationDuration;

        private CanvasGroup m_CurrentGUICanvasGroup;
        private GameObject m_CurrentGUI;

        private Action<GameObject> m_OnCloseGUI;
        private bool m_IsGuiOpened;

        private readonly List<GameObject> m_InstantiatedObjects = new List<GameObject>();
        private GameObject m_CanvasInstance;

        private bool m_JustOpenedGUI;

        private void Start() => InitializeCanvasInstance();

        private void Update()
        {
            if (m_IsGuiOpened && !m_JustOpenedGUI && PlayerInput.Instance.Pause) CloseGUIMenu();
            if (m_JustOpenedGUI) m_JustOpenedGUI = false;
        }

        public void InitializeCanvasInstance()
        {
            m_IsGuiOpened = false;

            if (m_CanvasInstance) Destroy(m_CanvasInstance);

            (m_CanvasInstance = Instantiate(mainCanvas)).transform.SetParent(transform);
        }

        /// <summary>
        /// Static method to animated a canvas group, as a fade effect
        /// </summary>
        /// <param name="canvasGroup">the canvas group to be animated</param>
        /// <param name="objectiveAlpha">the objective to get to as de alpha value, it starts from the value it already has</param>
        /// <param name="animationDuration">the duration of the fading</param>
        /// <param name="onceFinishAnimation">once the animation is finished</param>
        /// <param name="animEase">the type of animation ease</param>
        public static void AnimateAlpha(CanvasGroup canvasGroup, float objectiveAlpha, float animationDuration = 0.5f, TweenCallback onceFinishAnimation = null, Ease animEase = Ease.Unset)
        {
            canvasGroup.alpha = Mathf.Abs(1f - objectiveAlpha);
            canvasGroup.DOFade(objectiveAlpha, animationDuration).SetEase(animEase).OnComplete(onceFinishAnimation);
        }

        /// <summary>
        /// Opens the gameObject passed as a gui on the main canvas
        /// </summary>
        /// <param name="menu">the gui to instantiate</param>
        /// <param name="options">menu instantiation effects</param>
        public void OpenGUIMenu(GameObject menu, UIOptions options)
        {
            if (m_IsGuiOpened) return;

            m_IsGuiOpened = true;
            m_JustOpenedGUI = true;

            m_CurrentGUI = Instantiate(menu, m_CanvasInstance.transform);

            m_CurrentGUI.SetActive(true);

            options.beforeOpenGUI?.Invoke(m_CurrentGUI);

            m_CurrentGUICanvasGroup = m_CurrentGUI.GetComponent<CanvasGroup>();

            void Callback()
            {
                m_CurrentGUICanvasGroup.interactable = true;

                if (options.pauseTime) Time.timeScale = 0f;
                if (options.lockInput) PlayerInput.Instance.BlockInput();

                if (options.showPointer) PointerManager.Instance.SetCursorActive();
                PlayerController.Instance.BlockMovement(options.lockMovement);

                options.onOpenGUI?.Invoke(m_CurrentGUI);
            }

            if (m_CurrentGUICanvasGroup && !options.pauseTime) AnimateAlpha(m_CurrentGUICanvasGroup, 1f, alphaAnimationDuration, Callback);
            else Callback();

            m_OnCloseGUI = options.onCloseGUI;
        }

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
                PointerManager.Instance.SetCursorActive(false);

                Destroy(m_CurrentGUI);

                m_OnCloseGUI?.Invoke(m_CurrentGUI);
                m_OnCloseGUI = null;

                m_IsGuiOpened = false;
            }

            if (animate) AnimateAlpha(m_CurrentGUICanvasGroup, 0f, alphaAnimationDuration, OnceFinishAnimation);
            else OnceFinishAnimation();
        }

        /// <summary>
        /// Closes any opened menu instantly
        /// </summary>
        public void CloseGUIMenuInstantly()
        {
            Time.timeScale = 1f;
            Destroy(m_CurrentGUI);
            m_IsGuiOpened = false;
        }

        /// <summary>
        /// Instantiates a game object on the main canvas
        /// </summary>
        /// <param name="obj">game object to be instantiated</param>
        /// <param name="destroyOnLoad">check whether to be destroyed on load</param>
        /// <returns></returns>
        public GameObject InstantiateUIInstantly(GameObject obj, bool destroyOnLoad = true)
        {
            GameObject instance;
            m_InstantiatedObjects.Add(instance = Instantiate(obj, m_CanvasInstance.transform));

            if (destroyOnLoad)
            {
                void DestroyInstance()
                {
                    RemoveUIInstantly(instance);
                    LevelLoadManager.Instance.onSceneLoadCompleted -= DestroyInstance;
                }

                LevelLoadManager.Instance.onSceneLoadCompleted += DestroyInstance;
            }

            return instance;
        }

        /// <summary>
        /// Instantiates a game object on the main canvas with an animation
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="destroyOnLoad">check whether to be destroyed on load</param>
        /// <param name="targetAlpha">target alpha of the animation</param>
        /// <param name="animationDuration">optional</param>
        /// <param name="onAnimationEnd">optional event</param>
        /// <param name="animationEase">animation style</param>
        /// <returns></returns>
        public GameObject InstantiateUI(GameObject obj, bool destroyOnLoad, float targetAlpha = 1f, float animationDuration = 0.5f, TweenCallback onAnimationEnd = null, Ease animationEase = Ease.Unset)
        {
            GameObject instance = InstantiateUIInstantly(obj, destroyOnLoad);

            var canvasGroup = instance.GetComponent<CanvasGroup>();

            if (canvasGroup) AnimateAlpha(canvasGroup, targetAlpha, animationDuration, onAnimationEnd, animationEase);
            else onAnimationEnd?.Invoke();

            return instance;
        }

        /// <summary>
        /// Instantiates a game object on the main canvas with an animation
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="targetAlpha">target alpha of the animation</param>
        /// <param name="animationDuration">optional</param>
        /// <param name="onAnimationEnd">optional event</param>
        /// <param name="animationEase">animation style</param>
        /// <returns></returns>
        public GameObject InstantiateUI(GameObject obj, float targetAlpha = 1f, float animationDuration = 0.5f, TweenCallback onAnimationEnd = null, Ease animationEase = Ease.Unset) => InstantiateUI(obj, true, targetAlpha, animationDuration, onAnimationEnd, animationEase);

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
        /// <param name="animationEase">animation style</param>
        /// <returns></returns>
        public void RemoveUI(GameObject obj, float animationDuration = 0.5f, TweenCallback onceFinishAnimation = null, Ease animationEase = Ease.Unset)
        {
            var canvasGroup = obj.GetComponent<CanvasGroup>();

            onceFinishAnimation += () => RemoveUIInstantly(obj);

            if (canvasGroup) AnimateAlpha(canvasGroup, 0f, animationDuration, onceFinishAnimation, animationEase);
            else onceFinishAnimation();
        }

        /// <summary>
        /// Opens the pause menu
        /// </summary>
        /// <param name="onOpenGUI"></param>
        /// <param name="onCloseGUI"></param>
        public void OpenPauseMenu(Action onOpenGUI, Action onCloseGUI)
        {
            var options = new UIOptions(true, true, false, true);

            options.SetActions(null, x => onOpenGUI?.Invoke(), x => onCloseGUI?.Invoke());

            OpenGUIMenu(pauseMenu, options);
        }

        /// <summary>
        /// Opens the inventory
        /// </summary>
        /// <param name="onOpenGUI"></param>
        /// <param name="onCloseGUI"></param>
        public void OpenInventory(Action onOpenGUI, Action onCloseGUI)
        {
            var options = new UIOptions(true, false, false, false);

            options.SetActions(GameManager.Instance.inventory.InitializeInventory, x => onOpenGUI?.Invoke(), x => onCloseGUI?.Invoke());

            OpenGUIMenu(inventoryUi, options);
        }
    }
}
