using Managers;
using UnityEngine;

namespace GUI
{
    public class PauseMenu : MonoBehaviour
    {
        public void Resume() => GUIManager.Instance.CloseGUIMenu(false);

        public void LoadMainMenu()
        {
            Resume();
            LevelLoadManager.Instance.LoadScene("MainMenu");
        }

        public void OpenSettings()
        {
            Resume();
            LevelLoadManager.Instance.LoadScene("");
        }
    }
}
