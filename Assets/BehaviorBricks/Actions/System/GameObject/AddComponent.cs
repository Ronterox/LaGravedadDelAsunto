using System;
using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;

namespace BehaviorBricks.Actions.System.GameObject
{
    /// <summary>
    /// It is an action to add a component to a GameObject.
    /// </summary>
    [Action("GameObject/AddComponent")]
    [Help("Adds a component to the game object")]
    public class AddComponent : GOAction
    {
        /// <summary>All Input Parameters of PlayAnimation action.</summary>
        ///<value>Type of the component that must be added.</value>
        [InParam("type")]
        [Help("Type of the component that must be added")]
        public string type;

        ///<value>Game object to add the component.</value>
        [InParam("game object")]
        [Help("Game object to add the component, if no assigned the component is added to the game object of this behavior")]
        public UnityEngine.GameObject targetGameobject;

        ///<summary>Initialization Method of AddComponent.</summary>
        ///<remarks>Check if there is an associated Gameobject and if you have the component it is added.</remarks>
        public override void OnStart()
        {
            if (!targetGameobject) targetGameobject = gameObject;
            if (!targetGameobject.GetComponent(type)) targetGameobject.AddComponent(Type.GetType(type));
        }
        /// <summary>Method of Update of AddComponent.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate() => TaskStatus.COMPLETED;
    }
}
