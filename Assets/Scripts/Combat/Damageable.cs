using System.Collections;
using Animations;
using GUI;
using Inventory_System;
using Managers;
using Plugins.Tools;
using UnityEngine;

namespace Combat
{
    public enum GuyType { Goodguy, Badguy }
    
    [RequireComponent(typeof(CharacterHealth))]
    [RequireComponent(typeof(Timer))]
    public class Damageable : MonoBehaviour
    {
        public CharacterHealth myHealth;
        
        public GuyType type;
        public int karmaOnAttack, karmaOnKill;

        [Header("When Dead")]
        public float secondsToDisappear;
        public Item dropItem;
        public int quantityToDrop = 1;

        private RagdollScript m_RagdollScript;

        public Timer timer;

        public bool InCombat { get; private set; }
        
        private void Awake()
        {
            if (!myHealth) myHealth = GetComponent<CharacterHealth>();
            if (!m_RagdollScript) m_RagdollScript = GetComponent<RagdollScript>();
            if (!timer) timer = GetComponent<Timer>();
        }

        private void OnEnable()
        {
            myHealth.AddListeners(Die, EnterCombat);
            timer.AddListeners(null, ExitCombat);
        }
        
        private void OnDisable()
        {
            myHealth.RemoveListeners(Die, EnterCombat);
            timer.RemoveListeners(null, ExitCombat);
        }

        private void EnterCombat()
        {
            if (InCombat) timer.ResetTimer();
            else
            {
                ChangeKarma(karmaOnAttack);
                InCombat = true;
                timer.StartTimer();
            }
        }

        private void ExitCombat() => InCombat = false;

        public void Die()
        {
            ArchievementsManager.Instance.UpdateAchievement("trophy1", 1);
            
            if (dropItem) GameManager.Instance.inventory.SpawnItems(dropItem, transform.position, quantityToDrop);
            if (m_RagdollScript) m_RagdollScript.EnableRagdoll();
            
            ChangeKarma(karmaOnKill);
            ExitCombat();
            
            StartCoroutine(SetActive(gameObject, false, secondsToDisappear));
        }

        private void ChangeKarma(int quantity) => GameManager.Instance.karmaController.ChangeKarma(type == GuyType.Goodguy? -quantity : quantity);

        private IEnumerator SetActive(GameObject obj, bool active, float delay)
        {
            yield return new WaitForSeconds(delay);
            obj.SetActive(active);
        }
    }
}
