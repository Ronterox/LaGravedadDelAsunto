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
        [Space] public UnityEvent onCampaignCompletedInteraction;
        
        [Header("Speaking Settings")]
        public Transform textPosition;
        public Vector3 rotationAxis = Vector3.up;

        public bool infiniteCompletedEventCall;
        private bool m_CalledCampaignEventOnce;
        
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

        protected abstract void OnCampaignCompletedInteraction(Campaign campaign);

        protected abstract void OnInteractionRangeEnter(Campaign campaign);

        protected abstract void OnInteractionRangeExit(Campaign campaign);

        protected abstract void OnInteraction(Campaign campaign);

        protected override void OnEnterTrigger(Collider other)
        {
            if (!m_Player) m_Player = other.transform;
            OnInteractionRangeEnter(GameManager.Instance.questManager.GetCampaign(npcScriptable.campaignID));
        }

        protected override void OnExitTrigger(Collider other) => OnInteractionRangeExit(GameManager.Instance.questManager.GetCampaign(npcScriptable.campaignID));

        public override void Interact()
        {
            if (m_IsFirstInteraction)
            {
                Write($"Hello I'm {npcScriptable.name}, I'm {npcScriptable.description}");
                m_IsFirstInteraction = false;
                m_InteractTimes = 0;
            }
            
            Campaign npcCampaign = GameManager.Instance.questManager.GetCampaign(npcScriptable.campaignID);
            if (npcCampaign != null && npcCampaign.IsCompleted)
            {
                if (infiniteCompletedEventCall || !m_CalledCampaignEventOnce)
                {
                    m_CalledCampaignEventOnce = true;
                    OnCampaignCompletedInteraction(npcCampaign);
                    onCampaignCompletedInteraction?.Invoke();
                }
            }
            OnInteraction(npcCampaign);
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
