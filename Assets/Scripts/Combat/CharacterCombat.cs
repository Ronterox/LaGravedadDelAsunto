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

        public Weapon sword;

        private Animator m_Animator;

        private readonly int ATTACK_HASH = Animator.StringToHash("Attack");

        private void Start() => SetWeapon();

        public void SetWeapon()
        {
            Transform swordTransform = sword.transform;
            swordTransform.parent = weaponHolder;
            swordTransform.position = weaponHolder.position;
            sword.myDamageable = GetComponent<Damageable>();
            sword.isPlayer = true;
        }

        private void Awake() => m_Animator = GetComponent<Animator>();

        private void Update()
        {
            if (GUIManager.Instance.IsGuiOpened) return;

            if (PlayerInput.Instance.Attack) m_Animator.SetTrigger(ATTACK_HASH);
        }
        
        public void Attack(int isAttacking) => sword.SetCollider(isAttacking == 1);
    }
}
