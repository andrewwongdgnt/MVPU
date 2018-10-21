using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebratorService  {

    private ICelebrator celebrator;
    private Dictionary<LevelUtil.LevelType, AudioClip[]> sfxCelebrateStepMap;
    public CelebratorService(ICelebrator celebrator)
    {
        this.celebrator = celebrator;
        sfxCelebrateStepMap = new Dictionary<LevelUtil.LevelType, AudioClip[]>();
        Array.ForEach(celebrator.sfxCelebrateSteps, e => sfxCelebrateStepMap.Add(e.levelType, e.audioClips));
    }

    public void StartWinAnimation()
    {
        celebrator.animator.SetBool("Win", true);
    }


    public void StopWinAnimation()
    {
        celebrator.animator.SetBool("Win", false);

    }



    public AudioClip GetSfxCelebrateStep(LevelUtil.LevelType levelType)
    {
        AudioClip[] celebrateSteps = sfxCelebrateStepMap[levelType];
        int index = UnityEngine.Random.Range(0, celebrateSteps.Length);
        return celebrateSteps[index];
    }
}
