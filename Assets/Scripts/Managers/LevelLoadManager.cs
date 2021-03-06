using System.Linq;
using Plugins.Properties;
using Plugins.Tools;
using Plugins.Tools.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class LevelLoadManager : PersistentSingleton<LevelLoadManager>
    {
        private bool m_IsLoading;

        [Scene] public string[] guiScenes;

        public delegate void LevelLoadEvent();

        public event LevelLoadEvent onSceneLoadCompleted;

        public bool SceneIsGUI => guiScenes.Contains(GetCurrentSceneName());

        private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

        private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            MMEventManager.TriggerEvent(new MMGameEvent(MMGameEvent.LOAD));
            
            GUIManager.Instance.CloseGUIMenuInstantly();

            onSceneLoadCompleted?.Invoke();

            m_IsLoading = false;
        }

        /// <summary>
        /// Loads the next scene on the build settingsScene
        /// </summary>
        public void LoadNextScene() => LoadSceneProcedure(SceneManager.GetActiveScene().buildIndex + 1);

        /// <summary>
        /// Loads the scene by the name passed
        /// </summary>
        /// <param name="sceneName"></param>
        public void LoadScene(string sceneName) => LoadSceneProcedure(sceneName);

        /// <summary>
        /// Loads the scene by the index passed
        /// </summary>
        /// <param name="sceneIndex"></param>
        public void LoadScene(int sceneIndex) => LoadSceneProcedure(sceneIndex);

        private void LoadSceneProcedure(object scene)
        {
            if (m_IsLoading) return;

            Time.timeScale = 1f;

            m_IsLoading = true;

            TransitionManager.Instance.Open(() =>
            {
                switch (scene)
                {
                    case string sceneName:
                        SceneManager.LoadScene(sceneName);
                        break;
                    case int sceneIndex:
                        SceneManager.LoadScene(sceneIndex);
                        break;
                }

                TransitionManager.Instance.Close();
            });
        }

        /// <summary>
        /// Returns the name of the current scene
        /// </summary>
        /// <returns></returns>
        public string GetCurrentSceneName() => SceneManager.GetActiveScene().name;
    }
}
