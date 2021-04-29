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
        public ParticleSystem swordParticle;

        private void Awake()
        {
            if (!attackCollider) attackCollider = gameObject.GetComponentSafely<Collider>();
            SetCollider(false);
        }

        public void Attack(CharacterHealth targetHealth) => targetHealth.TakeDamage(damage);

        public void SetCollider(bool isEnable)
        {
            attackCollider.enabled = isEnable;
            if (isEnable)
            {
                swordParticle.Play();
            }
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
