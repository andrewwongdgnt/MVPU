using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Entity : MonoBehaviour, IUndoable, IEntity
{

    
    public enum Direction
    {
        NONE, UP, LEFT, DOWN, RIGHT,
    }

    public int x
    {
        get; set;
    }
    public int y
    {
        get; set;
    }
    public Direction facingDirection
    {
        get;set;
    }
    public AudioSource audioSource;

    public Animator animator;

    protected GameModel _gameModel;
    public GameModel gameModel
    {
        set
        {
            _gameModel = value;
        }
    }

    public Dictionary<string, object> BuildStateDict()
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict.Add("x", x);
        dict.Add("y", y);
        dict.Add("facingDirection", facingDirection);
        BuildAdditionalStateDict(dict);
        return dict;
    }

    protected virtual void BuildAdditionalStateDict(Dictionary<string, object> dict)
    {

    }

    public void RestoreState(Dictionary<string, object> dict)
    {
        foreach (KeyValuePair<string, object> entry in dict)
        {
            if (entry.Key == "x" && entry.Value is int)
                x = (int)entry.Value;
            else if (entry.Key == "y" && entry.Value is int)
                y = (int)entry.Value;
            else if (entry.Key == "facingDirection" && entry.Value is Direction)
                facingDirection = (Direction)entry.Value;
        }
        RestoreAdditionalState(dict);
    }

    protected virtual void RestoreAdditionalState(Dictionary<string, object> dict)
    {

    }

    protected bool TryToMoveUp()
    {

        if (y > 0 && !_gameModel.grid[y, x].topBlocked)
        {
            bool directionOfBlockingForCurrentWallIsUp = _gameModel.CheckIfUnretractedWallIsBlocker(x, y, Direction.UP);
            bool directionOfBlockingForAboveWallIsDown = _gameModel.CheckIfUnretractedWallIsBlocker(x, y - 1, Direction.DOWN);
            if (!directionOfBlockingForCurrentWallIsUp && !directionOfBlockingForAboveWallIsDown)
            {
                y--;
                return true;
            }
        }
        
        return false;
    }


    protected bool TryToMoveLeft()
    {
        if (x > 0 && !_gameModel.grid[y, x].leftBlocked)
        {
            bool directionOfBlockingForCurrentWallIsLeft = _gameModel.CheckIfUnretractedWallIsBlocker(x, y, Direction.LEFT);
            bool directionOfBlockingForLeftWallIsRight = _gameModel.CheckIfUnretractedWallIsBlocker(x - 1, y, Direction.RIGHT);
            if (!directionOfBlockingForCurrentWallIsLeft && !directionOfBlockingForLeftWallIsRight)
            {
                x--;
                return true;
            }
        }
        return false;
    }

    protected bool TryToMoveDown()
    {
        if (y < _gameModel.grid.GetLength(0) - 1 && !_gameModel.grid[y, x].bottomBlocked)
        {
            bool directionOfBlockingForCurrentWallIsDown = _gameModel.CheckIfUnretractedWallIsBlocker(x, y, Direction.DOWN);
            bool directionOfBlockingForBelowWallIsUp = _gameModel.CheckIfUnretractedWallIsBlocker(x, y + 1, Direction.UP);
            if (!directionOfBlockingForCurrentWallIsDown && !directionOfBlockingForBelowWallIsUp )
            {
                y++;
                return true;
            }
        }
        return false;
    }

    protected bool TryToMoveRight()
    {
        if (x < _gameModel.grid.GetLength(1) - 1 && !_gameModel.grid[y, x].rightBlocked)
        {
            bool directionOfBlockingForCurrentWallIsRight = _gameModel.CheckIfUnretractedWallIsBlocker(x, y, Direction.RIGHT);
            bool directionOfBlockingForRightWallIsLeft = _gameModel.CheckIfUnretractedWallIsBlocker(x + 1, y, Direction.LEFT);
            if (!directionOfBlockingForCurrentWallIsRight && !directionOfBlockingForRightWallIsLeft)
            {
                x++;
                return true;
            }
        }
        return false;
    }


    public Entity entity
    {
        get
        {
            return this;
        }
    }

    AudioSource IAudioSourceObtainer.audioSource
    {
        get
        {
            return audioSource;
        }
    }

    Animator IAnimatorObtainer.animator
    {
        get
        {
            return animator;
        }
    }
}
