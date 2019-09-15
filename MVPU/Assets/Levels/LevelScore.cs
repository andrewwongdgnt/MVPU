using UnityEngine;
using System.Collections;
[System.Serializable]
public class LevelScore
{

    public int minMoveCount
    {
        get; private set;
    }
    public int greatMoveCount
    {
        get; private set;
    }

    public int goodMoveCount
    {
        get; private set;
    }

    public int adequateMoveCount
    {
        get; private set;
    }

    public LevelScore (int minMoveCount, int greatMoveCount, int goodMoveCount, int adequateMoveCount)
    {
        this.minMoveCount = minMoveCount;
        this.greatMoveCount = greatMoveCount;
        this.goodMoveCount = goodMoveCount;
        this.adequateMoveCount = adequateMoveCount;
    }


    public LevelScore(int minMoveCount)
    {

        this.minMoveCount = minMoveCount;
        greatMoveCount = minMoveCount+3;
        goodMoveCount = greatMoveCount + 3;
        adequateMoveCount = goodMoveCount + 3;
    }

}
