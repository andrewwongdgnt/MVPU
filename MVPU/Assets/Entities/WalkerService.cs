using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerService
{

    [Serializable]
    public struct FootStepPair
    {
        public LevelUtil.LevelType levelType;
        public AudioClip[] footSteps;
    }

    private IWalker walker;
    private Dictionary<LevelUtil.LevelType, AudioClip[]> sfxFootStepMap;
    public WalkerService(IWalker walker)
    {
        this.walker = walker;
        sfxFootStepMap = new Dictionary<LevelUtil.LevelType, AudioClip[]>();
        Array.ForEach(walker.sfxFootSteps, e => sfxFootStepMap.Add(e.levelType, e.footSteps));
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
        AudioClip[] footsteps = sfxFootStepMap[levelType];
        int index = UnityEngine.Random.Range(0, footsteps.Length);
        return footsteps[index];
    }
}
