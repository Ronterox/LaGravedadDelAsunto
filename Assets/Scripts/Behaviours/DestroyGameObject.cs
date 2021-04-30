using Pada1.BBCore;
using Pada1.BBCore.Framework;
using UnityEngine;

namespace Behaviours
{
    [Action("Basic/DestroyGameObject")]
    [Help("Destroys a monoBehaviour")]
    public class DestroyGameObject : BasePrimitiveAction
    {
        [InParam("GameObject")]
        [Help("The monoBehaviour to be destroyed")]
        public GameObject destroyable;

        [InParam("Delay")]
        [Help("Delay until destroyed")]
        public float delay;
        
        public override void OnStart() => Object.Destroy(destroyable, delay);
    }
}
