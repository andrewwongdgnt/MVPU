using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager
{

    public enum LevelID
    {
        NO_LEVEL
            , TEST_LEVEL
            , LEVEL_1_1, LEVEL_1_2, LEVEL_1_3, LEVEL_1_4, LEVEL_1_5, LEVEL_1_6, LEVEL_1_7, LEVEL_1_8, LEVEL_1_9, LEVEL_1_10
            , LEVEL_2_1, LEVEL_2_2, LEVEL_2_3, LEVEL_2_4, LEVEL_2_5, LEVEL_2_6, LEVEL_2_7, LEVEL_2_8, LEVEL_2_9, LEVEL_2_10
            , LEVEL_3_1, LEVEL_3_2, LEVEL_3_3, LEVEL_3_4, LEVEL_3_5, LEVEL_3_6, LEVEL_3_7, LEVEL_3_8, LEVEL_3_9, LEVEL_3_10
            , LEVEL_4_1, LEVEL_4_2, LEVEL_4_3, LEVEL_4_4, LEVEL_4_5, LEVEL_4_6, LEVEL_4_7, LEVEL_4_8, LEVEL_4_9, LEVEL_4_10
            , LEVEL_5_1, LEVEL_5_2, LEVEL_5_3, LEVEL_5_4, LEVEL_5_5, LEVEL_5_6, LEVEL_5_7, LEVEL_5_8, LEVEL_5_9, LEVEL_5_10
            , LEVEL_6_1, LEVEL_6_2, LEVEL_6_3, LEVEL_6_4, LEVEL_6_5, LEVEL_6_6, LEVEL_6_7, LEVEL_6_8, LEVEL_6_9, LEVEL_6_10
            , LEVEL_7_1, LEVEL_7_2, LEVEL_7_3, LEVEL_7_4, LEVEL_7_5, LEVEL_7_6, LEVEL_7_7, LEVEL_7_8, LEVEL_7_9, LEVEL_7_10
            , LEVEL_8_1, LEVEL_8_2, LEVEL_8_3, LEVEL_8_4, LEVEL_8_5, LEVEL_8_6, LEVEL_8_7, LEVEL_8_8, LEVEL_8_9, LEVEL_8_10
            , LEVEL_9_1, LEVEL_9_2, LEVEL_9_3, LEVEL_9_4, LEVEL_9_5, LEVEL_9_6, LEVEL_9_7, LEVEL_9_8, LEVEL_9_9, LEVEL_9_10
            , LEVEL_10_1, LEVEL_10_2, LEVEL_10_3, LEVEL_10_4, LEVEL_10_5, LEVEL_10_6, LEVEL_10_7, LEVEL_10_8, LEVEL_10_9, LEVEL_10_10

    }

    //meant to be mutable
    public static LevelID levelToLoad = LevelID.LEVEL_3_6;

    public static LevelID[][] WorldToLevelArr = new LevelID[][]
    {
        new LevelID[] {LevelID.LEVEL_1_1,LevelID.LEVEL_2_1,LevelID.LEVEL_3_1,LevelID.LEVEL_4_1,LevelID.LEVEL_5_1,LevelID.LEVEL_6_1,LevelID.LEVEL_7_1,LevelID.LEVEL_8_1,LevelID.LEVEL_9_1,LevelID.LEVEL_10_1},
        new LevelID[] {LevelID.LEVEL_1_2,LevelID.LEVEL_2_2,LevelID.LEVEL_3_2,LevelID.LEVEL_4_2,LevelID.LEVEL_5_2,LevelID.LEVEL_6_2,LevelID.LEVEL_7_2,LevelID.LEVEL_8_2,LevelID.LEVEL_9_2,LevelID.LEVEL_10_2},
        new LevelID[] {LevelID.LEVEL_1_3,LevelID.LEVEL_2_3,LevelID.LEVEL_3_3,LevelID.LEVEL_4_3,LevelID.LEVEL_5_3,LevelID.LEVEL_6_3,LevelID.LEVEL_7_3,LevelID.LEVEL_8_3,LevelID.LEVEL_9_3,LevelID.LEVEL_10_3},
        new LevelID[] {LevelID.LEVEL_1_4,LevelID.LEVEL_2_4,LevelID.LEVEL_3_4,LevelID.LEVEL_4_4,LevelID.LEVEL_5_4,LevelID.LEVEL_6_4,LevelID.LEVEL_7_4,LevelID.LEVEL_8_4,LevelID.LEVEL_9_4,LevelID.LEVEL_10_4},
        new LevelID[] {LevelID.LEVEL_1_5,LevelID.LEVEL_2_5,LevelID.LEVEL_3_5,LevelID.LEVEL_4_5,LevelID.LEVEL_5_5,LevelID.LEVEL_6_5,LevelID.LEVEL_7_5,LevelID.LEVEL_8_5,LevelID.LEVEL_9_5,LevelID.LEVEL_10_5},
        new LevelID[] {LevelID.LEVEL_1_6,LevelID.LEVEL_2_6,LevelID.LEVEL_3_6,LevelID.LEVEL_4_6,LevelID.LEVEL_5_6,LevelID.LEVEL_6_6,LevelID.LEVEL_7_6,LevelID.LEVEL_8_6,LevelID.LEVEL_9_6,LevelID.LEVEL_10_6},
        new LevelID[] {LevelID.LEVEL_1_7,LevelID.LEVEL_2_7,LevelID.LEVEL_3_7,LevelID.LEVEL_4_7,LevelID.LEVEL_5_7,LevelID.LEVEL_6_7,LevelID.LEVEL_7_7,LevelID.LEVEL_8_7,LevelID.LEVEL_9_7,LevelID.LEVEL_10_7},
        new LevelID[] {LevelID.LEVEL_1_8,LevelID.LEVEL_2_8,LevelID.LEVEL_3_8,LevelID.LEVEL_4_8,LevelID.LEVEL_5_8,LevelID.LEVEL_6_8,LevelID.LEVEL_7_8,LevelID.LEVEL_8_8,LevelID.LEVEL_9_8,LevelID.LEVEL_10_8},
        new LevelID[] {LevelID.LEVEL_1_9,LevelID.LEVEL_2_9,LevelID.LEVEL_3_9,LevelID.LEVEL_4_9,LevelID.LEVEL_5_9,LevelID.LEVEL_6_9,LevelID.LEVEL_7_9,LevelID.LEVEL_8_9,LevelID.LEVEL_9_9,LevelID.LEVEL_10_9},
        new LevelID[] {LevelID.LEVEL_1_10,LevelID.LEVEL_2_10,LevelID.LEVEL_3_10,LevelID.LEVEL_4_10,LevelID.LEVEL_5_10,LevelID.LEVEL_6_10,LevelID.LEVEL_7_10,LevelID.LEVEL_8_10,LevelID.LEVEL_9_10,LevelID.LEVEL_10_10},
    };

    public static Dictionary<LevelID, Cell[,]> LevelGridMap = new Dictionary<LevelID, Cell[,]>
    {
        { LevelID.TEST_LEVEL, new Cell[,]{
            {Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,},
            {Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,},
        }
        },
        { LevelID.LEVEL_1_1, new Cell[,]{
            {Cell.CELL_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_TOP_RIGHT_CLOSED,Cell.CELL_LEFT_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,Cell.CELL_CLOSED,},
        }
        },
        { LevelID.LEVEL_1_2, new Cell[,]{
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
        }
        },
        { LevelID.LEVEL_1_3, new Cell[,]{
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_BOTTOM_CLOSED,Cell.CELL_TOP_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
        }
        },
        { LevelID.LEVEL_1_4, new Cell[,]{
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,Cell.CELL_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_TOP_RIGHT_CLOSED,Cell.CELL_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,},
            {Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
        }
        },
        { LevelID.LEVEL_1_5, new Cell[,]{
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,Cell.CELL_CLOSED,Cell.CELL_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
        }
        },
        { LevelID.LEVEL_1_6, new Cell[,]{
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_TOP_BOTTOM_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,},
        }
        },
        { LevelID.LEVEL_1_7, new Cell[,]{
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,},
        }
        },
        { LevelID.LEVEL_1_8, new Cell[,]{
            {Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
        }
        },
        { LevelID.LEVEL_1_9, new Cell[,]{
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_TOP_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,},
        }
        },
        { LevelID.LEVEL_1_10, new Cell[,]{
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
        }
        },
        { LevelID.LEVEL_2_1, new Cell[,]{
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_TOP_RIGHT_CLOSED,Cell.CELL_LEFT_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_BOTTOM_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,},
        }
        },
        { LevelID.LEVEL_2_2, new Cell[,]{
            {Cell.CELL_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,},
            {Cell.CELL_TOP_CLOSED,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,},
            {Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
        }
        },
        { LevelID.LEVEL_2_3, new Cell[,]{
            {Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_CLOSED,Cell.CELL_LEFT_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_TOP_RIGHT_CLOSED,Cell.CELL_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_BOTTOM_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,},
        }
        },
        { LevelID.LEVEL_2_4, new Cell[,]{
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_RIGHT_CLOSED,Cell.CELL_LEFT_RIGHT_CLOSED,Cell.CELL_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
        }
        },
        { LevelID.LEVEL_2_5, new Cell[,]{
           {Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_TOP_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,},
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_BOTTOM_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,},
        }
        },
        { LevelID.LEVEL_2_6, new Cell[,]{
           {Cell.CELL_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_BOTTOM_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,},
        }
        },
        { LevelID.LEVEL_2_7, new Cell[,]{
           {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
        }
        },
        { LevelID.LEVEL_2_8, new Cell[,]{
           {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
        }
        },
        { LevelID.LEVEL_2_9, new Cell[,]{
           {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,Cell.CELL_CLOSED,},
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,Cell.CELL_CLOSED,},
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,Cell.CELL_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,Cell.CELL_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_RIGHT_CLOSED,Cell.CELL_LEFT_RIGHT_CLOSED,Cell.CELL_LEFT_RIGHT_CLOSED,Cell.CELL_CLOSED,Cell.CELL_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,Cell.CELL_CLOSED,},
        }
        },
        { LevelID.LEVEL_2_10, new Cell[,]{
           {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
        }
        },
        { LevelID.LEVEL_3_1, new Cell[,]{
           {Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_RIGHT_CLOSED,Cell.CELL_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_TOP_CLOSED,},
            {Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
        }
        },
        { LevelID.LEVEL_3_2, new Cell[,]{
           {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,Cell.CELL_TOP_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
        }
        },
        { LevelID.LEVEL_3_3, new Cell[,]{
           {Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_TOP_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_BOTTOM_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,},
        }
        },
        { LevelID.LEVEL_3_4, new Cell[,]{
           {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_TOP_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,},
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_BOTTOM_CLOSED,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,},
        }
        },
        { LevelID.LEVEL_3_5, new Cell[,]{
           {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
        }
        },
        { LevelID.LEVEL_3_6, new Cell[,]{
           {Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,},
            {Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_RIGHT_CLOSED,Cell.CELL_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,},
        }
        },


    };

    public static Dictionary<LevelID, LevelScore> LevelScoreMap = new Dictionary<LevelID, LevelScore>
        {
            { LevelID.TEST_LEVEL, new LevelScore(10) },
            { LevelID.LEVEL_1_1, new LevelScore(6) },
            { LevelID.LEVEL_1_2, new LevelScore(8) },
            { LevelID.LEVEL_1_3, new LevelScore(9) },
            { LevelID.LEVEL_1_4, new LevelScore(11) },
            { LevelID.LEVEL_1_5, new LevelScore(9) },
            { LevelID.LEVEL_1_6, new LevelScore(10) },
            { LevelID.LEVEL_1_7, new LevelScore(9) },
            { LevelID.LEVEL_1_8, new LevelScore(13) },
            { LevelID.LEVEL_1_9, new LevelScore(16) },
            { LevelID.LEVEL_1_10, new LevelScore(15) },
            { LevelID.LEVEL_2_1, new LevelScore(12) },
            { LevelID.LEVEL_2_2, new LevelScore(11) },
            { LevelID.LEVEL_2_3, new LevelScore(8) },
            { LevelID.LEVEL_2_4, new LevelScore(11 ) },
            { LevelID.LEVEL_2_5, new LevelScore(12) },
            { LevelID.LEVEL_2_6, new LevelScore(11) },
            { LevelID.LEVEL_2_7, new LevelScore(12) },
            { LevelID.LEVEL_2_8, new LevelScore(15) },
            { LevelID.LEVEL_2_9, new LevelScore(15) },
            { LevelID.LEVEL_2_10, new LevelScore(24) },
            { LevelID.LEVEL_3_1, new LevelScore(12) },
            { LevelID.LEVEL_3_2, new LevelScore(16) },
            { LevelID.LEVEL_3_3, new LevelScore(21) },
            { LevelID.LEVEL_3_4, new LevelScore(22) },
            { LevelID.LEVEL_3_5, new LevelScore(7) },
            { LevelID.LEVEL_3_6, new LevelScore(12) },
        };
    public static Dictionary<LevelID, LevelID[]> LevelPrereq = new Dictionary<LevelID, LevelID[]>
        {
            { LevelID.TEST_LEVEL, new LevelID[] { } },
            { LevelID.LEVEL_1_1, new LevelID[] { } },
            { LevelID.LEVEL_1_2, new LevelID[] { LevelID.LEVEL_1_1 } },
            { LevelID.LEVEL_1_3, new LevelID[] { LevelID.LEVEL_1_2 } },
            { LevelID.LEVEL_1_4, new LevelID[] { LevelID.LEVEL_1_3 } },
            { LevelID.LEVEL_1_5, new LevelID[] { LevelID.LEVEL_1_4 } },
            { LevelID.LEVEL_1_6, new LevelID[] { LevelID.LEVEL_1_5 } },
            { LevelID.LEVEL_1_7, new LevelID[] { LevelID.LEVEL_1_6 } },
            { LevelID.LEVEL_1_8, new LevelID[] { LevelID.LEVEL_1_7 } },
            { LevelID.LEVEL_1_9, new LevelID[] { LevelID.LEVEL_1_8 } },
            { LevelID.LEVEL_1_10, new LevelID[] { LevelID.LEVEL_1_9 } },

            { LevelID.LEVEL_2_1, new LevelID[] { LevelID.LEVEL_1_10 } },
            { LevelID.LEVEL_2_2, new LevelID[] { LevelID.LEVEL_2_1 } },
            { LevelID.LEVEL_2_3, new LevelID[] { LevelID.LEVEL_2_2 } },
            { LevelID.LEVEL_2_4, new LevelID[] { LevelID.LEVEL_2_3 } },
            { LevelID.LEVEL_2_5, new LevelID[] { LevelID.LEVEL_2_4 } },
            { LevelID.LEVEL_2_6, new LevelID[] { LevelID.LEVEL_2_5 } },
            { LevelID.LEVEL_2_7, new LevelID[] { LevelID.LEVEL_2_6 } },
            { LevelID.LEVEL_2_8, new LevelID[] { LevelID.LEVEL_2_7 } },
            { LevelID.LEVEL_2_9, new LevelID[] { LevelID.LEVEL_2_8 } },
            { LevelID.LEVEL_2_10, new LevelID[] { LevelID.LEVEL_2_9 } },

            { LevelID.LEVEL_3_1, new LevelID[] { LevelID.LEVEL_2_10 } },
            { LevelID.LEVEL_3_2, new LevelID[] { LevelID.LEVEL_3_1 } },
            { LevelID.LEVEL_3_3, new LevelID[] { LevelID.LEVEL_3_2 } },
            { LevelID.LEVEL_3_4, new LevelID[] { LevelID.LEVEL_3_3 } },
            { LevelID.LEVEL_3_5, new LevelID[] { LevelID.LEVEL_3_4 } },
            { LevelID.LEVEL_3_6, new LevelID[] { LevelID.LEVEL_3_5 } },
            { LevelID.LEVEL_3_7, new LevelID[] { LevelID.LEVEL_3_6 } },
            { LevelID.LEVEL_3_8, new LevelID[] { LevelID.LEVEL_3_7 } },
            { LevelID.LEVEL_3_9, new LevelID[] { LevelID.LEVEL_3_8 } },
            { LevelID.LEVEL_3_10, new LevelID[] { LevelID.LEVEL_3_9 } },

            { LevelID.LEVEL_4_1, new LevelID[] { LevelID.LEVEL_3_10 } },
            { LevelID.LEVEL_4_2, new LevelID[] { LevelID.LEVEL_4_1 } },
            { LevelID.LEVEL_4_3, new LevelID[] { LevelID.LEVEL_4_2 } },
            { LevelID.LEVEL_4_4, new LevelID[] { LevelID.LEVEL_4_3 } },
            { LevelID.LEVEL_4_5, new LevelID[] { LevelID.LEVEL_4_4 } },
            { LevelID.LEVEL_4_6, new LevelID[] { LevelID.LEVEL_4_5 } },
            { LevelID.LEVEL_4_7, new LevelID[] { LevelID.LEVEL_4_6 } },
            { LevelID.LEVEL_4_8, new LevelID[] { LevelID.LEVEL_4_7 } },
            { LevelID.LEVEL_4_9, new LevelID[] { LevelID.LEVEL_4_8 } },
            { LevelID.LEVEL_4_10, new LevelID[] { LevelID.LEVEL_4_9 } },

            { LevelID.LEVEL_5_1, new LevelID[] { LevelID.LEVEL_4_10 } },
            { LevelID.LEVEL_5_2, new LevelID[] { LevelID.LEVEL_5_1 } },
            { LevelID.LEVEL_5_3, new LevelID[] { LevelID.LEVEL_5_2 } },
            { LevelID.LEVEL_5_4, new LevelID[] { LevelID.LEVEL_5_3 } },
            { LevelID.LEVEL_5_5, new LevelID[] { LevelID.LEVEL_5_4 } },
            { LevelID.LEVEL_5_6, new LevelID[] { LevelID.LEVEL_5_5 } },
            { LevelID.LEVEL_5_7, new LevelID[] { LevelID.LEVEL_5_6 } },
            { LevelID.LEVEL_5_8, new LevelID[] { LevelID.LEVEL_5_7 } },
            { LevelID.LEVEL_5_9, new LevelID[] { LevelID.LEVEL_5_8 } },
            { LevelID.LEVEL_5_10, new LevelID[] { LevelID.LEVEL_5_9 } },

            { LevelID.LEVEL_6_1, new LevelID[] { LevelID.LEVEL_5_10 } },
            { LevelID.LEVEL_6_2, new LevelID[] { LevelID.LEVEL_6_1 } },
            { LevelID.LEVEL_6_3, new LevelID[] { LevelID.LEVEL_6_2 } },
            { LevelID.LEVEL_6_4, new LevelID[] { LevelID.LEVEL_6_3 } },
            { LevelID.LEVEL_6_5, new LevelID[] { LevelID.LEVEL_6_4 } },
            { LevelID.LEVEL_6_6, new LevelID[] { LevelID.LEVEL_6_5 } },
            { LevelID.LEVEL_6_7, new LevelID[] { LevelID.LEVEL_6_6 } },
            { LevelID.LEVEL_6_8, new LevelID[] { LevelID.LEVEL_6_7 } },
            { LevelID.LEVEL_6_9, new LevelID[] { LevelID.LEVEL_6_8 } },
            { LevelID.LEVEL_6_10, new LevelID[] { LevelID.LEVEL_6_9 } },

            { LevelID.LEVEL_7_1, new LevelID[] { LevelID.LEVEL_6_10 } },
            { LevelID.LEVEL_7_2, new LevelID[] { LevelID.LEVEL_7_1 } },
            { LevelID.LEVEL_7_3, new LevelID[] { LevelID.LEVEL_7_2 } },
            { LevelID.LEVEL_7_4, new LevelID[] { LevelID.LEVEL_7_3 } },
            { LevelID.LEVEL_7_5, new LevelID[] { LevelID.LEVEL_7_4 } },
            { LevelID.LEVEL_7_6, new LevelID[] { LevelID.LEVEL_7_5 } },
            { LevelID.LEVEL_7_7, new LevelID[] { LevelID.LEVEL_7_6 } },
            { LevelID.LEVEL_7_8, new LevelID[] { LevelID.LEVEL_7_7 } },
            { LevelID.LEVEL_7_9, new LevelID[] { LevelID.LEVEL_7_8 } },
            { LevelID.LEVEL_7_10, new LevelID[] { LevelID.LEVEL_7_9 } },

            { LevelID.LEVEL_8_1, new LevelID[] { LevelID.LEVEL_7_10 } },
            { LevelID.LEVEL_8_2, new LevelID[] { LevelID.LEVEL_8_1 } },
            { LevelID.LEVEL_8_3, new LevelID[] { LevelID.LEVEL_8_2 } },
            { LevelID.LEVEL_8_4, new LevelID[] { LevelID.LEVEL_8_3 } },
            { LevelID.LEVEL_8_5, new LevelID[] { LevelID.LEVEL_8_4 } },
            { LevelID.LEVEL_8_6, new LevelID[] { LevelID.LEVEL_8_5 } },
            { LevelID.LEVEL_8_7, new LevelID[] { LevelID.LEVEL_8_6 } },
            { LevelID.LEVEL_8_8, new LevelID[] { LevelID.LEVEL_8_7 } },
            { LevelID.LEVEL_8_9, new LevelID[] { LevelID.LEVEL_8_8 } },
            { LevelID.LEVEL_8_10, new LevelID[] { LevelID.LEVEL_8_9 } },

            { LevelID.LEVEL_9_1, new LevelID[] { LevelID.LEVEL_8_10 } },
            { LevelID.LEVEL_9_2, new LevelID[] { LevelID.LEVEL_9_1 } },
            { LevelID.LEVEL_9_3, new LevelID[] { LevelID.LEVEL_9_2 } },
            { LevelID.LEVEL_9_4, new LevelID[] { LevelID.LEVEL_9_3 } },
            { LevelID.LEVEL_9_5, new LevelID[] { LevelID.LEVEL_9_4 } },
            { LevelID.LEVEL_9_6, new LevelID[] { LevelID.LEVEL_9_5 } },
            { LevelID.LEVEL_9_7, new LevelID[] { LevelID.LEVEL_9_6 } },
            { LevelID.LEVEL_9_8, new LevelID[] { LevelID.LEVEL_9_7 } },
            { LevelID.LEVEL_9_9, new LevelID[] { LevelID.LEVEL_9_8 } },
            { LevelID.LEVEL_9_10, new LevelID[] { LevelID.LEVEL_9_9 } },

            { LevelID.LEVEL_10_1, new LevelID[] { LevelID.LEVEL_9_10 } },
            { LevelID.LEVEL_10_2, new LevelID[] { LevelID.LEVEL_10_1 } },
            { LevelID.LEVEL_10_3, new LevelID[] { LevelID.LEVEL_10_2 } },
            { LevelID.LEVEL_10_4, new LevelID[] { LevelID.LEVEL_10_3 } },
            { LevelID.LEVEL_10_5, new LevelID[] { LevelID.LEVEL_10_4 } },
            { LevelID.LEVEL_10_6, new LevelID[] { LevelID.LEVEL_10_5 } },
            { LevelID.LEVEL_10_7, new LevelID[] { LevelID.LEVEL_10_6 } },
            { LevelID.LEVEL_10_8, new LevelID[] { LevelID.LEVEL_10_7 } },
            { LevelID.LEVEL_10_9, new LevelID[] { LevelID.LEVEL_10_8 } },
            { LevelID.LEVEL_10_10, new LevelID[] { LevelID.LEVEL_10_9 } },
        };
    public static Dictionary<LevelID, TutorialAction[]> TutorialContent = new Dictionary<LevelID, TutorialAction[]>
        {
            { LevelID.LEVEL_1_1, new TutorialAction[] {
                new TutorialAction("Hello!", TutorialAction.Action.NONE),
                new TutorialAction("My name is Chomp and I'm here to help you reunite the Monkey and Penguin.", TutorialAction.Action.NONE),
                new TutorialAction("To do that, you must move the Monkey along the grid while avoiding the rocks.", TutorialAction.Action.NONE),
                new TutorialAction("Swipe the screen to move the Monkey. Good luck!.", TutorialAction.Action.SWIPE) } },

            { LevelID.LEVEL_1_2, new TutorialAction[] {
                new TutorialAction("Hello again!", TutorialAction.Action.NONE),
                new TutorialAction("This is Kongo. He is grumpy and wants to attack the Monkey.", TutorialAction.Action.NONE),
                new TutorialAction("For every move the Monkey does, Kongo will try to follow along the blue path first and the red path second.", TutorialAction.Action.HENEMY),
                new TutorialAction("You have to try to confuse Kongo by making him walk into the rocks. Have fun!", TutorialAction.Action.NONE) } },
            { LevelID.LEVEL_1_3, new TutorialAction[] {
                new TutorialAction("Hello again!", TutorialAction.Action.NONE),
                new TutorialAction("Sometimes it is better to just not move at all and let Kongo walk into a trap.", TutorialAction.Action.NONE),
                new TutorialAction("Double tap on the screen to skip your move. Then head for the Penguin!", TutorialAction.Action.TAP) } },
            { LevelID.LEVEL_1_7, new TutorialAction[] {
                new TutorialAction("Hello again!", TutorialAction.Action.NONE),
                new TutorialAction("Looks like there are two Kongos, which means double the danger.", TutorialAction.Action.NONE),
                new TutorialAction("But not to worry! There is still a way to get the Monkey to the Penguin.", TutorialAction.Action.NONE),
                new TutorialAction("If you can get one Kongo to run into the other, only one will come out. Give it a try!", TutorialAction.Action.NONE) } },
            { LevelID.LEVEL_1_9, new TutorialAction[] {
                new TutorialAction("Uh Oh! Kongo looks really mad.", TutorialAction.Action.NONE),
                new TutorialAction("When Kongo is red, he can move two steps everytime the Monkey moves one.", TutorialAction.Action.NONE),
                new TutorialAction("So be extra careful with your moves. Good luck!", TutorialAction.Action.NONE) } },
            { LevelID.LEVEL_2_6, new TutorialAction[] {
                new TutorialAction("Hello! Did you miss me?", TutorialAction.Action.NONE),
                new TutorialAction("This is the Purple Monkey, because she's purple.", TutorialAction.Action.NONE),
                new TutorialAction("She has a heart stuck on her hands, so she wants to hit the Monkey with some love.", TutorialAction.Action.NONE),
                new TutorialAction("Just like Kongo, the Purple Monkey will follow the Monkey in a direct path.", TutorialAction.Action.NONE),
                new TutorialAction("But along the red path first and the blue path second. Try moving and see what happens. Good luck!", TutorialAction.Action.VENEMY) } },
            { LevelID.LEVEL_2_8, new TutorialAction[] {
                new TutorialAction("Oh look! The Purple Monkey's love level has risen.", TutorialAction.Action.NONE),
                new TutorialAction("When the Purple Monkey is red, that means she can move two steps per one move from the Monkey.", TutorialAction.Action.NONE),
                new TutorialAction("Good luck again!", TutorialAction.Action.NONE) } },
        };


}
