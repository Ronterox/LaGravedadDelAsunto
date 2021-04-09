using System.Collections;
using Player;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(CharacterHealth))]
    public class CharacterCombat : MonoBehaviour
    {
        public float attackSpeed = 1f;
        public float attackDelay = .6f;

        private float attackCooldown;

        public float combatCooldown = 2;
        public float lastAttackTime;

        public int damage;

        public bool inCombat { get; private set; }

        private Animator m_Animator;

        private readonly int IN_COMBAT_HASH = Animator.StringToHash("inCombat");

        private void Start() => m_Animator = GetComponent<Animator>();

        private void Update()
        {
            attackCooldown -= Time.deltaTime;
            if (Time.time - lastAttackTime > combatCooldown)
            {
                inCombat = false;
            }
            if (PlayerInput.Instance.Attack)
            {
                inCombat = true;
            }
        }

        private void FixedUpdate() => AnimatePlayer();

        public void Attack(CharacterHealth targetHealth)
        {
            if (attackCooldown <= 0f)
            {
                StartCoroutine(DoDamage(targetHealth, attackDelay));

                inCombat = true;
                attackCooldown = 1f / attackSpeed;
                lastAttackTime = Time.time;
            }
        }

        private IEnumerator DoDamage(CharacterHealth health, float delay)
        {
            yield return new WaitForSeconds(delay);
            health.TakeDamage(damage);

            if (health.currentHealth <= 0) inCombat = false;
        }

        private void AnimatePlayer()
        {
            m_Animator.SetBool(IN_COMBAT_HASH, inCombat);
        }
    }
}
