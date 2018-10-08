using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Bomb : Entity, IAttacker {

    public enum Animation { None, Explode }
    public enum WalkerDeathAnimation {Slip }

    public WalkerDeathAnimation walkerDeathAnimation;
    public bool affectsEnemy;
    public bool affectsPlayer;
    public int numOfUses;

    public bool inactive
    {
        get;set;
    }

    private AttackerService attackerService;

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

    // Use this for initialization
    void Start () {
        Debug.Log(this + " Created:" + " x=" + x + " y=" + y + " affectsEnemy=" + affectsEnemy + " affectsPlayer="+ affectsPlayer+ " numOfUses="+ numOfUses+ " inactive="+ inactive);
        attackerService = new AttackerService(animator);
    }
	
    public string GetPlayerLoseAnimationName()
    {
        switch (walkerDeathAnimation)
        {
            case WalkerDeathAnimation.Slip:
            default:
                return "Slip";

        }
    }


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
            throw new NotImplementedException();
        }
    }
}
