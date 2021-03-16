using UnityEngine;

namespace NPCs
{
    [CreateAssetMenu(fileName = "New Npc", menuName = "Penguins Mafia/NPC")]
    public class ScriptableNPC : ScriptableObject
    {
        [Header("NPC")] public string npcName;
        [Space] [TextArea] public string description;

        public string campaignID;
    }
}
