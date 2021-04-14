using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorBricks.Actions.System.Navigation
{
    /// <summary>
    /// It is an action to move towards the given goal using a NavMeshAgent.
    /// </summary>
    [Action("Navigation/MoveToGameObject")]
    [Help("Moves the game object towards a given target by using a NavMeshAgent")]
    public class MoveToGameObject : GOAction
    {
        ///<value>Input target game object towards this game object will be moved Parameter.</value>
        [InParam("target")]
        [Help("Target game object towards this game object will be moved")]
        public UnityEngine.GameObject target;

        private NavMeshAgent navAgent;

        private Transform targetTransform;

        /// <summary>Initialization Method of MoveToGameObject.</summary>
        /// <remarks>Check if GameObject object exists and NavMeshAgent, if there is no NavMeshAgent, the default one is added.</remarks>
        public override void OnStart()
        {
            if (!target)
            {
                Debug.LogError("The movement target of this game object is null", gameObject);
                return;
            }
            targetTransform = target.transform;

            navAgent = gameObject.GetComponent<NavMeshAgent>();
            if (!navAgent)
            {
                Debug.LogWarning("The " + gameObject.name + " game object does not have a Nav Mesh Agent component to navigate. One with default values has been added", gameObject);
                navAgent = gameObject.AddComponent<NavMeshAgent>();
            }
            navAgent.SetDestination(targetTransform.position);

            navAgent.isStopped = false;
        }

        /// <summary>Method of Update of MoveToGameObject.</summary>
        /// <remarks>Verify the status of the task, if there is no objective fails, if it has traveled the road or is near the goal it is completed
        /// y, the task is running, if it is still moving to the target.</remarks>
        public override TaskStatus OnUpdate()
        {
            if (!target) return TaskStatus.FAILED;
            if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance) return TaskStatus.COMPLETED;
            if (navAgent.destination != targetTransform.position) navAgent.SetDestination(targetTransform.position);
            return TaskStatus.RUNNING;
        }
        /// <summary>Abort method of MoveToGameObject </summary>
        /// <remarks>When the task is aborted, it stops the navAgentMesh.</remarks>
        public override void OnAbort()
        {
            if (navAgent) navAgent.isStopped = true;
        }
    }
}
