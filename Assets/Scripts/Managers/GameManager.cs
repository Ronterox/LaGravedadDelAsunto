using GUI;
using Inventory_System;
using Plugins.Tools;

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
    }
}
