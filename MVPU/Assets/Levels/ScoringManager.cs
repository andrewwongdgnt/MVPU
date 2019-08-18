using UnityEngine;
using System.Collections;

public class ScoringManager
{
    public enum ScoreTypes
    {
        NONE, ADEQUATE, GOOD, GREAT, MIN
    }
    public int numberOfMoves
    {
        get; private set;
    }

    private SaveStateUtil.LevelState _levelState;

    private GameModel _gameModel;
    public ScoringManager(SaveStateUtil.LevelState levelState, GameModel gameModel)
    {
        _levelState = levelState;
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
            _gameModel.UpdateScoreText(numberOfMoves,_levelState==null ? (int?) null : _levelState.moveCount);
        }
    }

    public static ScoreTypes GetResult(int numberOfMoves, LevelUtil.LevelID levelId)
    {
        return GetResult(numberOfMoves, LevelUtil.LevelScoreMap[levelId]);
    }
    private static ScoreTypes GetResult(int numberOfMoves, LevelScore levelScore)
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
