using Player;
using UnityEngine;

namespace Animations
{
    public class BlockMovementOnAnimation : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => PlayerController.Instance.BlockMovement(true);

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => PlayerController.Instance.BlockMovement(false);

    }
}
