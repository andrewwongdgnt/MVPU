using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HistoryState {

    public Dictionary<string, object> playerState
    {
        get;set;
    }
    public Dictionary<string, object> goalState
    {
        get; set;
    }
    public Dictionary<string, object>[] enemyArrState
    {
        get; set;
    }
    public Dictionary<string, object>[] bombArrState
    {
        get; set;
    }
    public Dictionary<string, object>[] keyArrState
    {
        get; set;
    }
}
