using Inventory_System;
using Managers;
using NPCs;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Plugins.Tools;

namespace Behaviours
{
    [Action("LGA/GiveItemToPlayer")]
    [Help("Gives an random destroyable from a list to a player")]
    public class GiveItemToPlayer : BasePrimitiveAction
    {
        [InParam("Npc")]
        [Help("The npc to give the items")]
        public NPC npc;

        public override void OnStart()
        {
            Inventory inventory = GameManager.Instance.inventory;
            Item item = npc.itemsToGive.GetRandom();
            
            if (!inventory.Add(item)) inventory.SpawnItem(item, npc.transform.position);
        }
    }
}
