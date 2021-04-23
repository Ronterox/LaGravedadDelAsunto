using UnityEngine;
using UnityEngine.Events;

namespace Combat
{
    public class CharacterHealth : MonoBehaviour
    {
        public int maxHealth = 100;
        public int currentHealth { get; private set; }
        
        public UnityEvent onDieEvent;
        public UnityEvent onTakeDamageEvent;

        public bool IsDead { get; private set; }

        private void Awake() => currentHealth = maxHealth;

        public void TakeDamage(int damageValue)
        {
            if(IsDead) return;
            
            currentHealth -= damageValue;
            if (currentHealth <= 0)
            {
                onDieEvent?.Invoke();
                IsDead = true;
            }
            else onTakeDamageEvent?.Invoke();
        }

        public void AddListeners(UnityAction onDie, UnityAction onTakeDamage)
        {
            if(onDie != null) onDieEvent.AddListener(onDie);
            if(onTakeDamage != null) onTakeDamageEvent.AddListener(onTakeDamage);
        }
        
        public void RemoveListeners(UnityAction onDie, UnityAction onTakeDamage)
        {
            if(onDie != null) onDieEvent.RemoveListener(onDie);
            if(onTakeDamage != null) onTakeDamageEvent.RemoveListener(onTakeDamage);
        }
    }
}
