using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class GameModel : MonoBehaviour
{

    private float _verticalSpace;
    public float verticalSpace
    {
        get; set;
    }

    private float _horizontalSpace;
    public float horizontalSpace
    {
        get; set;
    }

    private Cell[,] _grid;
    public Cell[,] grid
    {
        get; set;
    }

    private Player _playerEntity;
    public Player playerEntity
    {
        get; set;
    }

    private GameObject _player;
    public GameObject player
    {
        get; set;
    }

    private Enemy[] _enemyEntityArr;
    public Enemy[] enemyEntityArr
    {
        get; set;
    }

    private GameObject[] _enemyArr;
    public GameObject[] enemyArr
    {
        get; set;
    }

    private bool endGame;

    // Use this for initialization
    void Start()
    {

    }

    private bool animationHappening;

    // Update is called once per frame
    void Update()
    {
        if (!animationHappening &&
            (Input.GetKeyDown(KeyCode.UpArrow)
            || Input.GetKeyDown(KeyCode.LeftArrow)
            || Input.GetKeyDown(KeyCode.DownArrow)
            || Input.GetKeyDown(KeyCode.RightArrow))
            )
        {

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerEntity.do_moveUp(player, verticalSpace, horizontalSpace);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                playerEntity.do_moveLeft(player, verticalSpace, horizontalSpace);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                playerEntity.do_moveDown(player, verticalSpace, horizontalSpace);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                playerEntity.do_moveRight(player, verticalSpace, horizontalSpace);
            }
            Debug.Log(playerEntity.x + " , " + playerEntity.y);

            for (int i = 0; i < enemyEntityArr.Length; i++)
            {
                Enemy vEnemyEntity = enemyEntityArr[i];
                GameObject vEnemy = enemyArr[i];


                vEnemyEntity.do_react(vEnemy, verticalSpace, horizontalSpace);



            }

        }

        if (endGame && !animationHappening)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }

    //Event Listeners
    public void moveGameObject(GameObject gameObject, Vector3 endPosition)
    {
        StartCoroutine(MoveOverSeconds(gameObject, endPosition, .5f));
    }

    public void look(GameObject gameObject, Entity.Direction direction)
    {
        if (direction == Entity.Direction.UP)
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        if (direction == Entity.Direction.LEFT)
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
        if (direction == Entity.Direction.DOWN)
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);
        if (direction == Entity.Direction.RIGHT)
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void checkForEndGame(Entity anEnemyEntity)
    {
        if (playerEntity.x == anEnemyEntity.x && playerEntity.y == anEnemyEntity.y)
            endGame = true;


    }

    // speed should be 1 unit per second
    private IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        animationHappening = true;
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        animationHappening = false;
    }

    private IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        animationHappening = true;
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
        animationHappening = false;
    }



}
