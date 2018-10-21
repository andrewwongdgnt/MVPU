using UnityEngine;
using System.Collections;

public interface IWalker : IEntity
{
    

    void StartWalkAnimation();
    void StopWalkAnimation();
    AudioClip GetResolvedSfxFootStep(LevelUtil.LevelType levelType);

    LevelTypeAudioPair[] sfxFootSteps { get; }
}
