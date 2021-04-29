using System;
using Inventory_System;
using Managers;
using Plugins.Tools;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(Collider))]
    public class Weapon : MonoBehaviour
    {
        public float knockBackForce;
        public Collider attackCollider;
        public int damage;

        public Damageable myDamageable;

        public bool isPlayer;
        private StatusEffectManager m_StatusEffectManager;

        public WeaponItem weaponItem;

        private void Awake()
        {
            if (!attackCollider) attackCollider = gameObject.GetComponentSafely<Collider>();
            SetCollider(false);
        }

        private void Start()
        {
            if (isPlayer) m_StatusEffectManager = StatusEffectManager.Instance;
        }

        public void Attack(CharacterHealth targetHealth) => targetHealth.TakeDamage(damage + (int)m_StatusEffectManager.damageAffection);

        public void SetCollider(bool isEnable) => attackCollider.enabled = isEnable;

        private void OnDrawGizmos()
        {
            if (!attackCollider.enabled) return;

            Gizmos.color = Color.red;
            Bounds bounds = attackCollider.bounds;
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }

        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<Damageable>();
            if (!damageable || damageable == myDamageable) return;

            Attack(damageable.myHealth);
            Vector3 direction = (transform.position - other.transform.position).normalized;
            other.GetComponent<Rigidbody>().AddForce(direction * knockBackForce);
        }
    }

}
