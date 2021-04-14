using UnityEngine;

namespace Questing_System
{
    [CreateAssetMenu(fileName = "New Quest Info", menuName = "Penguins Mafia/Quest Info")]
    public class QuestInfo : ScriptableObject
    {
        public string questName;
        [TextArea]
        public string questDescription;  
        
        [Header("Quest Rewards")]
        public int positiveKarma;
        public int negativeKarma;
    }
}
