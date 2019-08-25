using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortalService {


    public enum DeathAnimation { None, Slip, Dead }

    private IMortal mortal;
    private Dictionary<LevelUtil.LevelType, AudioClip> sfxThudMap;
    public MortalService(IMortal mortal)
    {
        this.mortal = mortal;
        sfxThudMap = new Dictionary<LevelUtil.LevelType, AudioClip>();
        Array.ForEach(mortal.sfxThuds, e => sfxThudMap.Add(e.levelType, e.audioClips[0]));
        
    }

    public void StartDieAnimation(DeathAnimation deathAnimation)
    {
        mortal.animator.SetBool(deathAnimation.ToString(), true);
    }
    public void StopDieAnimation()
    {
        mortal.animator.SetBool("Dead", false);
        mortal.animator.SetBool("Slip", false);
    }
    public AudioClip GetSfxThud(LevelUtil.LevelType levelType)
    {
        return sfxThudMap[levelType];
    }
}
