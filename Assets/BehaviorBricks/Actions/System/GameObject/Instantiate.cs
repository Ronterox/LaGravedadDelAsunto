using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace BehaviorBricks.Actions.System.GameObject
{
    /// <summary>
    ///It is an action to clone a GameObject.
    /// </summary>
    [Action("GameObject/Instantiate")]
    [Help("Clones the object original and returns the clone")]
    public class Instantiate : GOAction
    {

        ///<value>Input Object to be cloned Parameter.</value>
        [InParam("original")]
        [Help("Object to be cloned")]
        public UnityEngine.GameObject original;

        ///<value>Input position for the clone Parameter.</value>
        [InParam("position")]
        [Help("position for the clone")]
        public UnityEngine.Vector3 position;

        ///<value>OutPut instantiated game object Parameter.</value>
        [OutParam("instantiated")]
        [Help("Returned game object")]
        public UnityEngine.GameObject instantiated;


        /// <summary>Initialization Method of Instantiate.</summary>
        /// <remarks>Installed a GameObject in the position and type dice.</remarks>
        public override void OnStart() => original = Object.Instantiate(original,position,original.transform.rotation) as UnityEngine.GameObject;

        /// <summary>Method of Update of Instantiate.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate() => TaskStatus.COMPLETED;
    }
}
