using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerService  {

    private Animator animator;
    public AttackerService(Animator animator)
    {
        this.animator = animator;
    }

    public void StartAttackAnimation()
    {
        animator.SetBool("Attack", true);
    }

    public void StopAttackAnimation()
    {
        animator.SetBool("Attack", false);
    }
}
