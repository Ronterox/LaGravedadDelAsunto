using System.Collections;
using System.Collections.Generic;
using Player;

using UnityEngine;
namespace Combat
{
    public class Weapon : MonoBehaviour
    {
        public BoxCollider AttackCollider;
        public int damage;

        public void Attack(CharacterHealth targethealth)
        {

            targethealth.TakeDamage(damage);
        }

        public void SetCollider(bool isenable)
        {
            AttackCollider.enabled = isenable;
          

        }

        private void OnTriggerEnter(Collider other)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy!=null)
            {
                Attack(enemy.myHealth);
            }
        }
    }

}

