using UnityEngine;

namespace Combat
{
    public class Weapon : MonoBehaviour
    {
        public BoxCollider AttackCollider;
        public int damage;

        public void Attack(CharacterHealth targetHealth) => targetHealth.TakeDamage(damage);

        public void SetCollider(bool isEnable) => AttackCollider.enabled = isEnable;

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<Enemy>();
            if (enemy)
            {
                Attack(enemy.myHealth);
            }
        }
    }

}
