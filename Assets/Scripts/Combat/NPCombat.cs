using UnityEngine;

namespace Combat
{
    public class NPCombat : MonoBehaviour
    {
        public Transform weaponHolder;

        public Weapon sword;

        private void Start() => SetWeapon();

        public void SetWeapon()
        {
            Transform swordTransform = sword.transform;
            swordTransform.parent = weaponHolder;
            swordTransform.position = weaponHolder.position;
            sword.myDamageable = GetComponent<Damageable>();
        }

        public void Attack(int isAttacking) => sword.SetCollider(isAttacking == 1);
    }
}
