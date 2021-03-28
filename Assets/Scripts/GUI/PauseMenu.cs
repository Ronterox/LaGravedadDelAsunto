using Managers;
using Player;
using Plugins.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUI
{
    public class PauseMenu : Singleton<PauseMenu>
    {
        public GameObject pauseMenuUI;
        public bool GameIsPaused => Time.timeScale == 0f;

        private void Update()
        {
            if (!PlayerInput.Instance.Pause && !Input.GetKeyDown(KeyCode.Escape)) return;
            if (GameIsPaused) Resume();
            else Pause();
        }

        private void ActivatePause(bool activate)
        {
            pauseMenuUI.SetActive(activate);
            Time.timeScale = activate ? 0f : 1f;
            GameManager.Instance.pointerManager.SetCursorActive(activate);
        }

        public void Resume() => ActivatePause(false);

        private void Pause() => ActivatePause(true);

        public void LoadMainMenu() => SceneManager.LoadScene("MainMenu");

        public void OpenSettings() => SceneManager.LoadScene("");
    }
}
