using Inventory_System;
using Questing_System;

namespace NPCs
{
    public class Monster : NPC
    {
        public Item favoriteFood;
        
        protected override void OnQuestCompletedInteraction(Quest quest) { }

        protected override void OnInteractionRangeEnter(Quest quest) { }

        protected override void OnInteractionRangeExit(Quest quest) { }

        protected override void OnInteraction(Quest quest) { }
    }
}
