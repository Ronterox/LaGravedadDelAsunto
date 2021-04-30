using Combat;
using Managers;
using UnityEngine;

namespace Inventory_System
{
    
    [CreateAssetMenu(fileName = "New weapon", menuName = "Penguins Mafia/Items/Weapon")]
    public class WeaponItem : Item
    {
        public override void Use()
        {
            CharacterCombat combat = CharacterCombat.Instance;
            Inventory inventory = GameManager.Instance.inventory;

            if (combat.weapon)
            {
                Destroy(combat.weapon.gameObject);
                if (!inventory.Add(combat.weapon.weaponItem)) inventory.SpawnItem(combat.weapon.weaponItem, combat.transform.position);
            }

            combat.weapon = Instantiate(itemRef).GetComponent<Weapon>();

            combat.SetWeapon();

            inventory.Remove(this);
        }
    }
}
