using UnityEngine;
using System.Collections;

public abstract class Entity
{
    public enum Direction
    {
        UP, LEFT, DOWN, RIGHT
    }

    private int _x;
    public int x
    {
        get
        {
            return _x;
        }
    }

    private int _y;
    public int y
    {
        get
        {
            return _y;
        }
    }

    private Cell[,] _grid;
    public Cell[,] grid
    {
        get
        {
            return _grid;
        }
    }

    protected GameModel _gameModel;

    public Entity(int x, int y, Cell[,] grid, GameModel gameModel)
    {
        _x = x;
        _y = y;
        _grid = grid;
        _gameModel = gameModel;
    }

    protected bool tryToMoveUp(GameObject gameObject, float displacement)
    {
        do_look(gameObject, Direction.UP);
        if (y > 0 && !grid[y, x].topBlocked)
        {
            _y--;
            _gameModel.moveGameObject(gameObject, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + displacement, gameObject.transform.position.z));
            return true;
        }
        return false;
    }

    public virtual void do_moveUp(GameObject gameObject, float vDisplacement, float hDisplacement)
    {
        tryToMoveUp(gameObject, vDisplacement);

    }

    protected bool tryToMoveLeft(GameObject gameObject, float displacement)
    {
        do_look(gameObject, Direction.LEFT);
        if (x > 0 && !grid[y, x].leftBlocked)
        {
            _x--;
            _gameModel.moveGameObject(gameObject, new Vector3(gameObject.transform.position.x - displacement, gameObject.transform.position.y, gameObject.transform.position.z));
            return true;
        }
        return false;
    }

    public virtual void do_moveLeft(GameObject gameObject, float vDisplacement, float hDisplacement)
    {
        tryToMoveLeft(gameObject, hDisplacement);
    }

    protected bool tryToMoveDown(GameObject gameObject, float displacement)
    {
        do_look(gameObject, Direction.DOWN);
        if (y < grid.GetLength(0) - 1 && !grid[y, x].bottomBlocked)
        {
            _y++;
            _gameModel.moveGameObject(gameObject, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - displacement, gameObject.transform.position.z));
            return true;
        }
        return false;
    }

    public virtual void do_moveDown(GameObject gameObject, float vDisplacement, float hDisplacement)
    {
        tryToMoveDown(gameObject, vDisplacement);
    }

    protected bool tryToMoveRight(GameObject gameObject, float displacement)
    {
        do_look(gameObject, Direction.RIGHT);
        if (x < grid.GetLength(1) - 1 && !grid[y, x].rightBlocked)
        {
            _x++;
            _gameModel.moveGameObject(gameObject, new Vector3(gameObject.transform.position.x + displacement, gameObject.transform.position.y, gameObject.transform.position.z));
            return true;
        }
        return false;
    }

    public virtual void do_moveRight(GameObject gameObject, float vDisplacement, float hDisplacement)
    {
        tryToMoveRight(gameObject, hDisplacement);
    }

    public void do_look(GameObject gameObject, Direction direction)
    {
        _gameModel.look(gameObject, direction);
    }
}
