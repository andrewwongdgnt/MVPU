using UnityEngine;
using System.Collections;
[System.Serializable]
public class LevelScore : System.Object
{
    //Minimum number of moves to beat a level
    public int goldMoves;

    //Number of moves that is considered par
    public int silverMoves;

    //Highest number of moves before considered subpar
    public int bronzeMoves;
}
