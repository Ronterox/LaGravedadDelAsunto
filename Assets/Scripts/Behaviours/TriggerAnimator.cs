using BBUnity.Actions;
using Pada1.BBCore;
using Plugins.Tools;
using UnityEngine;

namespace Behaviours
{
    [Action("Animation/TriggerAnimator")]
    [Help("Triggers the animation string on the animator")]
    public class TriggerAnimator : GOAction
    {
        [InParam("Animator")]
        [Help("Animator to set the trigger from")]
        public Animator animator;

        [InParam("Parameter ID")]
        [Help("The parameter id in the animator")]
        public string parameterID;

        public override void OnStart()
        {
            if (!animator) animator = gameObject.GetComponentSafely<Animator>();
            animator.SetTrigger(parameterID);
        }
    }
}
