using Managers;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUI
{
    public class PauseMenu : MonoBehaviour
    {
        //TODO: Remove load methods from pause menu, and add then differently?
        public bool GameIsPaused => Time.timeScale == 0f;

        private void Update()
        {
            if (!PlayerInput.Instance.Pause) return;
            
            if (GameIsPaused) GUIManager.Instance.CloseGUIMenu();
            else GUIManager.Instance.OpenPauseMenu();
        }
        
        public void LoadMainMenu() => SceneManager.LoadScene("MainMenu");

        public void OpenSettings() => SceneManager.LoadScene("");
    }
}
