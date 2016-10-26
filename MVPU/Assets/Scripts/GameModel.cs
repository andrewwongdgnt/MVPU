using UnityEngine;
using System.Collections;

public class GameModel : MonoBehaviour
{

    public float verticalSpace;
    public float horizontalSpace;

    public class Cell
    {
        public static readonly Cell CELL_OPEN = new Cell(false, false, false, false);
        public static readonly Cell CELL_TOP_CLOSED = new Cell(true, false, false, false);
        public static readonly Cell CELL_BOTTOM_CLOSED = new Cell(false, false, true, false);

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

    public Cell[,] grid;

    public class Entity
    {
        public enum Direction
        {
            UP, LEFT, DOWN, RIGHT
        }

        private int _x;
        public int x
        {
            get
            {
                return _x;
            }
        }

        private int _y;
        public int y
        {
            get
            {
                return _y;
            }
        }

        private Cell[,] _grid;
        public Cell[,] grid
        {
            get
            {
                return _grid;
            }
        }

        private GameModel _gameModel;

        public Entity(int x, int y, Cell[,] grid, GameModel gameModel)
        {
            _x = x;
            _y = y;
            _grid = grid;
            _gameModel = gameModel;
        }

        public void moveUp(GameObject gameObject, float displacement)
        {
            if (y > 0 && !grid[y, x].topBlocked)
            {
                _y--;
                _gameModel.moveGameObject(gameObject, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + displacement, gameObject.transform.position.z), Direction.UP);
            }
        }

        public void moveLeft(GameObject gameObject, float displacement)
        {
            if (x > 0 && !grid[y, x].leftBlocked)
            {
                _x--;
                _gameModel.moveGameObject(gameObject, new Vector3(gameObject.transform.position.x - displacement, gameObject.transform.position.y, gameObject.transform.position.z), Direction.LEFT);
            }
        }

        public void moveDown(GameObject gameObject, float displacement)
        {
            if (y < grid.GetLength(0) - 1 && !grid[y, x].bottomBlocked)
            {
                _y++;
                _gameModel.moveGameObject(gameObject, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - displacement, gameObject.transform.position.z), Direction.DOWN);
            }
        }

        public void moveRight(GameObject gameObject, float displacement)
        {
            if (x < grid.GetLength(1) - 1 && !grid[y, x].rightBlocked)
            {
                _x++;
                _gameModel.moveGameObject(gameObject, new Vector3(gameObject.transform.position.x + displacement, gameObject.transform.position.y, gameObject.transform.position.z), Direction.RIGHT);
            }
        }



    }

    public Entity playerEntity;


    public GameObject player;

    // Use this for initialization
    void Start()
    {

    }

    private bool movementDisabled;

    // Update is called once per frame
    void Update()
    {
        if (!movementDisabled &&
            (
            Input.GetKeyDown(KeyCode.UpArrow)
            || Input.GetKeyDown(KeyCode.LeftArrow)
            || Input.GetKeyDown(KeyCode.DownArrow)
            || Input.GetKeyDown(KeyCode.RightArrow)
            ))
        {

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerEntity.moveUp(player, verticalSpace);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                playerEntity.moveLeft(player, horizontalSpace);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                playerEntity.moveDown(player, verticalSpace);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                playerEntity.moveRight(player, horizontalSpace);
            }
            Debug.Log(playerEntity.x + " , " + playerEntity.y);

        }
    }

    //Event Listeners
    public void moveGameObject(GameObject gameObject, Vector3 endPosition, Entity.Direction direction)
    {
        StartCoroutine(MoveOverSeconds(player, endPosition, .5f));
        if (direction == Entity.Direction.UP)
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        if (direction == Entity.Direction.LEFT)
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);

        if (direction == Entity.Direction.DOWN)
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);
        if (direction == Entity.Direction.RIGHT)
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);


    }

    // speed should be 1 unit per second
    private IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        movementDisabled = true;
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        movementDisabled = false;
    }

    private IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        movementDisabled = true;
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
        movementDisabled = false;
    }



}
