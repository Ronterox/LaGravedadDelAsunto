using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Plugins.Tools;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviours
{
    [Action("Navigation/MoveToGameObject")]
    [Help("Moves to the targeted gameObject")]
    public class MoveToGameObject : GOAction
    {
        [InParam("Target GameObject")]
        [Help("The target gameObject to go to")]
        public GameObject target;
        private Transform m_TargetTransform;

        [InParam("Reach Distance")]
        [Help("What distance is accepted as reached")]
        public float reachDistance;

        [InParam("NavMeshAgent")]
        [Help("The navigation agent of the object to move")]
        public NavMeshAgent navMeshAgent;

        public override void OnStart()
        {
            if (!navMeshAgent) navMeshAgent = gameObject.GetComponentSafely<NavMeshAgent>();
            navMeshAgent.isStopped = false;

            m_TargetTransform = target.transform;
        }

        public override TaskStatus OnUpdate()
        {
            navMeshAgent.SetDestination(m_TargetTransform.position);
            return navMeshAgent.remainingDistance > reachDistance ? TaskStatus.RUNNING : TaskStatus.COMPLETED;
        }
    }
}
