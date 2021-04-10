using DG.Tweening;
using Plugins.Tools;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Manager class that allows displaying a transition animation whenever necessary.
    /// It's a persistent Singleton.
    /// </summary>
    public class TransitionManager : PersistentSingleton<TransitionManager>
    {
        public GameObject transitionPanelGameObject;

        public Ease openEase = Ease.InQuad, closeEase = Ease.OutQuad;
        public float duration = 0.5f;

        private GameObject m_TransitionPanelInstance;
        private bool m_IsTransitioning;

        /// <summary>
        /// Animates the transition with a canvas group
        /// </summary>
        /// <param name="open"></param>
        /// <param name="callback"></param>
        private void AnimateTransition(bool open, TweenCallback callback)
        {
            if (m_IsTransitioning) return;

            if (open && transitionPanelGameObject) GUIManager.Instance.RemoveUIInstantly(transitionPanelGameObject);

            m_IsTransitioning = true;
            callback += () => m_IsTransitioning = false;

            if (open) m_TransitionPanelInstance = GUIManager.Instance.InstantiateUI(transitionPanelGameObject, 1f, duration, callback, openEase);
            else GUIManager.Instance.RemoveUI(m_TransitionPanelInstance, duration, callback, closeEase);
        }

        /// <summary>
        /// Starts the Open animation
        /// </summary>
        public void Open(TweenCallback callback = null) => AnimateTransition(true, callback);

        /// <summary>
        /// Starts the Close animation. (Coroutine inside TransitionManager)
        /// </summary>
        public void Close(TweenCallback callback = null) => AnimateTransition(false, callback);
    }
}
