using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerService
{

    private IWalker walker;
    private Dictionary<LevelUtil.LevelType, AudioClip[]> sfxFootStepMap;
    public WalkerService(IWalker walker)
    {
        this.walker = walker;
        sfxFootStepMap = new Dictionary<LevelUtil.LevelType, AudioClip[]>();
        Array.ForEach(walker.sfxFootSteps, e => sfxFootStepMap.Add(e.levelType, e.audioClips));
    }

    public void StartWalkAnimation()
    {
        walker.animator.SetBool("HorizontalWalk", true);
    }

    public void StopWalkAnimation()
    {
        walker.animator.SetBool("HorizontalWalk", false);
    }

    public AudioClip GetSfxFootStep(LevelUtil.LevelType levelType)
    {
        AudioClip[] footSteps = sfxFootStepMap[levelType];
        int index = UnityEngine.Random.Range(0, footSteps.Length);
        return footSteps[index];
    }
}
