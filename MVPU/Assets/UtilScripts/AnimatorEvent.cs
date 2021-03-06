﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvent : MonoBehaviour {

    public GameModel gameModel { private get; set; }

    //Attacker Section
    public IAttacker attacker { private get; set; }
    public void AttackerAttackEvent()
    {
        IMortal mortal = gameModel.findMortalInSamePositionAs(attacker.entity);
        if (mortal == null)
            return;
        mortal.StartDieAnimation(attacker.opponentDeathAnimation);
        AudioUtil.PlaySFX(attacker.audioSource, attacker.sfxAttackClip);
    }

    //Player Section
    public void PlayerDiedEvent()
    {
        gameModel.ShowLoseMenu();
    }

    public void PlayerWinEvent()
    {
        gameModel.ShowWinMenu();
    }

    //Walker Section
    public IWalker walker { private get; set; }
    public void WalkerFootStepEvent()
    {
        LevelUtil.LevelType levelType = gameModel.currentLevelType;
        AudioClip sfxFootStep = walker.GetResolvedSfxFootStep(levelType);

        AudioUtil.PlaySFX(walker.audioSource, sfxFootStep);
    }

    //Mortal Section
    public IMortal mortal { private get; set; }
    public void MortalThudEvent()
    {
        LevelUtil.LevelType levelType = gameModel.currentLevelType;
        AudioClip sfxThud = mortal.GetResolvedSfxThud(levelType);
        AudioUtil.PlaySFX(mortal.audioSource, sfxThud);
    }
    public void MortalHitEvent()
    {
        AudioUtil.PlaySFX(mortal.audioSource, mortal.sfxHitClip);
    }

    //Consumable Section
    public IConsumable consumable { private get; set; }
    public void ConsumableConsumedEvent()
    {
        AudioUtil.PlaySFX(consumable.audioSource, consumable.sfxConsumedClip);
    }
    public void ConsumableUsedEvent()
    {
        AudioUtil.PlaySFX(consumable.audioSource, consumable.sfxUsedClip);
    }

    //Switchable Section
    public ISwitchable switchable { private get; set; }
    public void SwitchableTransitionOnEvent()
    {
        AudioUtil.PlaySFX(switchable.audioSource, switchable.sfxTransitionOnClip);
    }
    public void SwitchableTransitionOffEvent()
    {
        AudioUtil.PlaySFX(switchable.audioSource, switchable.sfxTransitionOffClip);
    }

    //Celebrator Section
    public ICelebrator celebrator { private get; set; }
    public void CelebratorCelebrateStepEvent()
    {
        LevelUtil.LevelType levelType = gameModel.currentLevelType;
        AudioClip sfxCelebrateStep = celebrator.GetResolvedSfxCelebrateStep(levelType);

        AudioUtil.PlaySFX(celebrator.audioSource, sfxCelebrateStep);
    }

    //Generic audio event
    public AudioSource genericAudioSource { private get; set; }
    public AudioClip genericAudioClip { private get; set; }
    public void AudioEvent()
    {
        AudioUtil.PlaySFX(genericAudioSource, genericAudioClip);
    }
}
