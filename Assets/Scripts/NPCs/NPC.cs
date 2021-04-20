using General.Utilities;
using Managers;
using Plugins.Tools;
using Questing_System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace NPCs
{
    public abstract class NPC : Interactable
    {
        [Header("NPC Settings")]
        public ScriptableNPC npcScriptable;
        [Space] public UnityEvent onQuestCompletedInteraction;
        
        [Header("Speaking Settings")]
        public Transform textPosition;
        public Vector3 rotationAxis = Vector3.up;

        public bool infiniteCompletedEventCall;
        private bool m_CalledQuestEventOnce;
        
        private Transform m_Player;
        private NavMeshAgent m_Agent;

        private bool m_IsFirstInteraction = true;

        protected override void Awake()
        {
            base.Awake();
            m_Agent = GetComponent<NavMeshAgent>();
        }

        protected override void Update()
        {
            base.Update();
            if(IsPlayerOnRange) transform.RotateTowards(m_Player, rotationAxis);
            else if(m_Agent && !m_Agent.isStopped) transform.RotateTowards(m_Agent.destination);
        }

        protected abstract void OnQuestCompletedInteraction(Quest quest);

        protected abstract void OnInteractionRangeEnter(Quest quest);

        protected abstract void OnInteractionRangeExit(Quest quest);

        protected abstract void OnInteraction(Quest quest);

        protected override void OnEnterTrigger(Collider other)
        {
            if (!m_Player) m_Player = other.transform;
            OnInteractionRangeEnter(GameManager.Instance.questManager.GetQuest(npcScriptable.questID));
        }

        protected override void OnExitTrigger(Collider other) => OnInteractionRangeExit(GameManager.Instance.questManager.GetQuest(npcScriptable.questID));

        public override void Interact()
        {
            if (m_IsFirstInteraction)
            {
                Write($"Hello I'm {npcScriptable.name}, I'm {npcScriptable.description}");
                m_IsFirstInteraction = false;
                m_InteractTimes = 0;
                return;
            }
            
            Quest npcQuest = GameManager.Instance.questManager.GetQuest(npcScriptable.questID);
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

        public void Say(string dialogueID)
        {
            Dialogue dialogue = npcScriptable.GetDialogue(dialogueID);
            
            if(dialogue != null) Write(dialogue.line);
            else Debug.LogError($"Dialogue Id {dialogueID} doesn't exist!");
        }

        private void Write(string text) => GameManager.Instance.dialogueManager.Type(text, textPosition);
    }
}
