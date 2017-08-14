using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;

public class Enemy : Entity, IWalker, IAttacker
{
    public bool verticalOrientation;
    public int stepsPerMove;
    public bool dozer;
    public AudioClip sfxHitClip;

    public bool inactive
    {
        get; set;
    }
    private bool blocked;

    protected override void BuildAdditionalStateDict(Dictionary<string, object> dict)
    {
        dict.Add("verticalOrientation", verticalOrientation);
        dict.Add("stepsPerMove", stepsPerMove);
        dict.Add("dozer", dozer);
        dict.Add("inactive", inactive);
    }
    protected override void RestoreAdditionalState(Dictionary<string, object> dict)
    {
        foreach (KeyValuePair<string, object> entry in dict)
        {
            if (entry.Key == "verticalOrientation" && entry.Value is bool)
                verticalOrientation = (bool)entry.Value;
            else if (entry.Key == "stepsPerMove" && entry.Value is int)
                stepsPerMove = (int)entry.Value;
            else if (entry.Key == "dozer" && entry.Value is bool)
                dozer = (bool)entry.Value;
            else if (entry.Key == "inactive" && entry.Value is bool)
                inactive = (bool)entry.Value;
        }
    }

    // Use this for initialization
    void Start()
    {

        Debug.Log(this + " Created:" + " x=" + x + " y=" + y + " verticalOrientation=" + verticalOrientation + " stepsPerMove=" + stepsPerMove + " dozer=" + dozer);
    }


    public void Do_React()
    {
        int playerX = _gameModel.playerX;
        int playerY = _gameModel.playerY;


        for (int i = 0; i < stepsPerMove; i++)
        {
            Direction direction = Direction.NONE;
            if (!inactive)
            {
                blocked = false;

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

                _gameModel.CheckForKey(this, direction, i);
                _gameModel.CheckForEndGame(this, i);
                if (dozer)
                    _gameModel.CheckIfOtherEnemiesGotDozed(this, i);
                if (blocked)
                    _gameModel.SetBlocked(this, i);
                _gameModel.CheckIfBombed(this, i);

            }

            _gameModel.AnimateGameObject(this, direction, i);
            facingDirection = direction;
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
        blocked = true;
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
        blocked = true;
        return defaultDirection;
    }

    protected bool TryToMoveUpForEnemy()
    {
        if (!_gameModel.IsGoalInTheWay(go => go.x == x && go.y == y - 1)
            && (dozer || !_gameModel.IsAnEnemyInTheWay(en => en.x == x && en.y == y - 1 && !en.inactive)))
        {
            return TryToMoveUp();
        }
        return false;
    }

    protected bool TryToMoveLeftForEnemy()
    {
        if (!_gameModel.IsGoalInTheWay(go => go.x == x - 1 && go.y == y)
            && (dozer || !_gameModel.IsAnEnemyInTheWay(en => en.x == x - 1 && en.y == y && !en.inactive)))
        {
            return TryToMoveLeft();
        }
        return false;
    }

    protected bool TryToMoveDownForEnemy()
    {
        if (!_gameModel.IsGoalInTheWay(go => go.x == x && go.y == y + 1)
            && (dozer || !_gameModel.IsAnEnemyInTheWay(en => en.x == x && en.y == y + 1 && !en.inactive)))
        {
            return TryToMoveDown();
        }
        return false;
    }

    protected bool TryToMoveRightForEnemy()
    {
        if (!_gameModel.IsGoalInTheWay(go => go.x == x + 1 && go.y == y)
            && (dozer || !_gameModel.IsAnEnemyInTheWay(en => en.x == x + 1 && en.y == y && !en.inactive)))
        {
            return TryToMoveRight();
        }
        return false;
    }


    public void StartWalkAnimation()
    {
        animator.SetBool("HorizontalWalk", true);
    }
    public void StopWalkAnimation()
    {
        animator.SetBool("HorizontalWalk", false);
    }


    public void StartDozedAnimation()
    {
        animator.SetBool("Dozed", true);
    }
    public void StopDozedAnimation()
    {
        animator.SetBool("Dozed", false);
    }
    public void StartAttackAnimation()
    {
        animator.SetBool("Attack", true);
    }
    public void StopAttackAnimation()
    {
        animator.SetBool("Attack", false);
    }
}
