using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class Enemy : Entity
{
    public bool verticalOrientation;
    public int stepsPerMove;
    public bool dozer;

    public bool inactive
    {
        get; set;
    }
    private bool blocked;


    // Use this for initialization
    void Start()
    {
        
        Debug.Log(this+" Created:"+" x="+x+" y="+y+" verticalOrientation="+verticalOrientation+" stepsPerMove="+stepsPerMove+" dozer="+dozer);
    }


    public void Do_React()
    {
        if (!inactive)
        {
            int playerX = _gameModel.playerX;
            int playerY = _gameModel.playerY;


            for (int i = 0; i < stepsPerMove; i++)
            {
                blocked = false;
                Direction direction = Direction.NONE;
                if (verticalOrientation)
                {
                    if (playerY != y)
                    {
                        //Player is above of 
                        if (playerY < y)
                        {
                            direction = Direction.UP;
                            if (!TryToMoveUpForEnemy())
                            {
                                direction = TryToMoveLeftOrRightForEnemy(playerX, direction);
                            }

                        }
                        //Player is below of
                        else if (playerY > y)
                        {
                            direction = Direction.DOWN;
                            if (!TryToMoveDownForEnemy())
                            {
                                direction = TryToMoveLeftOrRightForEnemy(playerX, direction);
                            }
                        }
                    }
                    else
                    {
                        direction = TryToMoveLeftOrRightForEnemy(playerX, direction);
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
                            if (!TryToMoveLeftForEnemy())
                            {
                                direction = TryToMoveUpOrDownForEnemy(playerY, direction);
                            }

                        }
                        //Player is right of
                        else if (playerX > x)
                        {
                            direction = Direction.RIGHT;
                            if (!TryToMoveRightForEnemy())
                            {
                                direction = TryToMoveUpOrDownForEnemy(playerY, direction);
                            }
                        }
                    }
                    else
                    {
                        direction = TryToMoveUpOrDownForEnemy(playerY, direction);
                    }
                }
                _gameModel.CheckForEndGame(this, i);
                if (dozer)
                    _gameModel.CheckIfOtherEnemiesGotDozed(this, i);
                if (blocked)
                    _gameModel.SetBlocked(this, i);

                _gameModel.AnimateGameObject(this, direction, i);

            }
        }
        else
        {
            _gameModel.AnimateGameObject(this, Direction.NONE, 0);
        }
    }

    private Direction TryToMoveLeftOrRightForEnemy(int playerX, Direction defaultDirection)
    {

        //Player is left of 
        if (playerX < x)
        {
            blocked = !TryToMoveLeftForEnemy();
            return Direction.LEFT;
        }
        //Player is right of
        else if (playerX > x)
        {
            blocked = !TryToMoveRightForEnemy();
            return Direction.RIGHT;
        }
        
        return defaultDirection;
    }

    private Direction TryToMoveUpOrDownForEnemy(int playerY, Direction defaultDirection)
    {

        //Player is above of 
        if (playerY < y)
        {
            blocked = !TryToMoveUpForEnemy();
            return Direction.UP;
        }
        //Player is below of
        else if (playerY > y)
        {
            blocked = !TryToMoveDownForEnemy();
            return Direction.DOWN;
        }

        return defaultDirection;
    }

    protected bool TryToMoveUpForEnemy()
    {
        if (dozer || _gameModel.IsAnEnemyInTheWay(en => en.x == x && en.y == y - 1))
        {
            return TryToMoveUp();
        }
        return false;
    }

    protected bool TryToMoveLeftForEnemy()
    {
        if (dozer || _gameModel.IsAnEnemyInTheWay(en => en.x == x - 1 && en.y == y))
        {
            return TryToMoveLeft();
        }
        return false;
    }

    protected bool TryToMoveDownForEnemy()
    {
        if (dozer || _gameModel.IsAnEnemyInTheWay(en => en.x == x && en.y == y + 1))
        {
            return TryToMoveDown();
        }
        return false;
    }

    protected bool TryToMoveRightForEnemy()
    {
        if (dozer || _gameModel.IsAnEnemyInTheWay(en => en.x == x + 1 && en.y == y))
        {
            return TryToMoveRight();
        }
        return false;
    }


}
