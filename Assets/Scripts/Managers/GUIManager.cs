using System;
using DG.Tweening;
using Player;
using Plugins.DOTween.Modules;
using Plugins.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        //TODO: Fix Minigames
        private void Start()
        {
            if (mainCanvas)
            {
                const string sceneName = "GUI Objects";
                Scene scene = SceneManager.GetSceneByName(sceneName);
                SceneManager.MoveGameObjectToScene(mainCanvas.gameObject, scene.IsValid() ? scene : SceneManager.CreateScene(sceneName));
            }
            else Debug.Log("Main canvas is missing on GUI Manager!".ToColorString("cyan"));
        }

        private void Update()
        {
            if (m_IsGuiOpened && PlayerInput.Instance.Pause) CloseGUIMenu();
        }

        public static void AnimateAlpha(CanvasGroup canvasGroup, float objectiveAlpha, float animationDuration = 0.5f, TweenCallback onceFinishAnimation = null) =>
            canvasGroup.DOFade(objectiveAlpha, animationDuration).OnComplete(onceFinishAnimation);

        public void LockInputs() => PlayerInput.Instance.BlockInput();

        public void UnlockInputs() => PlayerInput.Instance.UnlockInput();

        public void OpenGUIMenu(GameObject menu, Action<GameObject> beforeOpenGUI = null, Action<GameObject> onOpenGUI = null, Action<GameObject> onCloseGUI = null, bool showPointer = false, bool pauseTime = false, bool lockMovement = true, bool lockInput = false)
        {
            if (m_IsGuiOpened) return;

            m_CurrentGUI = Instantiate(menu, mainCanvas.transform);

            beforeOpenGUI?.Invoke(m_CurrentGUI);

            m_CurrentGUICanvasGroup = m_CurrentGUI.GetComponent<CanvasGroup>();

            void Callback()
            {
                m_CurrentGUICanvasGroup.interactable = true;

                if (lockInput) LockInputs();
                if (pauseTime) Time.timeScale = 0f;
                if (showPointer) GameManager.Instance.pointerManager.SetCursorActive();
                PlayerController.Instance.BlockMovement(lockMovement);

                onOpenGUI?.Invoke(m_CurrentGUI);
            }

            if (m_CurrentGUICanvasGroup && !pauseTime) AnimateAlpha(m_CurrentGUICanvasGroup, 1f, alphaAnimationDuration, Callback);
            else Callback();

            m_OnCloseGUI = onCloseGUI;

            m_IsGuiOpened = true;
        }

        public void OpenGUIMenu(GameObject menu, Action beforeOpenGUI = null, Action onOpenGUI = null, Action onCloseGUI = null, bool showPointer = false, bool pauseTime = false, bool lockMovement = true, bool lockInput = false) => OpenGUIMenu(menu, x => beforeOpenGUI?.Invoke(), x => onOpenGUI?.Invoke(), x => onCloseGUI?.Invoke(), showPointer, pauseTime, lockMovement, lockInput);

        public void CloseGUIMenu(bool animate = true)
        {
            if (!m_IsGuiOpened) return;

            void OnceFinishAnimation()
            {
                m_CurrentGUICanvasGroup.interactable = false;

                UnlockInputs();
                Time.timeScale = 1f;

                PlayerController.Instance.BlockMovement(false);
                GameManager.Instance.pointerManager.SetCursorActive(false);

                Destroy(m_CurrentGUI);

                m_OnCloseGUI?.Invoke(m_CurrentGUI);
                m_OnCloseGUI = null;
            }

            if (animate) AnimateAlpha(m_CurrentGUICanvasGroup, 0f, alphaAnimationDuration, OnceFinishAnimation);
            else OnceFinishAnimation();

            m_IsGuiOpened = false;
        }

        public void OpenPauseMenu() => OpenGUIMenu(pauseMenu, x => { }, null, null, true, true, false);

        public void OpenInventory() => OpenGUIMenu(inventoryUi, GameManager.Instance.inventory.InitializeInventory, null, null, true, false, false);
    }
}
