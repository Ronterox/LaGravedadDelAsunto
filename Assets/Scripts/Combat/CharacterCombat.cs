using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    public float attackDelay = .6f;
    CharacterStats myStats;
    public float combatCooldown = 2;
    public float lastAttackTime;
    public bool inCombat { get; private set; }

    public event System.Action onAttack;
    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }
    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        if (Time.time - lastAttackTime > combatCooldown)
        {
            inCombat = false;
        }
        if (PlayerInput.Instance.Attack)
        {
            inCombat = true;
        }
    }
    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats,attackDelay));
            if (onAttack != null) onAttack();
          
            attackCooldown = 1f / attackSpeed;
            inCombat = true;
            lastAttackTime = Time.time;
        }
        
    }

    IEnumerator DoDamage(CharacterStats stats,float delay)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.damage.GetValue());
        if (stats.currentHealth <= 0)
        {
            inCombat = false;
        }
    }
}
