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
        return GetResult(numberOfMoves, _levelScore);
    }
    public static ScoreTypes GetResult(int numberOfMoves, LevelManager.LevelID levelId)
    {
        return GetResult(numberOfMoves, LevelManager.LevelScoreMap[levelId]);
    }
    public static ScoreTypes GetResult(int numberOfMoves, LevelScore levelScore)
    {
        if (numberOfMoves <= levelScore.minMoveCount)
            return ScoreTypes.MIN;
        else if (numberOfMoves <= levelScore.greatMoveCount)
            return ScoreTypes.GREAT;
        else if (numberOfMoves <= levelScore.goodMoveCount)
            return ScoreTypes.GOOD;
        else if (numberOfMoves <= levelScore.adequateMoveCount)
            return ScoreTypes.ADEQUATE;
        else
            return ScoreTypes.NONE;
    }

}
