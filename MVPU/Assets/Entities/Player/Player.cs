using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : Entity, IWalker
{
    public WalkerService.FootStepPair[] sfxFootSteps;

    private WalkerService walkerService;

    // Use this for initialization
    void Start()
    {
        Debug.Log(this+" Created:" + " x=" + x + " y=" + y);
        walkerService = new WalkerService(animator, sfxFootSteps);
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


    public void StartWalkAnimation()
    {
        walkerService.StartWalkAnimation();
    }
    public void StopWalkAnimation()
    {
        walkerService.StopWalkAnimation();
    }

    public AudioClip GetSfxFootStep(LevelUtil.LevelType levelType)
    {
        return walkerService.GetSfxFootStep(levelType);
    }

    public void StartDieAnimation(string name, bool showOnlyFirstFrame = false)
    {
        animator.SetBool(name, true);
        animator.speed = showOnlyFirstFrame ? 0 : 1;
    }


    public void StopDieAnimation()
    {
        animator.SetBool("Dead", false);
        animator.SetBool("Slip", false);

    }

    public void StartWinAnimation()
    {
        animator.SetBool("Win", true);
    }


    public void StopWinAnimation()
    {
        animator.SetBool("Win", false);

    }
}
