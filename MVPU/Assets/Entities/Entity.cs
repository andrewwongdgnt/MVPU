using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour
{
    public enum Direction
    {
        NONE, UP, LEFT, DOWN, RIGHT, 
    }

    public int x
    {
        get;set;
    }
    public int y
    {
        get; set;
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

    protected bool TryToMoveUp()
    {

        if (y > 0 && !_gameModel.grid[y, x].topBlocked)
        {
            y--;
            return true;
        }
        return false;
    }


    protected bool TryToMoveLeft()
    {
        if (x > 0 && !_gameModel.grid[y, x].leftBlocked)
        {
            x--;
            return true;
        }
        return false;
    }

    protected bool TryToMoveDown()
    {
        if (y < _gameModel.grid.GetLength(0) - 1 && !_gameModel.grid[y, x].bottomBlocked)
        {
            y++;
            return true;
        }
        return false;
    }

    protected bool TryToMoveRight()
    {
        if (x < _gameModel.grid.GetLength(1) - 1 && !_gameModel.grid[y, x].rightBlocked)
        {
            x++;
            return true;
        }
        return false;
    }


}
