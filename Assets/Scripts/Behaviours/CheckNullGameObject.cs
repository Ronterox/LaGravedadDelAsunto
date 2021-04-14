using Pada1.BBCore;
using Pada1.BBCore.Framework;
using UnityEngine;

namespace Behaviours
{
    [Condition("Basic/IsGameObjectNull")]
    [Help("Checks whether the gameObject is null")]
    public class CheckNullGameObject : ConditionBase
    {
        [InParam("Game Object")]
        [Help("The gameObject to be checked")]
        public GameObject gameObject;

        public override bool Check() => !gameObject;
    }
}
