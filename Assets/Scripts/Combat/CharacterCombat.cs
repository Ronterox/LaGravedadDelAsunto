using Inventory_System;
using Managers;
using Player;
using Plugins.Persistence;
using Plugins.Tools;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(CharacterHealth))]
    public class CharacterCombat : Singleton<CharacterCombat>, IDataPersister
    {
        public Transform weaponHolder;

        public Weapon weapon;

        private Animator m_Animator;

        private readonly int ATTACK_HASH = Animator.StringToHash("Attack");

        public DataSettings dataSettings;

        private void Start()
        {
            if (weapon) SetWeapon();
        }

        private void OnEnable() => PersistentDataManager.LoadPersistedData(this);

        private void OnDisable() => PersistentDataManager.SavePersistedData(this);

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
            if (weapon) weapon.SetCollider(isAttacking == 1);
        }

        public DataSettings GetDataSettings() => dataSettings;

        public void SetDataSettings(ushort dataId, DataSettings.PersistenceType persistenceType) { }

        public Data SaveData() => new Data<WeaponItem>(weapon.weaponItem);

        public void LoadData(Data data)
        {
            WeaponItem item = ((Data<WeaponItem>)data).value;
            weapon = Instantiate(item.itemRef).GetComponent<Weapon>();
            SetWeapon();
        }
    }
}
