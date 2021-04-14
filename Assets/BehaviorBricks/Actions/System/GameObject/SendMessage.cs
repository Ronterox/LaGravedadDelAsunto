using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;

namespace BehaviorBricks.Actions.System.GameObject
{
    /// <summary>
    /// It is an action for Calls the method named methodName on every MonoBehaviour in this game object.
    /// </summary>
    [Action("GameObject/SendMessage")]
    [Help("Calls the method named methodName on every MonoBehaviour in this game object")]
    public class SendMessage : GOAction
    {
        ///<value>Input Name of the method that must be called Parameter.</value>
        [InParam("methodName")]
        [Help("Name of the method that must be called")]
        public string methodName;

        ///<value>Input Game object to send the message Parameter.</value>
        [InParam("game object")]
        [Help("Game object to send the message, if no assigned the message will be sent to the game object of this behavior")]
        public UnityEngine.GameObject targetGameobject;

        /// <summary>Initialization Method of SendMessage.</summary>
        /// <remarks>If targetGameObject not null calls the method named methodName on every MonoBehaviour in that game object.</remarks>
        public override void OnStart()
        {
            if (!targetGameobject) targetGameobject = gameObject;
            targetGameobject.SendMessage(methodName);
        }

        /// <summary>Method of Update of SendMessage.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate() => TaskStatus.COMPLETED;
    }
}
