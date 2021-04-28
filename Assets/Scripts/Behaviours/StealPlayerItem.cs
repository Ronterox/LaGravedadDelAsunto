using Inventory_System;
using Managers;
using NPCs;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Plugins.Tools;

namespace Behaviours
{
    [Action("LGA/StealPlayerItem")]
    [Help("Steals an item from the player")]
    public class StealPlayerItem : BasePrimitiveAction
    {
        [InParam("Npc")]
        [Help("The npc who steals the items")]
        public NPC npc;

        private bool m_StoleAnItem;

        public override void OnStart()
        {
            Inventory inventory = GameManager.Instance.inventory;

            m_StoleAnItem = inventory.items.Count != 0;
            
            if (!m_StoleAnItem) return;
            
            inventory.Drop(inventory.items.GetRandom(), npc.transform.position);
        }

        public override TaskStatus OnUpdate() => m_StoleAnItem ? TaskStatus.COMPLETED : TaskStatus.FAILED;
    }
}
