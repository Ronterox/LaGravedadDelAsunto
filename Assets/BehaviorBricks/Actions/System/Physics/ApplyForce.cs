using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace BehaviorBricks.Actions.System.Physics
{
    /// <summary>
    /// It is an action to apply force to the GameObject, the result will cause the object to move.
    /// </summary>
    [Action("Physics/ApplyForce")]
    [Help("Adds a force to the game object. As a result the game object will start moving")]
    public class ApplyForce : GOAction
    {
        ///<value>Input Game object where the force is applied Parameter.</value>
        [InParam("toApplyForce")]
        [Help("Game object where the force is applied, if no assigned the force is applied to the game object of this behavior")]
        public UnityEngine.GameObject toApplyForce;

        ///<value>Input Force to be applied Parameter.</value>
        [InParam("force")]
        [Help("Force to be applied")]
        public UnityEngine.Vector3 force;

        /// <summary>Initialization Method of ApplyForce.</summary>
        /// <remarks>checks the GameObject which we apply the force, look for the rigidbody component to add strength
        /// and if it does not exist, it adds rigidbody by default.</remarks>
        public override void OnStart()
        {
            if (!toApplyForce) toApplyForce = gameObject;
            if (toApplyForce.TryGetComponent(out Rigidbody rigidbody)) rigidbody.AddForce(force);
            else toApplyForce.AddComponent<Rigidbody>().AddForce(force);
        }

        /// <summary>Abort method of ApplyForce.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate() => TaskStatus.COMPLETED;
    }
}
