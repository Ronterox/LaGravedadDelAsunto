using System.Collections.Generic;
using UnityEngine;

namespace NPCs
{
    [CreateAssetMenu(fileName = "New Npc", menuName = "Penguins Mafia/NPC")]
    public class ScriptableNPC : ScriptableObject
    {
        [Header("NPC")] public string npcName;
        [Space] [TextArea] public string description;

        public string campaignID;

        public DialogueGroup[] dialogues;
        private readonly Dictionary<string, Dialogue> m_Dialogues = new Dictionary<string, Dialogue>();

        public Dialogue GetDialogue(string dialogueID) => m_Dialogues.TryGetValue(dialogueID, out Dialogue value) ? value : new Dialogue();

        private void OnEnable()
        {
            foreach (DialogueGroup dialogueGroup in dialogues)
                foreach (Dialogue dialogueGroupDialogue in dialogueGroup.dialogues)
                    m_Dialogues.Add(dialogueGroupDialogue.id, dialogueGroupDialogue);
        }

        private void OnDisable() => m_Dialogues.Clear();
    }
}
