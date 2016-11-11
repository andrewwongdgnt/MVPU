using UnityEngine;
using System.Collections;


public class Player : Entity
{

    // Use this for initialization
    void Start()
    {
        Debug.Log(this+" Created:" + " x=" + x + " y=" + y);
    }


    public void Do_MoveUp()
    {
        TryToMove(Direction.UP);
    }

    public void Do_MoveLeft()
    {
        TryToMove(Direction.LEFT);
    }

    public void Do_MoveDown()
    {
        TryToMove(Direction.DOWN);
    }

    public void Do_MoveRight()
    {
        TryToMove(Direction.RIGHT);
    }

    public void Do_Nothing()
    {
        TryToMove(Direction.NONE);
    }

    private void TryToMove(Direction direction)
    {
        if (direction == Entity.Direction.UP)
            TryToMoveUp();
        if (direction == Entity.Direction.LEFT)
            TryToMoveLeft();
        if (direction == Entity.Direction.DOWN)
            TryToMoveDown();
        if (direction == Entity.Direction.RIGHT)
            TryToMoveRight();

        _gameModel.CheckForEndGame(this, 0);
        _gameModel.AnimateGameObject(this, direction, 0);

    }
}
