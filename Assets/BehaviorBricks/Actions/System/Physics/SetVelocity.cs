using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace BehaviorBricks.Actions.System.Physics
{
    /// <summary>
    /// It is an action to apply speed to the GameObject, the result will cause the object to move.
    /// </summary>
    [Action("Physics/SetVelocity")]
    [Help("Sets a velocity to the game object. As a result the game object will start moving")]
    public class SetVelocity : GOAction
    {
        ///<value>Input Game object where the velocity is set Parameter.</value>
        [InParam("toSetVelocity")]
        [Help("Game object where the velocity is set, if no assigned the velocity is set to the game object of this behavior")]
        public UnityEngine.GameObject toSetVelocity;

        ///<value>Input Velocity Parameter.</value>
        [InParam("velocity")]
        [Help("Velocity")]
        public UnityEngine.Vector3 velocity;

        /// <summary>Initialization Method of SetVelocity</summary>
        /// <remarks>Check the GameObject which we apply the speed, look for the rigidbody component to add the speed
        /// and if it does not exist, it adds a rigidbody by default.</remarks>
        public override void OnStart()
        {
            if (!toSetVelocity) toSetVelocity = gameObject;
            
            if (toSetVelocity.TryGetComponent(out Rigidbody rigidbody)) rigidbody.velocity = velocity;
            else toSetVelocity.AddComponent<Rigidbody>().velocity = velocity;
        }

        /// <summary>Abort method of SetVelocity.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate() => TaskStatus.COMPLETED;
    }
}
