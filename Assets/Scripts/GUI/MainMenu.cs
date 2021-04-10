using Managers;
using UnityEngine;

namespace GUI
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame() => LevelLoadManager.Instance.LoadScene("");

        public void TestZone() => LevelLoadManager.Instance.LoadScene("TestZone");

        public void OpenSettings() => LevelLoadManager.Instance.LoadScene("");

        public void QuitGame() => Application.Quit();
    }
}
