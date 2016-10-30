using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class Enemy : Entity
{
    public bool verticalOrientation;
    public int stepsPerMove;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Enemy Created:"+" x="+x+" y="+y+" verticalOrientation="+verticalOrientation+" stepsPerMove="+stepsPerMove);
    }


    public void Do_React(float vDisplacement, float hDisplacement)
    {
        int playerX = _gameModel.playerX;
        int playerY = _gameModel.playerY;

        
        for (int i = 0; i < stepsPerMove; i++)
        {
            Direction direction = Direction.NEUTRAL;
            if (verticalOrientation)
            {
                if (playerY != y)
                {
                    //Player is above of 
                    if (playerY < y)
                    {
                        direction = Direction.UP;
                        if (!TryToMoveUpForEnemy(gameObject, vDisplacement))
                        {
                            direction = TryToMoveLeftOrRightForEnemy(playerX, gameObject, hDisplacement);
                        }

                    }
                    //Player is below of
                    else if (playerY > y)
                    {
                        direction = Direction.DOWN;
                        if (!TryToMoveDownForEnemy(gameObject, vDisplacement))
                        {
                            direction = TryToMoveLeftOrRightForEnemy(playerX, gameObject, hDisplacement);
                        }
                    }
                }
                else
                {
                    direction = TryToMoveLeftOrRightForEnemy(playerX, gameObject, hDisplacement);
                }
            }
            else
            {

                if (playerX != x)
                {
                    //Player is left of 
                    if (playerX < x)
                    {
                        direction = Direction.LEFT;
                        if (!TryToMoveLeftForEnemy(gameObject, hDisplacement))
                        {
                            direction = TryToMoveUpOrDownForEnemy(playerY, gameObject, vDisplacement);
                        }

                    }
                    //Player is right of
                    else if (playerX > x)
                    {
                        direction = Direction.RIGHT;
                        if (!TryToMoveRightForEnemy(gameObject, hDisplacement))
                        {
                            direction = TryToMoveUpOrDownForEnemy(playerY, gameObject, vDisplacement);
                        }
                    }
                }
                else
                {
                    direction = TryToMoveUpOrDownForEnemy(playerY, gameObject, vDisplacement);
                }
            }
            _gameModel.AnimateGameObject(this, direction,i);

            _gameModel.CheckForEndGame(this);
        }
    }

    private Direction TryToMoveLeftOrRightForEnemy(int playerX, GameObject gameObject, float hDisplacement)
    {

        //Player is left of 
        if (playerX < x)
        {
            TryToMoveLeftForEnemy(gameObject, hDisplacement);
            return Direction.LEFT;
        }
        //Player is right of
        else if (playerX > x)
        {
            TryToMoveRightForEnemy(gameObject, hDisplacement);
            return Direction.RIGHT;
        }
        return Direction.NEUTRAL;
    }

    private Direction TryToMoveUpOrDownForEnemy(int playerY, GameObject gameObject, float vDisplacement)
    {

        //Player is above of 
        if (playerY < y)
        {
            TryToMoveUpForEnemy(gameObject, vDisplacement);
            return Direction.UP;
        }
        //Player is below of
        else if (playerY > y)
        {
            TryToMoveDownForEnemy(gameObject, vDisplacement);
            return Direction.DOWN;
        }
        return Direction.NEUTRAL;
    }

    protected bool TryToMoveUpForEnemy(GameObject gameObject, float displacement)
    {
        if (_gameModel.IsAnEnemyInTheWay(en => en.x == x && en.y == y - 1))
        {
            return TryToMoveUp(displacement);
        }
        return false;
    }

    protected bool TryToMoveLeftForEnemy(GameObject gameObject, float displacement)
    {
        if (_gameModel.IsAnEnemyInTheWay(en => en.x == x - 1 && en.y == y))
        {
            return TryToMoveLeft(displacement);
        }
        return false;
    }

    protected bool TryToMoveDownForEnemy(GameObject gameObject, float displacement)
    {
        if (_gameModel.IsAnEnemyInTheWay(en => en.x == x && en.y == y + 1))
        {
            return TryToMoveDown(displacement);
        }
        return false;
    }

    protected bool TryToMoveRightForEnemy(GameObject gameObject, float displacement)
    {
        if (_gameModel.IsAnEnemyInTheWay(en => en.x == x + 1 && en.y == y))
        {
            return TryToMoveRight(displacement);
        }
        return false;
    }


}
