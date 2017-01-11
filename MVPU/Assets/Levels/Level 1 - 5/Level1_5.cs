﻿using UnityEngine;
using System.Collections;

public class Level1_5 : LevelModel {

    protected override Cell[,] Grid()
    {
        return new Cell[,]{
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_TOP_BOTTOM_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,},
        };
    }


    protected override LevelManager.LevelID LevelId()
    {
        return LevelManager.LevelID.LEVEL_1_5;
    }
}
