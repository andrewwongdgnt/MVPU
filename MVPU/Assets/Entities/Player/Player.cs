using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : Entity, IWalker, IMortal, ICelebrator
{
    public LevelTypeAudioPair[] sfxFootSteps;
    public AudioClip sfxSlipThudClip;
    public LevelTypeAudioPair[] sfxCelebrateSteps;

    private WalkerService _walkerService;
    private WalkerService walkerService
    {
        get
        {
            if (_walkerService == null)
            {
                _walkerService = new WalkerService(this);
            }
            return _walkerService;
        }
    }
    private MortalService _mortalService;
    private MortalService mortalService
    {
        get
        {
            if (_mortalService == null)
            {
                _mortalService = new MortalService(this);
            }
            return _mortalService;
        }
    }

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
    void Start()
    {
        Debug.Log(this+" Created:" + " x=" + x + " y=" + y);
    }


    public bool Do_MoveUp()
    {
        return TryToMove(Direction.UP);
    }

    public bool Do_MoveLeft()
    {
        return TryToMove(Direction.LEFT);
    }

    public bool Do_MoveDown()
    {
        return TryToMove(Direction.DOWN);
    }

    public bool Do_MoveRight()
    {
        return TryToMove(Direction.RIGHT);
    }

    public bool Do_Nothing()
    {
        return TryToMove(Direction.NONE);
    }

    private bool TryToMove(Direction direction)
    {
        bool unblocked = true;
        if (direction == Direction.UP)
            unblocked = TryToMoveUp();
        if (direction == Direction.LEFT)
            unblocked = TryToMoveLeft();
        if (direction == Direction.DOWN)
            unblocked = TryToMoveDown();
        if (direction == Direction.RIGHT)
            unblocked = TryToMoveRight();

        if (!unblocked)
            return false;

        _gameModel.CheckForKey(this, direction, 0);
        _gameModel.CheckForEndGame(this, 0);
        _gameModel.AnimateGameObject(this, direction, 0);
        if (direction!=Direction.NONE)
            facingDirection = direction;
        return true;
            

    }

    //---------------
    // IWalker Impl
    //---------------

    public void StartWalkAnimation()
    {
        walkerService.StartWalkAnimation();
    }
    public void StopWalkAnimation()
    {
        walkerService.StopWalkAnimation();
    }
    LevelTypeAudioPair[] IWalker.sfxFootSteps
    {
        get
        {
            return sfxFootSteps;
        }
    }
    public AudioClip GetResolvedSfxFootStep(LevelUtil.LevelType levelType)
    {
        return walkerService.GetSfxFootStep(levelType);
    }

    //---------------
    // IMortal Impl
    //---------------

    public void StartDieAnimation(MortalService.DeathAnimation deathAnimation)
    {
        StartDieAnimation(deathAnimation, false);
    }
    public void StartDieAnimation(MortalService.DeathAnimation deathAnimation, bool showOnlyFirstFrame)
    {
        mortalService.StartDieAnimation(deathAnimation);
        animator.speed = showOnlyFirstFrame ? 0 : 1;
    }

    public void StopDieAnimation()
    {
        mortalService.StopDieAnimation();
    }

    AudioClip IMortal.sfxSlipThudClip
    {
        get
        {
            return sfxSlipThudClip;
        }
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
