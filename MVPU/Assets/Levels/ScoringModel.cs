using UnityEngine;
using System.Collections;

public class ScoringModel
{
    public enum ScoreTypes
    {
        GOLD, SILVER, BRONZE, NONE
    }
    public int numberOfMoves
    {
        get; private set;
    }
    public int minOfMoves
    {
        get
        {
            return _levelScore.goldMoves;
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
            _gameModel.UpdateScoreText(numberOfMoves + "/" + _levelScore.goldMoves);
    }

    public ScoreTypes GetResult()
    {
        if (numberOfMoves <= _levelScore.goldMoves)
            return ScoreTypes.GOLD;
        else if (numberOfMoves <= _levelScore.silverMoves)
            return ScoreTypes.SILVER;
        else if (numberOfMoves <= _levelScore.bronzeMoves)
            return ScoreTypes.BRONZE;
        else
            return ScoreTypes.NONE;
    }

}
