using Inventory_System;
using Managers;
using Player;
using Plugins.Tools;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(CharacterHealth))]
    public class CharacterCombat : Singleton<CharacterCombat>
    {
        public Transform weaponHolder;

        public Weapon weapon;

        private Animator m_Animator;

        private readonly int ATTACK_HASH = Animator.StringToHash("Attack");

        private void Start()
        {
            if(weapon) SetWeapon();
        }

        public void SetWeapon()
        {
            Transform swordTransform = weapon.transform;
            
            swordTransform.SetParent(weaponHolder, false);
            swordTransform.position = weaponHolder.position;
            
            weapon.myDamageable = GetComponent<Damageable>();
            weapon.isPlayer = true;
            
            weapon.DisableExtraColliders();

            weapon.GetComponent<PickUpItem>().enabled = false;

            var weaponBody = weapon.GetComponent<Rigidbody>();
            weaponBody.useGravity = false;
            weaponBody.isKinematic = true;
        }

        protected override void Awake()
        {
            base.Awake();
            m_Animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (GUIManager.Instance.IsGuiOpened) return;

            if (PlayerInput.Instance.Attack) m_Animator.SetTrigger(ATTACK_HASH);
        }
        
        public void Attack(int isAttacking)
        {
            if(weapon) weapon.SetCollider(isAttacking == 1);
        }
    }
}
