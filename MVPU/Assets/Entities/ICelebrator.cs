using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICelebrator : IEntity
{

    void StartWinAnimation();
    void StopWinAnimation();
    AudioClip GetResolvedSfxCelebrateStep(LevelUtil.LevelType levelType);

    LevelTypeAudioPair[] sfxCelebrateSteps { get; }
}
