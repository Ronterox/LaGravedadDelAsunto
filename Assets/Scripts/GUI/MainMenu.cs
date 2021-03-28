using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUI
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame() => SceneManager.LoadScene("");

        public void TestZone() => SceneManager.LoadScene("TestZone");

        public void OpenSettings() => SceneManager.LoadScene("");

        public void QuitGame() => Application.Quit();
    }
}
