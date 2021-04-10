using Managers;
using Plugins.Properties;
using UnityEngine;

namespace GUI
{
    public class MainMenu : MonoBehaviour
    {
        [Scene] public string testZone, settings;

        public void PlayGame() {}

        public void TestZone() => LevelLoadManager.Instance.LoadScene(testZone);

        public void OpenSettings() => LevelLoadManager.Instance.LoadScene(settings);

        public void QuitGame() => Application.Quit();
    }
}
