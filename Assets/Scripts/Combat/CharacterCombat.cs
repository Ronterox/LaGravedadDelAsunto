using System.Collections;
using Player;
using Plugins.Tools;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(CharacterHealth))]
    public class CharacterCombat : MonoBehaviour
    {
        public Transform weaponHolder;
        
        public float attackSpeed = 1f;
        public float attackDelay = .6f;

 

        public float combatCooldown = 2;
        public float lastAttackTime;

 

        public Weapon sword;

        public int damage;

        public bool inCombat { get; private set; }

        private Animator m_Animator;

        private readonly int ATTACK_HASH = Animator.StringToHash("Attack");

        private void Start() => SetWeapon();

        public void SetWeapon()
        {
            sword.transform.parent = weaponHolder;
            sword.transform.position = weaponHolder.position;
          
        }

        private void Awake()
        {
           
            m_Animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (PlayerInput.Instance.Attack)
            {
                m_Animator.SetTrigger(ATTACK_HASH);

            }
        }

 

        public void Attack(int isAttacking)
        {
            bool enableCollider=false;
            if (isAttacking == 0)
            {
                enableCollider = false;
            }else if (isAttacking == 1)
            {
                enableCollider = true;
            }
            sword.SetCollider(enableCollider);

        }


   
    }
}
