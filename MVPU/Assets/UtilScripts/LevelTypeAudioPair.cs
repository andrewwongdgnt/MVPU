using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct LevelTypeAudioPair
{
    public LevelUtil.LevelType levelType;
    public AudioClip[] audioClips;
}
