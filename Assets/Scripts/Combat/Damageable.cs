using Animations;
using Inventory_System;
using Managers;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(CharacterHealth))]
    public class Damageable : MonoBehaviour
    {
        public CharacterHealth myHealth;
        public Item dropItem;
        
        private RagdollScript m_RagdollScript;
        public bool InCombat { get; private set; }

        private void OnEnable() => myHealth.die += Die;
        private void OnDisable() => myHealth.die -= Die;

        private void Start()
        {
            if(!myHealth) myHealth = GetComponent<CharacterHealth>();
        }
        
        public virtual void Die()
        {
            if(dropItem) GameManager.Instance.inventory.Drop(dropItem, transform.position);
            if(m_RagdollScript) m_RagdollScript.EnableRagdoll();
        }
    }
}
