using BBUnity.Actions;
using Pada1.BBCore;
using Plugins.Tools;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviours
{
    [Action("Navigation/MoveAwayFromTransform")]
    [Help("Moves the game object to the contrary position of a transform by using a NavMeshAgent")]
    public class MoveAwayFromTransform : GOAction
    {
        [InParam("Target GameObject")]
        [Help("The target to move away from")]
        public GameObject gameObjectTransform;

        private NavMeshAgent m_AgentNav;
        private Transform m_TargetTransform;

        public override void OnStart()
        {
            if (!m_TargetTransform) m_TargetTransform = gameObjectTransform.transform;
            if (!m_AgentNav) m_AgentNav = gameObject.GetComponentSafely<NavMeshAgent>();

            Vector3 targetPosition = m_TargetTransform.position;
            m_AgentNav.SetDestination(new Vector3(-targetPosition.x, gameObject.transform.position.y, -targetPosition.z));
            m_AgentNav.isStopped = false;
        }

        public override void OnAbort()
        {
            if (m_AgentNav) m_AgentNav.isStopped = true;
        }
    }
}
