﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttacker : IEntity
{
    //Obstacles can attack too.  EG, bomb will just blow up and banana will fly away

    //Should start the animation event "EndGameAttack()"
    void StartAttackAnimation();
    void StopAttackAnimation();

    MortalService.DeathAnimation opponentDeathAnimation { get; }

    AudioClip sfxAttackClip
    {
        get;
    }

    bool attackDelayed
    {
        get;
    }

}
