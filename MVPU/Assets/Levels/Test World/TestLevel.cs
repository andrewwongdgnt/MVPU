using UnityEngine;
using System.Collections;

public class TestLevel : MonoBehaviour
{
    public GameModel gameModel;
    public Player player;
    public Enemy[] enemyArr;
    public Bomb[] bombArr;

    public float verticalSpace;
    public float horizontalSpace;

    public float originX;
    public float originY;

    public int winX;
    public int winY;

    // Use this for initialization
    void Start()
    {
        gameModel.verticalSpace = verticalSpace;
        gameModel.horizontalSpace = horizontalSpace;
        gameModel.originX = originX;
        gameModel.originY = originY;
        gameModel.winX = winX;
        gameModel.winY = winY;

        gameModel.grid = new Cell[,]{
            {Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
        };

        gameModel.bombArr = bombArr;

        gameModel.player = player;

        gameModel.enemyArr = enemyArr;

        gameModel.Commence();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
