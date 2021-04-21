using UnityEngine;

namespace Combat
{
    public class CharacterHealth : MonoBehaviour
    {
        public int maxHealth = 100;
        public int currentHealth { get; private set; }
        
        public delegate void DieEvent();
        public event DieEvent die;

        private void Awake() => currentHealth = maxHealth;

        public void TakeDamage(int damageValue)
        {
            currentHealth -= damageValue;
            Debug.Log(transform.name + "takes" + damageValue + "damage");
            if (currentHealth <= 0) die?.Invoke();
        }
    }
}
