using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace BehaviorBricks.Actions.System.GameObject
{
    /// <summary>
    /// It is an action to destroy the associated game object.
    /// </summary>
    [Action("GameObject/Destroy")]
    [Help("Destroys the gameobject")]
    public class Destroy : GOAction
    {
        ///<value>Input Game object to be destroyed Parameter.</value>
        [InParam("game object")]
        [Help("Game object to be destroyed, if no assigned the game object of this behavior will be destroyed")]
        public UnityEngine.GameObject targetGameobject;

        /// <summary>Initialization Method of Destroy.</summary>
        /// <remarks>Destroy the associated GameObject.</remarks>
        public override void OnStart()
        {
            if (!targetGameobject) targetGameobject = gameObject;
            Object.Destroy(targetGameobject);
        }

        /// <summary>Method of Update of Destroy.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate() => TaskStatus.COMPLETED;
    }
}
