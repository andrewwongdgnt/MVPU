using UnityEngine;
using System.Collections;

public class TutorialAction  {

    public enum Action
    {
        NONE, ALL,SWIPE, TAP, HENEMY, VENEMY
    }

    private string _text;
    public string text
    {
        get
        {
            return _text;
        }
    }

    private Action _action;
    public Action action
    {
        get
        {
            return _action;
        }
    }

    public TutorialAction(string text, Action action)
    {
        _text = text;
        _action = action;
    }

    public static bool isNoAction(Action action)
    {
        return action == Action.NONE || action == Action.HENEMY || action == Action.VENEMY;
    }
}
