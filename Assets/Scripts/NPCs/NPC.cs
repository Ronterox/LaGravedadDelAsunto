using Combat;
using General.Utilities;
using Inventory_System;
using Managers;
using Plugins.Audio;
using Plugins.Tools;
using Questing_System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace NPCs
{
    public enum DialogueAction { DoNothing, StartQuest, EndQuestGood, EndQuestBad }

    [System.Serializable]
    public struct QuestDialogueID
    {
        public string dialogueID;
        public DialogueAction action;
        [Space]
        public string questRelatedId;
    }

    [RequireComponent(typeof(Damageable))]
    public abstract class NPC : Interactable
    {
        [Header("NPC")] public string npcName;
        [Space] [TextArea] public string description;
        public string questID;

        [Header("NPC Settings")]
        public ScriptableNPC npcScriptable;
        [Space] public UnityEvent onQuestCompletedInteraction;

        [Header("Speaking Settings")]
        public Transform textPosition;
        public Vector3 rotationAxis = Vector3.up;

        public bool infiniteCompletedEventCall;
        private bool m_CalledQuestEventOnce;

        [Header("Dialogues IDs")]
        [Tooltip("The ids are called on order")]
        public QuestDialogueID[] notStartedDialogues;
        public QuestDialogueID[] onGoingDialogues;
        public QuestDialogueID[] completedDialogues;
        [Space]
        public string[] onCombatDialogues;

        private Transform m_Player;
        private NavMeshAgent m_Agent;

        public Damageable damageable;
        public AudioClip npcTalkSound;

        [Header("Behaviour Tree")]
        public Item[] itemsToGive;

        private bool m_IsFirstInteraction = true;

        protected override void Awake()
        {
            base.Awake();
            m_Agent = GetComponent<NavMeshAgent>();
            if (!damageable) damageable = GetComponent<Damageable>();
        }

        private void OnEnable() => damageable.myHealth.AddListeners(null, SayCombatDialogue);
        
        private void OnDisable() => damageable.myHealth.RemoveListeners(null, SayCombatDialogue);

        protected override void Update()
        {
            if(damageable.InCombat) return;
            
            base.Update();
            if (IsPlayerOnRange) transform.RotateTowards(m_Player, rotationAxis);
            else if (m_Agent && !m_Agent.isStopped) transform.RotateTowards(m_Agent.destination);
        }

        protected abstract void OnQuestCompletedInteraction(Quest quest);

        protected abstract void OnInteractionRangeEnter(Quest quest);

        protected abstract void OnInteractionRangeExit(Quest quest);

        protected abstract void OnInteraction(Quest quest);

        protected override void OnEnterTrigger(Collider other)
        {
            if (!m_Player) m_Player = other.transform;
            OnInteractionRangeEnter(GameManager.Instance.questManager.GetQuest(questID));
        }

        protected override void OnExitTrigger(Collider other) => OnInteractionRangeExit(GameManager.Instance.questManager.GetQuest(questID));

        public override void Interact()
        {
            if (m_IsFirstInteraction)
            {
                Write($"Hello I'm {npcName}, I'm {description}");
                m_IsFirstInteraction = false;
                m_InteractTimes = 0;
                return;
            }

            Quest npcQuest = string.IsNullOrEmpty(questID) ? GameManager.Instance.questManager.GetQuestRandom() : GameManager.Instance.questManager.GetQuest(questID);
            if (npcQuest && npcQuest.IsCompleted)
            {
                if (infiniteCompletedEventCall || !m_CalledQuestEventOnce)
                {
                    m_CalledQuestEventOnce = true;
                    OnQuestCompletedInteraction(npcQuest);
                    onQuestCompletedInteraction?.Invoke();
                }
            }
            OnInteraction(npcQuest);
        }

        public void SayCombatDialogue() => Say(onCombatDialogues[Random.Range(0, onCombatDialogues.Length)]);

        public void Say(string dialogueID)
        {
            Dialogue dialogue = npcScriptable.GetDialogue(dialogueID);

            if (dialogue != null) Write(dialogue.line);
            else Debug.LogError($"Dialogue Id {dialogueID} doesn't exist!");
        }

        public void Write(string text)
        {
            GameManager.Instance.dialogueManager.Type(text, textPosition);
            SoundManager.Instance.PlayNonDiegeticRandomPitchSound(npcTalkSound);
        }
    }
}
