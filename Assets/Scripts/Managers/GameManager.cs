using Inventory_System;
using Karma_System;
using Player;
using Plugins.Tools;

namespace Managers
{
    public class GameManager : PersistentSingleton<GameManager>, MMEventListener<MMGameEvent>
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

        public void OnMMEvent(MMGameEvent eventType)
        {
            if (!eventType.Equals(MMGameEvent.LOAD)) return;
            
            if (LevelLoadManager.Instance.SceneIsGUI) Destroy(gameObject);
        }

        public void OnEnable() => this.MMEventStartListening();

        public void OnDisable() => this.MMEventStopListening();
    }
}
