using Inventory_System;
using Managers;
using NPCs;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Plugins.Tools;

namespace Behaviours
{
    [Action("LGA/GiveItemToPlayer")]
    [Help("Gives an random destroyable from a list to a player, and then the npc says something")]
    public class GiveItemToPlayer : BasePrimitiveAction
    {
        [InParam("Items List")]
        [Help("Items that the npc can give")]
        public Item[] itemsToGive;

        [InParam("Npc")]
        [Help("The npc to give the items")]
        public NPC npc;

        [InParam("Dialogues Ids")]
        [Help("The ids of the dialogues to say after doing the action")]
        public string[] dialogues;

        public override void OnStart()
        {
            Inventory inventory = GameManager.Instance.inventory;
            Item item = itemsToGive.GetRandom();
            
            if (!inventory.Add(item)) inventory.SpawnItem(item, npc.transform.position);

            npc.Say(dialogues.GetRandom());
        }
    }
}
