using Pada1.BBCore;
using Pada1.BBCore.Framework;
using UnityEngine;

namespace Behaviours
{
    [Action("Basic/DestroyGameObject")]
    [Help("Destroys a gameObject")]
    public class DestroyGameObject : BasePrimitiveAction
    {
        [InParam("GameObject")]
        [Help("The gameObject to be destroyed")]
        public GameObject destroyable;

        [InParam("Delay")]
        [Help("Delay until destroyed")]
        public float delay;
        
        public override void OnStart() => Object.Destroy(destroyable, delay);
    }
}
