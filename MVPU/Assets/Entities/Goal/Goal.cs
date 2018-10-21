using UnityEngine;
using System.Collections;

public class Goal : Entity, ICelebrator
{

    public LevelTypeAudioPair[] sfxCelebrateSteps;

    private CelebratorService _celebratorService;
    private CelebratorService celebratorService
    {
        get
        {
            if (_celebratorService == null)
            {
                _celebratorService = new CelebratorService(this);
            }
            return _celebratorService;
        }
    }

    // Use this for initialization
    void Start () {
        Debug.Log(this + " Created:" + " x=" + x + " y=" + y);
    }

    //---------------
    // ICelebrator Impl
    //---------------

    public void StartWinAnimation()
    {
        celebratorService.StartWinAnimation();
    }


    public void StopWinAnimation()
    {
        celebratorService.StopWinAnimation();

    }
    public AudioClip GetResolvedSfxCelebrateStep(LevelUtil.LevelType levelType)
    {
        return celebratorService.GetSfxCelebrateStep(levelType);
    }
    LevelTypeAudioPair[] ICelebrator.sfxCelebrateSteps
    {
        get
        {
            return sfxCelebrateSteps;
        }
    }

}
