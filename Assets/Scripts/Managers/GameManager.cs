using Inventory_System;
using Karma_System;
using Player;
using Plugins.Tools;

namespace Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public KarmaController karmaController;
        public QuestManager questManager;

        public DialogueManager dialogueManager;
        public Inventory inventory;

        public bool GameIsPaused;
        
        private void Update()
        {
            if (!PlayerInput.Instance.Pause) return;

            if (GameIsPaused) GUIManager.Instance.CloseGUIMenu();
            else GUIManager.Instance.OpenPauseMenu(() => GameIsPaused = true, () => GameIsPaused = false);

            GameIsPaused = !GameIsPaused;
        }
    }
}
