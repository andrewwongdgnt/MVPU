using UnityEngine;
using System.Collections;

public class VEnemy : Entity
{
    public VEnemy(int x, int y, Cell[,] grid, GameModel gameModel) : base(x, y, grid, gameModel)
    {
    }

    public override void do_moveUp(GameObject gameObject, float vDisplacement, float hDisplacement)
    {
        move(Direction.UP, gameObject, vDisplacement, hDisplacement);
    }

    public override void do_moveLeft(GameObject gameObject, float vDisplacement, float hDisplacement)
    {
        move(Direction.LEFT, gameObject, vDisplacement, hDisplacement);
    }

    public override void do_moveDown(GameObject gameObject, float vDisplacement, float hDisplacement)
    {
        move(Direction.DOWN, gameObject, vDisplacement, hDisplacement);
    }

    public override void do_moveRight(GameObject gameObject, float vDisplacement, float hDisplacement)
    {
        move(Direction.RIGHT, gameObject, vDisplacement, hDisplacement);
    }

    private void move(Direction directionOfPlayer, GameObject gameObject, float vDisplacement, float hDisplacement)
    {
        Player player = _gameModel.playerEntity;
        int playerX = player.x;
        int playerY = player.y;

        if (directionOfPlayer == Direction.LEFT || directionOfPlayer == Direction.RIGHT)
        {
            if (playerY != y)
            {
                moveUpOrDown(playerY, gameObject, vDisplacement);
            }
            else
            {
                moveLeftOrRight(playerX, gameObject, hDisplacement);
            }
        }
        else
        {
            if (playerY == y)
            {
                moveLeftOrRight(playerX, gameObject, hDisplacement);
            }
            else
            {
                moveUpOrDown(playerY, gameObject, vDisplacement);
            }
        }

        _gameModel.checkForEndGame(this);
    }

    private void moveLeftOrRight(int playerX, GameObject gameObject, float displacement)
    {
        //Player is left of 
        if (playerX < x)
        {
            tryToMoveLeft(gameObject, displacement);
        }
        //Player is right of
        else if (playerX > x)
        {
            tryToMoveRight(gameObject, displacement);
        }
    }

    private void moveUpOrDown(int playerY, GameObject gameObject, float displacement)
    {

        //Player is above of 
        if (playerY < y)
        {
            tryToMoveUp(gameObject, displacement);
        }
        //Player is below of
        else if (playerY > y)
        {
            tryToMoveDown(gameObject, displacement);
        }
    }
}