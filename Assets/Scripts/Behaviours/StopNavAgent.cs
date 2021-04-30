using Pada1.BBCore;
using Pada1.BBCore.Framework;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviours
{
    [Action("Navigation/StopNavigationAgentMesh")]
    [Help("Stops the navigation agent movement if found")]
    public class StopNavAgent : BasePrimitiveAction
    {
        [InParam("Agent Navigation")]
        [Help("The component of type NavMeshAgent")]
        public NavMeshAgent agent;
        
        public override void OnStart()
        {
            base.OnStart();
            if (agent) agent.isStopped = true;
            else Debug.LogError("Missing component NavMeshAgent");
        }
    }
}
