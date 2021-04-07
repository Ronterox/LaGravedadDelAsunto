using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUI
{
    public class PauseMenu : MonoBehaviour
    {
        public void Resume() => GUIManager.Instance.CloseGUIMenu(false);

        public void LoadMainMenu()
        {
            Resume();
            SceneManager.LoadScene("MainMenu");
        }

        public void OpenSettings()
        {
            Resume();
            SceneManager.LoadScene("");
        }
    }
}
