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
    }
}
