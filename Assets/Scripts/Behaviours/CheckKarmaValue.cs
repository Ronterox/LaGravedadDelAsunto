using Managers;
using Pada1.BBCore;
using Pada1.BBCore.Framework;

namespace Behaviours
{
    [Condition("LGA/CheckKarma")]
    [Help("Checks to whether the player karma es greater or lesser than a value")]
    public class CheckKarmaValue : ConditionBase
    {
        [InParam("Karma Value")]
        [Help("The karma quantity to compare to")]
        public int karma;
        
        [InParam("Check If Greater")]
        [Help("Select whether to check if is greater, or lesser")]
        public bool isGreater;

        public override bool Check() => isGreater ? karma > GameManager.Instance.karmaController.karma : karma < GameManager.Instance.karmaController.karma;
    }
}
