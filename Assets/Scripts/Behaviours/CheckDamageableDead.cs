using Combat;
using Pada1.BBCore;
using Pada1.BBCore.Framework;

namespace Behaviours
{
    [Condition("Status/IsDead")]
    [Help("Checks if the damageable is dead")]
    public class CheckDamageableDead : ConditionBase
    {
        [InParam("Damageable")]
        [Help("The damageable of the character to check")]
        public Damageable damageable;

        public override bool Check() => damageable.myHealth.IsDead;
    }
}
