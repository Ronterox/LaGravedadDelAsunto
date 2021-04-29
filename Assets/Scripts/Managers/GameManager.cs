using Inventory_System;
using Karma_System;
using Player;
using Plugins.Tools;
using Plugins.Tools.Events;

namespace Managers
{
    public class GameManager : Singleton<GameManager>, MMEventListener<MMGameEvent>
    {
        public KarmaController karmaController;
        public QuestManager questManager;

        public DialogueManager dialogueManager;
        public Inventory inventory;
        
        public bool GameIsPaused { get; private set; }

        private void Update()
        {
            if (!PlayerInput.Instance.Pause) return;
            PauseGame();
        }

        public void OnMMEvent(MMGameEvent eventType)
        {
            if (m_Instance || !eventType.Equals(MMGameEvent.LOAD)) return;
            if (LevelLoadManager.Instance.SceneIsGUI) Destroy(m_Instance.gameObject);
        }

        public void PauseGame()
        {
            if (GameIsPaused) GUIManager.Instance.CloseGUIMenu();
            else GUIManager.Instance.OpenPauseMenu(() => GameIsPaused = true, () => GameIsPaused = false);
        }

        public void OnEnable() => this.MMEventStartListening();

        public void OnDisable() => this.MMEventStopListening();
    }
}
