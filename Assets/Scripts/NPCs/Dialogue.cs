using UnityEngine;

namespace NPCs
{
    [System.Serializable]
    public class Dialogue
    {
        public string id;
        [TextArea] public string line;

        public DialogueDecision[] options;

        public bool hasDecisions => options.Length > 0;
    }
    public enum Decision { Agree, Disagree, Ignore }

    [System.Serializable]
    public struct DialogueGroup
    {
        public string groupName;
        public Dialogue[] dialogues;
    }

    [System.Serializable]
    public struct DialogueDecision
    {
        public Decision decision;
        public string text;
    }
}
