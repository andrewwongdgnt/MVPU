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
                if (!tryToMoveLeftForEnemy(gameObject, hDisplacement))
                {
                    tryToMoveUpOrDownForEnemy(playerY, gameObject, vDisplacement);
                }

            }
            //Player is right of
            else if (playerX > x)
            {
                if (!tryToMoveRightForEnemy(gameObject, hDisplacement))
                {
                    tryToMoveUpOrDownForEnemy(playerY, gameObject, vDisplacement);
                }
            }
        }
        else
        {
            tryToMoveUpOrDownForEnemy(playerY, gameObject, vDisplacement);
        }

        _gameModel.checkForEndGame(this);
    }

    private void tryToMoveUpOrDownForEnemy(int playerY, GameObject gameObject, float vDisplacement)
    {

        //Player is above of 
        if (playerY < y)
        {
            tryToMoveUpForEnemy(gameObject, vDisplacement);
        }
        //Player is below of
        else if (playerY > y)
        {
            tryToMoveDownForEnemy(gameObject, vDisplacement);
        }
    }

}