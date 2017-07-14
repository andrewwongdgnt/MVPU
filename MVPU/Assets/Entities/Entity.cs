using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Entity : MonoBehaviour, IUndoable, IEntityObtainer
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
            Direction directionOfBlockingForCurrentWall = _gameModel.CheckForUnretractedWall(x, y);
            Direction directionOfBlockingForAboveWall = _gameModel.CheckForUnretractedWall(x, y - 1);
            if (directionOfBlockingForCurrentWall != Direction.UP && directionOfBlockingForAboveWall != Direction.DOWN)
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
            Direction directionOfBlockingForCurrentWall = _gameModel.CheckForUnretractedWall(x, y);
            Direction directionOfBlockingForLeftWall = _gameModel.CheckForUnretractedWall(x - 1, y);
            if (directionOfBlockingForCurrentWall != Direction.LEFT && directionOfBlockingForLeftWall != Direction.RIGHT)
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
            Direction directionOfBlockingForCurrentWall = _gameModel.CheckForUnretractedWall(x, y);
            Direction directionOfBlockingForBelowWall = _gameModel.CheckForUnretractedWall(x, y + 1);
            if (directionOfBlockingForCurrentWall != Direction.DOWN && directionOfBlockingForBelowWall != Direction.UP)
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
            Direction directionOfBlockingForCurrentWall = _gameModel.CheckForUnretractedWall(x, y);
            Direction directionOfBlockingForRightWall = _gameModel.CheckForUnretractedWall(x + 1, y);
            if (directionOfBlockingForCurrentWall != Direction.RIGHT && directionOfBlockingForRightWall != Direction.LEFT)
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

}
