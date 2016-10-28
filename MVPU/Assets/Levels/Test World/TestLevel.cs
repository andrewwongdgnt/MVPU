using UnityEngine;
using System.Collections;

public class TestLevel : MonoBehaviour {
	public GameModel gameModel;
    public GameObject player;
    public GameObject[] enemyArr;

    // Use this for initialization
    void Start () {
        gameModel.verticalSpace = 1;
        gameModel.horizontalSpace = 1.333f;

        gameModel.grid = new Cell[,]{
			{Cell.CELL_BOTTOM_CLOSED,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_BOTTOM_CLOSED,Cell.CELL_OPEN,},
			{Cell.CELL_TOP_CLOSED,Cell.CELL_TOP_BOTTOM_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_TOP_LEFT_CLOSED,Cell.CELL_OPEN,},
			{Cell.CELL_OPEN,Cell.CELL_TOP_CLOSED,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
            {Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_OPEN,Cell.CELL_RIGHT_CLOSED,Cell.CELL_LEFT_CLOSED,Cell.CELL_OPEN,},
        };
        gameModel.playerEntity = new Player(1, 2, gameModel.grid, gameModel);
        gameModel.player = player;

        gameModel.enemyEntityArr = new Enemy[] { new VEnemy(1, 1, gameModel.grid, gameModel) , new HEnemy(5, 2, gameModel.grid, gameModel) };
        gameModel.enemyArr = enemyArr;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
