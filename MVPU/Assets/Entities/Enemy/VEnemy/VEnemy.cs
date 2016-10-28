using UnityEngine;
using System.Collections;

public class VEnemy : Enemy
{
    public VEnemy(int x, int y, Cell[,] grid, GameModel gameModel) : base(x, y, grid, gameModel)
    {
    }


    public override void do_react(GameObject gameObject, float vDisplacement, float hDisplacement)
    {
        Player player = _gameModel.playerEntity;
        int playerX = player.x;
        int playerY = player.y;

        if (playerY != y)
        {
            //Player is above of 
            if (playerY < y)
            {      
                if (!tryToMoveUpForEnemy(gameObject, vDisplacement))
                {
                    tryToMoveLeftOrRightForEnemy(playerX, gameObject, hDisplacement);
                }                

            }
            //Player is below of
            else if (playerY > y)
            {
                if (!tryToMoveDownForEnemy(gameObject, vDisplacement))
                {
                    tryToMoveLeftOrRightForEnemy(playerX, gameObject, hDisplacement);
                }
            }
        }
        else
        {
            tryToMoveLeftOrRightForEnemy(playerX, gameObject, hDisplacement);
        }

        _gameModel.checkForEndGame(this);
    }

    private void tryToMoveLeftOrRightForEnemy(int playerX, GameObject gameObject, float hDisplacement)
    {

        //Player is left of 
        if (playerX < x)
        {
            tryToMoveLeftForEnemy(gameObject, hDisplacement);
        }
        //Player is right of
        else if (playerX > x)
        {
            tryToMoveRightForEnemy(gameObject, hDisplacement);
        }
    }

}