using System;
using UnityEngine;

namespace Questing_System
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Penguins Mafia/Quest")]
    public class ScriptableQuest : ScriptableObject
    {
        public Quest quest;
        
        [Header("Quest Settings")]
        public int karmaWon;
        public int karmaLost;
    }
}
