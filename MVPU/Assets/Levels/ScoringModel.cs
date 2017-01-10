using UnityEngine;
using System.Collections;

public class ScoringModel
{
    public enum ScoreTypes
    {
        MIN, GREAT, GOOD, ADEQUATE, NONE
    }
    public int numberOfMoves
    {
        get; private set;
    }
    public int minOfMoves
    {
        get
        {
            return _levelScore.minMoveCount;
        }
    }
    private LevelScore _levelScore;

    private GameModel _gameModel;
    public ScoringModel(LevelScore levelScore, GameModel gameModel)
    {
        _levelScore = levelScore;
        _gameModel = gameModel;
        DispatchScore();
    }

    public void AddMove()
    {
        numberOfMoves++;
        DispatchScore();
    }
    public void SubtractMove()
    {
        numberOfMoves--;
        if (numberOfMoves < 0)
            numberOfMoves = 0;
        DispatchScore();
    }

    private void DispatchScore()
    {
        if (_gameModel != null)
        {
            _gameModel.UpdateScoreText(numberOfMoves,_levelScore.minMoveCount);
        }
    }

    public ScoreTypes GetResult()
    {
        if (numberOfMoves <= _levelScore.minMoveCount)
            return ScoreTypes.MIN;
        else if (numberOfMoves <= _levelScore.greatMoveCount)
            return ScoreTypes.GREAT;
        else if (numberOfMoves <= _levelScore.goodMoveCount)
            return ScoreTypes.GOOD;
        else if (numberOfMoves <= _levelScore.adequateMoveCount)
            return ScoreTypes.ADEQUATE;
        else
            return ScoreTypes.NONE;
    }

}
