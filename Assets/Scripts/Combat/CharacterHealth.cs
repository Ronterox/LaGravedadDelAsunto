using UnityEngine;

namespace Combat
{
    public class CharacterHealth : MonoBehaviour
    {
        public int maxHealth = 100;
        public int currentHealth { get; private set; }

        private void Awake() => currentHealth = maxHealth;

        public void TakeDamage(int damageValue)
        {
            currentHealth -= damageValue;
            Debug.Log(transform.name + "takes" + damageValue + "damage");
            if (currentHealth <= 0) Die();
        }

        public virtual void Die() => Destroy(gameObject);
    }
}
