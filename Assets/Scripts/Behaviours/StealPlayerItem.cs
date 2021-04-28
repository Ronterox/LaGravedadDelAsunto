using Inventory_System;
using Managers;
using NPCs;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Plugins.Tools;

namespace Behaviours
{
    [Action("LGA/StealPlayerItem")]
    [Help("")]
    public class StealPlayerItem : BasePrimitiveAction
    {
        [InParam("Npc")]
        [Help("The npc who steals the items")]
        public NPC npc;

        [InParam("Dialogues Ids")]
        [Help("The ids of the dialogues to say after doing the action")]
        public string[] dialogues;

        public override void OnStart()
        {
            Inventory inventory = GameManager.Instance.inventory;
            
            inventory.Drop(inventory.items.GetRandom(), npc.transform.position);

            npc.Say(dialogues.GetRandom());
        }
    }
}
