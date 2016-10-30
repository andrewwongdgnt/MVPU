using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour
{
    public enum Direction
    {
        UP, LEFT, DOWN, RIGHT, NEUTRAL
    }

    public int x;
    public int y;


    protected GameModel _gameModel;
    public GameModel gameModel
    {
        set
        {
            _gameModel = value;
        }
    }

    protected bool TryToMoveUp(float displacement)
    {

        if (y > 0 && !_gameModel.grid[y, x].topBlocked)
        {
            y--;
            return true;
        }
        return false;
    }


    protected bool TryToMoveLeft(float displacement)
    {
        if (x > 0 && !_gameModel.grid[y, x].leftBlocked)
        {
            x--;
            return true;
        }
        return false;
    }

    protected bool TryToMoveDown(float displacement)
    {
        if (y < _gameModel.grid.GetLength(0) - 1 && !_gameModel.grid[y, x].bottomBlocked)
        {
            y++;
            return true;
        }
        return false;
    }

    protected bool TryToMoveRight(float displacement)
    {
        if (x < _gameModel.grid.GetLength(1) - 1 && !_gameModel.grid[y, x].rightBlocked)
        {
            x++;
            return true;
        }
        return false;
    }

}
