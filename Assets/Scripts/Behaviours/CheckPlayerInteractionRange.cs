using General.Utilities;
using Pada1.BBCore;
using Pada1.BBCore.Framework;

namespace Behaviours
{
    [Condition("Perception/IsPlayerOnInteractionRange")]
    [Help("Checks whether the player is on the interaction range")]
    public class CheckPlayerInteractionRange : ConditionBase
    {
        [InParam("Interactable")]
        [Help("The interactable class to check if is on range")]
        public Interactable interactable;

        public override bool Check() => interactable.IsPlayerOnRange;
    }
}
