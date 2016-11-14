using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System;

public class UndoManager
{
    private int currentHistoryIndex = -1;
    private List<HistoryState> history = new List<HistoryState>();

    private HistoryState _initialHistoryState = new HistoryState();
    public HistoryState initialHistoryState
    {
        get
        {
            return _initialHistoryState;
        }
    }
    public void AddInitialState(Player player, Goal goal, Enemy[] enemyArr, Bomb[] bombArr)
    {
        _initialHistoryState = CreateHistoryState(player, goal, enemyArr, bombArr);
    }
    public void AddToHistory(Player player, Goal goal, Enemy[] enemyArr, Bomb[] bombArr)
    {
        if (history.Count > currentHistoryIndex + 1)
            history.RemoveRange(currentHistoryIndex + 1, history.Count - (currentHistoryIndex + 1));


        HistoryState historyState = CreateHistoryState(player, goal, enemyArr, bombArr);
        currentHistoryIndex++;
        history.Add(historyState);
    }

    private HistoryState CreateHistoryState(Player player, Goal goal, Enemy[] enemyArr, Bomb[] bombArr)
    {
        HistoryState historyState = new HistoryState();
        historyState.playerState = player.BuildDict();
        historyState.goalState = goal.BuildDict();
        Dictionary<string, object>[] enemyArrState = new Dictionary<string, object>[enemyArr.Length];
        for (int i = 0; i < enemyArr.Length; i++)
        {
            enemyArrState[i] = enemyArr[i].BuildDict();
        }
        historyState.enemyArrState = enemyArrState;
        Dictionary<string, object>[] bombArrState = new Dictionary<string, object>[bombArr.Length];
        for (int i = 0; i < bombArr.Length; i++)
        {
            bombArrState[i] = bombArr[i].BuildDict();
        }
        historyState.bombArrState = bombArrState;
        return historyState;
    }

    public HistoryState Undo()
    {
        if (currentHistoryIndex >= 0)
        {
            currentHistoryIndex--;
            if (currentHistoryIndex >= 0)
            {
                return history[currentHistoryIndex];
            }

        }
        return null;
    }

    public HistoryState Redo()
    {
        if (currentHistoryIndex < history.Count - 1)
        {
            currentHistoryIndex++; return history[currentHistoryIndex];
        }
        return null;

    }
}
