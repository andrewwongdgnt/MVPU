using UnityEngine;
using System.Collections;
[System.Serializable]
public class LevelScore 
{
    //Minimum number of moves to beat a level
    public int goldMoves
    {
        get; private set;
    }

    //Number of moves that is considered par
    public int silverMoves
    {
        get; private set;
    }

    //Highest number of moves before considered subpar
    public int bronzeMoves
    {
        get; private set;
    }

    public LevelScore (int goldMoves, int silverMoves, int bronzeMoves)
    {
        this.goldMoves = goldMoves;
        this.silverMoves = silverMoves;
        this.bronzeMoves = bronzeMoves;
    }


    public LevelScore(int goldMoves)
    {

        this.goldMoves = goldMoves;
        silverMoves = goldMoves+1;
        bronzeMoves = silverMoves + 1;
    }
}
