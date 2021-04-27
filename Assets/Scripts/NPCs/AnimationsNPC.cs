using UnityEngine;
using UnityEngine.AI;
using Combat;

namespace NPCs
{
    [RequireComponent(typeof(Damageable))]
    public class AnimationsNPC : MonoBehaviour
    {
        private Animator m_Animator;
        private NavMeshAgent m_Agent;
        private Damageable m_Damageable;

        private const float SPEED_DAMP_TIME = 0.15f;
        private readonly int SPEED_ANIMATION_HASH = Animator.StringToHash("Speed");
        private readonly int HIT_ANIMATION_HASH = Animator.StringToHash("Hit");

        protected void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_Agent = GetComponent<NavMeshAgent>();
            m_Damageable = GetComponent<Damageable>();
        }

        private void OnEnable() => m_Damageable.myHealth.AddListeners(null, HitAnimation);
        
        private void OnDisable() => m_Damageable.myHealth.RemoveListeners(null, HitAnimation);

        private void FixedUpdate() => AnimateNPC();

        private void HitAnimation() => m_Animator.SetTrigger(HIT_ANIMATION_HASH);

        private void AnimateNPC()
        {
            if (m_Agent && m_Agent.isStopped) m_Animator.SetFloat(SPEED_ANIMATION_HASH, 0f, SPEED_DAMP_TIME, Time.deltaTime);
            else m_Animator.SetFloat(SPEED_ANIMATION_HASH, m_Damageable.InCombat ? 1f : 0.5f, SPEED_DAMP_TIME, Time.deltaTime);
        }
    }
}
