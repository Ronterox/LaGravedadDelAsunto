using Combat;
using Pada1.BBCore;
using Pada1.BBCore.Framework;

namespace Behaviours
{
    [Condition("Status/IsInCombat")]
    [Help("Checks whether the damageable is in combat")]
    public class CheckInCombat : ConditionBase
    {
        [InParam("Damageable")]
        [Help("The damageable component")]
        public Damageable damageable;

        public override bool Check() => damageable.InCombat;
    }
}
