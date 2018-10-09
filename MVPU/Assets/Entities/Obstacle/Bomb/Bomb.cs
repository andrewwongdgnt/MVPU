using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Bomb : Entity, IAttacker {

    public enum Animation { None, Explode }

    public MortalService.DeathAnimation mortalDeathAnimation;
    public bool affectsEnemy;
    public bool affectsPlayer;
    public int numOfUses;
    public AudioClip sfxHitClip;

    public bool inactive
    {
        get;set;
    }

    private AttackerService _attackerService;
    private AttackerService attackerService
    {
        get
        {
            if (_attackerService == null)
            {
                _attackerService = new AttackerService(this);
            }
            return _attackerService;
        }
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log(this + " Created:" + " x=" + x + " y=" + y + " affectsEnemy=" + affectsEnemy + " affectsPlayer=" + affectsPlayer + " numOfUses=" + numOfUses + " inactive=" + inactive);
    }

    protected override void BuildAdditionalStateDict(Dictionary<string, object> dict)
    {
        dict.Add("affectsEnemy", affectsEnemy);
        dict.Add("affectsPlayer", affectsPlayer);
        dict.Add("numOfUses", numOfUses);
        dict.Add("inactive", inactive);
    }
    protected override void RestoreAdditionalState(Dictionary<string, object> dict)
    {
        foreach (KeyValuePair<string, object> entry in dict)
        {
            if (entry.Key == "affectsEnemy" && entry.Value is bool)
                affectsEnemy = (bool)entry.Value;
            else if (entry.Key == "affectsPlayer" && entry.Value is bool)
                affectsPlayer = (bool)entry.Value;
            else if (entry.Key == "numOfUses" && entry.Value is int)
                numOfUses = (int)entry.Value;
            else if (entry.Key == "inactive" && entry.Value is bool)
                inactive = (bool)entry.Value;
        }
    }

    //---------------
    // IAttacker Impl
    //---------------

    public void StartAttackAnimation()
    {
        attackerService.StartAttackAnimation();
    }
    public void StopAttackAnimation()
    {
        attackerService.StopAttackAnimation();
    }

    AudioClip IAttacker.sfxHitClip
    {
        get
        {
            return sfxHitClip;
        }
    }

    public bool attackDelayed
    {
        get
        {
            return false;
        }
    }

    MortalService.DeathAnimation IAttacker.mortalDeathAnimation
    {
        get
        {
            return mortalDeathAnimation;
        }
    }
}
