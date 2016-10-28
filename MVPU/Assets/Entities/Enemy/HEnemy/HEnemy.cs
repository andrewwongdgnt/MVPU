using UnityEngine;
using System.Collections;

public class HEnemy : Enemy
{
    public HEnemy(int x, int y, Cell[,] grid, GameModel gameModel) : base(x, y, grid, gameModel)
    {
    }


    public override void do_react(GameObject gameObject, float vDisplacement, float hDisplacement)
    {
        Player player = _gameModel.playerEntity;
        int playerX = player.x;
        int playerY = player.y;

        if (playerX != x)
        {
            //Player is left of 
            if (playerX < x)
            {
                if (!tryToMoveLeft(gameObject, hDisplacement))
                {
                    tryToMoveUpOrDown(playerY, gameObject, vDisplacement);
                }

            }
            //Player is right of
            else if (playerX > x)
            {
                if (!tryToMoveRight(gameObject, hDisplacement))
                {
                    tryToMoveUpOrDown(playerY, gameObject, vDisplacement);
                }
            }
        }
        else
        {
            tryToMoveUpOrDown(playerY, gameObject, vDisplacement);
        }

        _gameModel.checkForEndGame(this);
    }

    private void tryToMoveUpOrDown(int playerY, GameObject gameObject, float vDisplacement)
    {

        //Player is above of 
        if (playerY < y)
        {
            tryToMoveUp(gameObject, vDisplacement);
        }
        //Player is below of
        else if (playerY > y)
        {
            tryToMoveDown(gameObject, vDisplacement);
        }
    }

}