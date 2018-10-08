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

    private Animator animator;
    private Dictionary<LevelUtil.LevelType, AudioClip[]> sfxFootStepMap;
    public WalkerService(Animator animator, FootStepPair[] sfxFootSteps)
    {
        this.animator = animator;
        sfxFootStepMap = new Dictionary<LevelUtil.LevelType, AudioClip[]>();
        Array.ForEach(sfxFootSteps, e => sfxFootStepMap.Add(e.levelType, e.footSteps));
    }

    public void StartWalkAnimation()
    {
        animator.SetBool("HorizontalWalk", true);
    }

    public void StopWalkAnimation()
    {
        animator.SetBool("HorizontalWalk", false);
    }

    public AudioClip GetSfxFootStep(LevelUtil.LevelType levelType)
    {
        AudioClip[] footsteps = sfxFootStepMap[levelType];
        int index = UnityEngine.Random.Range(0, footsteps.Length);
        return footsteps[index];
    }
}
