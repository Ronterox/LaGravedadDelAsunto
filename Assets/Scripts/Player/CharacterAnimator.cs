using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{

    protected Animator animator;
    protected CharacterCombat combat;

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

    }

    protected virtual void Update()
    {
        animator.SetBool("inCombat", combat.inCombat);
    }
}
