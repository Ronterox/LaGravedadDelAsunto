using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorBricks.Actions.System.Navigation
{
    /// <summary>
    /// It is an action to move the GameObject to a random position in an area using a NavMeshAgent.
    /// </summary>
    [Action("Navigation/MoveToRandomPosition")]
    [Help("Gets a random position from a given area and moves the game object to that point by using a NavMeshAgent")]
    public class MoveToRandomPosition : GOAction
    {
        private NavMeshAgent navAgent;

        ///<value>Input game object Parameter that must have a BoxCollider or SphereColider, which will determine the area from which the position is extracted.</value>
        [InParam("area")]
        [Help("game object that must have a BoxCollider or SphereColider, which will determine the area from which the position is extracted")]
        public UnityEngine.GameObject area;

        /// <summary>Initialization Method of MoveToRandomPosition.</summary>
        /// <remarks>Check if there is a NavMeshAgent to assign it one by default and assign it
        /// to the NavMeshAgent the destination a random position calculated with <see cref="getRandomPosition()"/> </remarks>
        public override void OnStart()
        {
            navAgent = gameObject.GetComponent<NavMeshAgent>();
            if (!navAgent)
            {
                Debug.LogWarning("The " + gameObject.name + " game object does not have a Nav Mesh Agent component to navigate. One with default values has been added", gameObject);
                navAgent = gameObject.AddComponent<NavMeshAgent>();
            }
            navAgent.SetDestination(getRandomPosition());
            navAgent.isStopped = false;
        }
        /// <summary>Method of Update of MoveToRandomPosition </summary>
        /// <remarks>Check the status of the task, if it has traveled the road or is close to the goal it is completed
        /// and otherwise it will remain in operation.</remarks>
        public override TaskStatus OnUpdate() => !navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance ? TaskStatus.COMPLETED : TaskStatus.RUNNING;

        private UnityEngine.Vector3 getRandomPosition()
        {
            if (area.TryGetComponent(out BoxCollider boxCollider))
            {
                UnityEngine.Vector3 position = area.transform.position;
                UnityEngine.Vector3 localScale = area.transform.localScale;
                UnityEngine.Vector3 size = boxCollider.size;
                return new UnityEngine.Vector3(Random.Range(position.x - localScale.x * size.x * 0.5f, position.x + localScale.x * size.x * 0.5f), position.y,
                                               Random.Range(position.z - localScale.z * boxCollider.size.z * 0.5f, position.z + localScale.z * size.z * 0.5f));
            }

            if (area.TryGetComponent(out SphereCollider sphereCollider))
            {
                float radius = sphereCollider.radius;
                
                float distance = Random.Range(-radius, area.transform.localScale.x * radius);
                float angle = Random.Range(0, 2 * Mathf.PI);
                
                UnityEngine.Vector3 position = area.transform.position;
                return new UnityEngine.Vector3(position.x + distance * Mathf.Cos(angle), position.y, position.z + distance * Mathf.Sin(angle));
            }
            
            return gameObject.transform.position + new UnityEngine.Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
        }
        /// <summary>Abort method of MoveToRandomPosition </summary>
        /// <remarks>When the task is aborted, it stops the navAgentMesh.</remarks>
        public override void OnAbort() => navAgent.isStopped = true;
    }
}
