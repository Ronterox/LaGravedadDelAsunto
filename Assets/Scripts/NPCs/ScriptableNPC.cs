using System.Collections.Generic;
using UnityEngine;

namespace NPCs
{
    [CreateAssetMenu(fileName = "New Npc", menuName = "Penguins Mafia/NPC")]
    public class ScriptableNPC : ScriptableObject
    {
        public DialogueGroup[] dialogues;
        private readonly Dictionary<string, Dialogue> m_Dialogues = new Dictionary<string, Dialogue>();

        public Dialogue GetDialogue(string dialogueID) => m_Dialogues.TryGetValue(dialogueID, out Dialogue value) ? value : null;

        private void OnEnable()
        {
            foreach (DialogueGroup dialogueGroup in dialogues)
                foreach (Dialogue dialogue in dialogueGroup.dialogues) m_Dialogues.Add(dialogue.id, dialogue);
        }

        private void OnDisable() => m_Dialogues.Clear();
    }
}
