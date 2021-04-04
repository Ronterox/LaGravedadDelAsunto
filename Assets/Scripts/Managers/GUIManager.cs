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

        [Header("GUI Animation Settings")]
        [Range(0.5f, 3f)] public float alphaAnimationDuration;

        private CanvasGroup m_CurrentGUICanvasGroup;
        private GameObject m_CurrentGUI;

        private Action m_OnCloseGUI;
        private bool m_IsGuiOpened;

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

        public void AnimateAlpha(CanvasGroup canvasGroup, float objectiveAlpha, TweenCallback onceFinishAnimation = null) =>
            canvasGroup.DOFade(objectiveAlpha, alphaAnimationDuration).OnComplete(onceFinishAnimation);

        public void LockInputs() => PlayerInput.Instance.BlockInput();

        public void UnlockInputs() => PlayerInput.Instance.UnlockInput();

        //TODO: maybe, add addressables to instantiate async
        public void OpenGUIMenu(GameObject menu, Action onOpenGUI = null, Action onCloseGUI = null, bool showPointer = false, bool pauseTime = false, bool lockMovement = false, bool lockInput = false)
        {
            if (m_IsGuiOpened) return;

            m_CurrentGUI = Instantiate(menu, mainCanvas.transform);

            m_CurrentGUICanvasGroup = m_CurrentGUI.GetComponent<CanvasGroup>();

            AnimateAlpha(m_CurrentGUICanvasGroup, 1f, () =>
            {
                m_CurrentGUICanvasGroup.interactable = true;

                if (lockInput) LockInputs();
                if (pauseTime) Time.timeScale = 0;

                if (showPointer) GameManager.Instance.pointerManager.SetCursorActive();
                PlayerController.Instance.BlockMovement(lockMovement);

                onOpenGUI?.Invoke();
            });

            m_OnCloseGUI = onCloseGUI;

            m_IsGuiOpened = true;
        }

        public void CloseGUIMenu()
        {
            if (!m_IsGuiOpened) return;

            AnimateAlpha(m_CurrentGUICanvasGroup, 0f, () =>
            {
                m_CurrentGUICanvasGroup.interactable = false;

                UnlockInputs();
                Time.timeScale = 0;

                PlayerController.Instance.BlockMovement(false);
                GameManager.Instance.pointerManager.SetCursorActive(false);

                Destroy(m_CurrentGUI);

                m_OnCloseGUI?.Invoke();
                m_OnCloseGUI = null;
            });

            m_IsGuiOpened = false;
        }

        public void OpenPauseMenu() => OpenGUIMenu(pauseMenu, null, null, true, true);
    }
}
