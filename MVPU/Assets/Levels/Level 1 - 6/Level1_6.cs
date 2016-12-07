﻿using UnityEngine;
using System.Collections;

public class Level1_6 : LevelModel {

    protected override Cell[,] Grid()
    {
        return new Cell[,]{
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,},
        };
    }


    protected override string LevelId()
    {
        return "Level 1 - 6";
    }
}
