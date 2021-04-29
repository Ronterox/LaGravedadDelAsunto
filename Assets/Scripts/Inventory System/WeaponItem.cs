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

            if (!inventory.Add(combat.sword.weaponItem)) inventory.SpawnItem(combat.sword.weaponItem, combat.transform.position);

            combat.sword = Instantiate(itemRef).GetComponent<Weapon>();

            combat.SetWeapon();

            inventory.Remove(this);
        }
    }
}
