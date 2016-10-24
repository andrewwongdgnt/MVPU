using UnityEngine;
using System.Collections;

public class GameModel : MonoBehaviour {


	public class Cell
	{
		public static readonly Cell CELL_OPEN = new Cell(false,false,false,false);
		public static readonly Cell CELL_TOP_CLOSED = new Cell(true,false,false,false);
		public static readonly Cell CELL_BOTTOM_CLOSED = new Cell(false,false,true,false);

		private bool _topBlocked;
		public bool topBlocked {
			get 
			{
				return _topBlocked;
			}
		}

		private bool _leftBlocked;
		public bool leftBlocked {
			get 
			{
				return _leftBlocked;
			}
		}

		private bool _bottomBlocked;
		public bool bottomBlocked {
			get 
			{
				return _bottomBlocked;
			}
		}

		private bool _rightBlocked;
		public bool rightBlocked {
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

	public Cell[,] grid;

	public class Position
	{
		private int _x;
		public int x {
			get {
				return _x;
			}
		}

		private int _y;
		public int y {
			get {
				return _y;
			}
		}

		private Cell[,] _grid;	
		public Cell[,] grid {
			get {
				return _grid;
			}
		}

		public Position (int x, int y, Cell[,] grid)
		{
			_x = x;
			_y = y;
			_grid = grid;
		}

		public void moveUp()
		{
			if (y > 0 && !grid[y,x].topBlocked) 
			{
				_y--;
			}
		}

		public void moveLeft()
		{
			if (x > 0 && !grid[y,x].leftBlocked) 
			{
				_x--;
			}
		}

		public void moveDown()
		{
			if (y < grid.GetLength(1)-1 && !grid[y,x].bottomBlocked) 
			{
				_y++;
			}
		}

		public void moveRight()
		{
			if (x < grid.GetLength(0)-1 && !grid[y,x].rightBlocked) 
			{
				_x++;
			}
		}


	
	}

	public Position playerPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow)
			|| Input.GetKeyDown(KeyCode.LeftArrow)
			|| Input.GetKeyDown(KeyCode.DownArrow)
			|| Input.GetKeyDown(KeyCode.RightArrow))
		{

			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				playerPosition.moveUp();
			}
			else if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				playerPosition.moveLeft();
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				playerPosition.moveDown ();
			}
			else if (Input.GetKeyDown(KeyCode.RightArrow))
			{				
				playerPosition.moveRight ();
			}
			Debug.Log(playerPosition.x + " , " + playerPosition.y);

		}
	}
}
