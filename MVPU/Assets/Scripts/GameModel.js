#pragma strict

public class Cell
{
	private var _topBlocked : boolean;
	private var _leftBlocked : boolean;
	private var _bottomBlocked : boolean;
	private var _rightBlocked : boolean;
    
    public function Cell(topBlocked : boolean, leftBlocked : boolean, bottomBlocked : boolean, rightBlocked : boolean)
    {
        _topBlocked = topBlocked;
        _leftBlocked = leftBlocked;
        _bottomBlocked = bottomBlocked;
        _rightBlocked = rightBlocked;
    }

    public function isTopBlocked(): boolean
    {
    	return _topBlocked;
    }

    public function isLeftBlocked(): boolean
    {
    	return _leftBlocked;
    }

    public function isBottomBlocked(): boolean
    {
    	return _bottomBlocked;
    }

    public function isRightBlocked(): boolean
    {
    	return _rightBlocked;
    }
}


public var grid = 
[
[new Cell(false,false,false,false)],
[new Cell(false,false,false,false)]
];

public class Position
{
	private var _x : int;
	private var _y : int;

	public function Position(x : int, y : int)
    {
    	_x = x;
    	_y = y;
    }

    public function getX() : int
    {
    	return _x;
    }

    public function setX(x : int) : void
    {
    	_x = x;
    }

    public function getY() : int
    {
    	return _y;
    }

    public function setY(y : int) : void
    {
    	_y = y;
    }


}
public var playerPosition : Position;

function Start ()
{
	
}

function Update ()
{

    if (Input.GetKeyDown(KeyCode.UpArrow))
    {
        if (playerPosition.getY()>0)
        {
        	playerPosition.setY(playerPosition.getY()-1);
        }
        Debug.Log(playerPosition.getX() + " , " + playerPosition.getY());
	}
	else if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
        if (playerPosition.getX()>0)
        {
        	playerPosition.setX(playerPosition.getX()-1);
        }
        Debug.Log(playerPosition.getX() + " , " + playerPosition.getY());
	}
	else if (Input.GetKeyDown(KeyCode.DownArrow))
    {
        if (playerPosition.getY()<grid.length-1)
        {
        	playerPosition.setY(playerPosition.getY()+1);
        }
        Debug.Log(playerPosition.getX() + " , " + playerPosition.getY());
	}
	else if (Input.GetKeyDown(KeyCode.RightArrow))
    {
        if (playerPosition.getX()<grid[playerPosition.getX()].length-1)
        {
        	playerPosition.setX(playerPosition.getX()+1);
        }
        Debug.Log(playerPosition.getX() + " , " + playerPosition.getY());
	}

	//Debug.Log(grid[0][0].isTopBlocked());
}