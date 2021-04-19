using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Combat;

namespace NPCs
{
    public class AnimationsNPC : MonoBehaviour
    {
        private Animator m_Animator;
        private Transform m_Player;
        private NavMeshAgent m_Agent;
        private Enemy m_Enemy;

        private readonly int SPEED_ANIMATION_HASH = Animator.StringToHash("Speed");

        protected void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_Agent = GetComponent<NavMeshAgent>(); 
            m_Enemy = GetComponent<Enemy>();          
        }

        private void FixedUpdate()
        {
            AnimateNPC();
        }

        private void AnimateNPC()
        {            
            if (m_Agent.isStopped)
            {
                m_Animator.SetFloat(SPEED_ANIMATION_HASH, 0f, 0.15f, Time.deltaTime);
            }
            else
            {
                m_Animator.SetFloat(SPEED_ANIMATION_HASH, m_Enemy.InCombat ? 1f : 0.5f, 0.15f, Time.deltaTime);
            }
        }
    }
}

