using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public abstract class Enemy : Entity
{
    public Enemy(int x, int y, Cell[,] grid, GameModel gameModel) : base(x, y, grid, gameModel)
    {
    }

    protected bool tryToMoveUpForEnemy(GameObject gameObject, float displacement)
    {
        do_look(gameObject, Direction.UP);
        if (!Array.Exists(_gameModel.enemyEntityArr, en => en.x == x && en.y == y - 1))
        {
            return tryToMoveUp(gameObject, displacement);
        }
        return false;
    }

    protected bool tryToMoveLeftForEnemy(GameObject gameObject, float displacement)
    {
        do_look(gameObject, Direction.LEFT);
        if (!Array.Exists(_gameModel.enemyEntityArr, en => en.x == x - 1 && en.y == y))
        {
            return tryToMoveLeft(gameObject, displacement);
        }
        return false;
    }

    protected bool tryToMoveDownForEnemy(GameObject gameObject, float displacement)
    {
        do_look(gameObject, Direction.DOWN);
        if (!Array.Exists(_gameModel.enemyEntityArr, en => en.x == x && en.y == y + 1))
        {
            return tryToMoveDown(gameObject, displacement);
        }
        return false;
    }

    protected bool tryToMoveRightForEnemy(GameObject gameObject, float displacement)
    {
        do_look(gameObject, Direction.RIGHT);
        if (!Array.Exists(_gameModel.enemyEntityArr, en => en.x == x + 1 && en.y == y))
        {
            return tryToMoveRight(gameObject, displacement);
        }
        return false;
    }

    public abstract void do_react(GameObject gameObject, float vDisplacement, float hDisplacement);
}
