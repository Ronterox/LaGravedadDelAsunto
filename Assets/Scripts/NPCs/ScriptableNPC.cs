using Questing_System;
using UnityEngine;

namespace NPCs
{
    [CreateAssetMenu(fileName = "New Npc", menuName = "Penguins Mafia/NPC")]
    public class ScriptableNPC : ScriptableObject
    {
        [Header("NPC")] public string npcName;
        [Space] [TextArea] public string description;

        public Campaign campaign;
    }
}
