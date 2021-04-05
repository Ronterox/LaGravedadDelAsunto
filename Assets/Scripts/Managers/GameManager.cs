using GUI;
using Inventory_System;
using Player;
using Plugins.Tools;
using UnityEngine;

namespace Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public KarmaController karmaController;
        public QuestManager questManager;
        
        public DialogueManager dialogueManager;
        public Inventory inventory;

        public PointerManager pointerManager;
        public GUIManager guiManager;
        
        public static bool GameIsPaused => Time.timeScale == 0f;

        private void Update()
        {
            if (!PlayerInput.Instance.Pause) return;
            
            if (GameIsPaused) GUIManager.Instance.CloseGUIMenu();
            else GUIManager.Instance.OpenPauseMenu();
        }
    }
}
