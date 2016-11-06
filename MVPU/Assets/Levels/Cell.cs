using UnityEngine;
using System.Collections;

public class Cell
{
    public static readonly Cell CELL_OPEN = new Cell(false, false, false, false);
    public static readonly Cell CELL_TOP_CLOSED = new Cell(true, false, false, false);
    public static readonly Cell CELL_LEFT_CLOSED = new Cell(false, true, false, false);
    public static readonly Cell CELL_BOTTOM_CLOSED = new Cell(false, false, true, false);
    public static readonly Cell CELL_RIGHT_CLOSED = new Cell(false, false, false, true);
    public static readonly Cell CELL_TOP_LEFT_CLOSED = new Cell(true, true, false, false);
    public static readonly Cell CELL_TOP_BOTTOM_CLOSED = new Cell(true, false, true, false);
    public static readonly Cell CELL_TOP_RIGHT_CLOSED = new Cell(true, false, false, true);
    public static readonly Cell CELL_LEFT_BOTTOM_CLOSED = new Cell(false, true, true, false);
    public static readonly Cell CELL_LEFT_RIGHT_CLOSED = new Cell(false, true, false, true);
    public static readonly Cell CELL_BOTTOM_RIGHT_CLOSED = new Cell(false, false, true, true);

    private bool _topBlocked;
    public bool topBlocked
    {
        get
        {
            return _topBlocked;
        }
    }

    private bool _leftBlocked;
    public bool leftBlocked
    {
        get
        {
            return _leftBlocked;
        }
    }

    private bool _bottomBlocked;
    public bool bottomBlocked
    {
        get
        {
            return _bottomBlocked;
        }
    }

    private bool _rightBlocked;
    public bool rightBlocked
    {
        get
        {
            return _rightBlocked;
        }
    }

    public Cell(bool topBlocked, bool leftBlocked, bool bottomBlocked, bool rightBlocked)
    {
        _topBlocked = topBlocked;
        _leftBlocked = leftBlocked;
        _bottomBlocked = bottomBlocked;
        _rightBlocked = rightBlocked;
    }

}
