using UnityEngine;
using System.Collections;

public class Level1_1 : MonoBehaviour {
	public GameModel gameModel;
	// Use this for initialization
	void Start () {
		gameModel.grid = new GameModel.Cell[,]{
			{GameModel.Cell.CELL_OPEN,GameModel.Cell.CELL_OPEN,GameModel.Cell.CELL_BOTTOM_CLOSED,},
			{GameModel.Cell.CELL_OPEN,GameModel.Cell.CELL_OPEN,GameModel.Cell.CELL_TOP_CLOSED,},
			{GameModel.Cell.CELL_OPEN,GameModel.Cell.CELL_OPEN,GameModel.Cell.CELL_OPEN,},
		};
		gameModel.playerPosition = new GameModel.Position (0, 0, gameModel.grid );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
