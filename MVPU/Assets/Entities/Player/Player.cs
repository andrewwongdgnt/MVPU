using UnityEngine;
using System.Collections;


public class Player : Entity
{

    // Use this for initialization
    void Start()
    {
        Debug.Log("Player Created:" + " x=" + x + " y=" + y);
    }

    public void Do_MoveUp(float vDisplacement, float hDisplacement)
    {
        TryToMoveUp(vDisplacement);
        _gameModel.AnimateGameObject(this, Direction.UP,0);
    }

    public void Do_MoveLeft(float vDisplacement, float hDisplacement)
    {
        TryToMoveLeft(hDisplacement);
        _gameModel.AnimateGameObject(this, Direction.LEFT, 0);
    }

    public void Do_MoveDown(float vDisplacement, float hDisplacement)
    {
        TryToMoveDown(vDisplacement);
        _gameModel.AnimateGameObject(this, Direction.DOWN, 0);
    }

    public void Do_MoveRight(float vDisplacement, float hDisplacement)
    {
        TryToMoveRight(hDisplacement);
        _gameModel.AnimateGameObject(this, Direction.RIGHT, 0);
    }
}
