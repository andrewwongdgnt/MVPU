﻿using UnityEngine;
using System.Collections;
using System;

public class TestLevel : LevelModel
{


    protected override Cell[,] grid()
    {
        return new Cell[,]{
            {Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
        };
    }
}
