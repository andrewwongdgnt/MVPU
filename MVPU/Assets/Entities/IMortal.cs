using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMortal : IEntity
{

    void StartDieAnimation(MortalService.DeathAnimation deathAnimation);
    void StopDieAnimation();

    AudioClip GetResolvedSfxThud(LevelUtil.LevelType levelType);
    LevelTypeAudioPair[] sfxThuds { get; }
    AudioClip sfxHitClip
    {
        get;
    }
}
