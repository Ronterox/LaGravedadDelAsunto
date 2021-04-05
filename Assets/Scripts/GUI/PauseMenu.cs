using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUI
{
    public class PauseMenu : MonoBehaviour
    {
        public void LoadMainMenu() => SceneManager.LoadScene("MainMenu");

        public void OpenSettings() => SceneManager.LoadScene("");
    }
}
