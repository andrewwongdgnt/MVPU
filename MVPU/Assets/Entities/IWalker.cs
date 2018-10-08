using UnityEngine;
using System.Collections;

public interface IWalker : IEntity
{
    

    void StartWalkAnimation();
    void StopWalkAnimation();
    AudioClip GetSfxFootStep(LevelUtil.LevelType levelType);
}
