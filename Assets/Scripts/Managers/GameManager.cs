using Plugins.Tools;

namespace Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public KarmaController karmaController;
        public QuestManager questManager;
        public DialogueManager dialogueManager;
    }
}
