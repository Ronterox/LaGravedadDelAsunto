using Inventory_System;
using Karma_System;
using Player;
using Plugins.Tools;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public KarmaController karmaController;
        public QuestManager questManager;

        public DialogueManager dialogueManager;
        public Inventory inventory;

        public bool GameIsPaused { get; private set; }

        private LevelLoadManager m_LevelLoadManager;

        private void Start() => m_LevelLoadManager = LevelLoadManager.Instance;

        private void Update()
        {
            if (m_LevelLoadManager.SceneIsGUI) return;

            if (!PlayerInput.Instance.Pause) return;
            PauseGame();
        }

        public void PauseGame()
        {
            if (GameIsPaused) GUIManager.Instance.CloseGUIMenu();
            else GUIManager.Instance.OpenPauseMenu(() => GameIsPaused = true, () => GameIsPaused = false);
        }
    }
}
