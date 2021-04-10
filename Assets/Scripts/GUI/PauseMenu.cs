using Managers;
using Plugins.Properties;
using UnityEngine;

namespace GUI
{
    public class PauseMenu : MonoBehaviour
    {
        [Scene] public string menu, settings;

        public void Resume() => GUIManager.Instance.CloseGUIMenu();

        public void LoadMainMenu() => LevelLoadManager.Instance.LoadScene(menu);

        public void OpenSettings() => LevelLoadManager.Instance.LoadScene(settings);
    }
}
