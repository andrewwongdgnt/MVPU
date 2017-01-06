using UnityEngine;
using System.Collections;


public class Player : Entity, IWalker
{

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
        if (direction == Entity.Direction.UP)
            unblocked = TryToMoveUp();
        if (direction == Entity.Direction.LEFT)
            unblocked = TryToMoveLeft();
        if (direction == Entity.Direction.DOWN)
            unblocked = TryToMoveDown();
        if (direction == Entity.Direction.RIGHT)
            unblocked = TryToMoveRight();

        if (!unblocked)
            return false;

        _gameModel.CheckForEndGame(this, 0);
        _gameModel.AnimateGameObject(this, direction, 0);

        return true;
        

    }

    public void StartWalkAnimation()
    {
        animator.SetBool("HorizontalWalk", true);
    }
    public void StopWalkAnimation()
    {
        animator.SetBool("HorizontalWalk", false);
    }

    public Entity entity
    {
        get
        {
            return this;
        }
    }
}
