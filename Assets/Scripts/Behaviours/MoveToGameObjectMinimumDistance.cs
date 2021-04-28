using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Plugins.Tools;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviours
{
    [Action("Navigation/MoveToGameObjectMinimumDistance")]
    [Help("Moves to the targeted gameObject")]
    public class MoveToGameObjectMinimumDistance : GOAction
    {
        [InParam("Target GameObject")]
        [Help("The target gameObject to go to")]
        public GameObject target;
        private Transform m_TargetTransform;

        [InParam("Reach Distance")]
        [Help("What distance is accepted as reached")]
        public float reachDistance;

        [InParam("Follow Speed")]
        [Help("The speed to follow with")]
        public float followingSpeed;
        private float m_DefaultSpeed;

        [InParam("NavMeshAgent")]
        [Help("The navigation agent of the object to move")]
        public NavMeshAgent navMeshAgent;

        public override void OnStart()
        {
            if (!navMeshAgent) navMeshAgent = gameObject.GetComponentSafely<NavMeshAgent>();
            navMeshAgent.isStopped = false;

            m_DefaultSpeed = navMeshAgent.speed;
            navMeshAgent.speed = followingSpeed;

            m_TargetTransform = target.transform;
        }

        public override TaskStatus OnUpdate()
        {
            navMeshAgent.SetDestination(m_TargetTransform.position);
            
            if (navMeshAgent.remainingDistance > reachDistance) return TaskStatus.RUNNING;
            
            navMeshAgent.speed = m_DefaultSpeed;
            return TaskStatus.COMPLETED;
        }
    }
}
