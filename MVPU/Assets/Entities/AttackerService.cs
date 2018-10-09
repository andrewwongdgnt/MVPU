using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerService  {

    private IAttacker attacker;
    public AttackerService(IAttacker attacker)
    {
        this.attacker = attacker;
    }

    public void StartAttackAnimation()
    {
        attacker.animator.SetBool("Attack", true);
    }

    public void StopAttackAnimation()
    {
        attacker.animator.SetBool("Attack", false);
    }
}
