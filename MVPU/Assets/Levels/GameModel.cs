using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System;
using System.Collections.Generic;

public class GameModel : MonoBehaviour
{
    private readonly float ANIMATION_DELAY = 0.5f;

    public float verticalSpace
    {
        get; set;
    }

    public float horizontalSpace
    {
        get; set;
    }

    public float originX
    {
        get; set;
    }

    public float originY
    {
        get; set;
    }

    public Cell[,] grid
    {
        get; set;
    }

    private Player _player;
    public Player player
    {

        set
        {
            _player = value;
            _player.gameModel = this;
        }
    }

    public int playerX
    {
        get
        {
            return _player.x;
        }
    }

    public int playerY
    {
        get
        {
            return _player.y;
        }
    }


    private Enemy[] _enemyArr;
    public Enemy[] enemyArr
    {
        set
        {
            _enemyArr = value;
            Array.ForEach(_enemyArr, en =>
            {
                en.gameModel = this;
            });
        }
    }

    private bool endGame;
    private List<List<bool>> animationComplete = new List<List<bool>>();



    public void Commence()
    {
        //for Player
        List<bool> stepForPlayer = new List<bool>();
        stepForPlayer.Add(true);
        animationComplete.Add(stepForPlayer);

        Array.ForEach(_enemyArr, en =>
        {

            List<bool> step = new List<bool>();
            for (int i = 0; i < en.stepsPerMove; i++)
                step.Add(true);
            animationComplete.Add(step);

        });

        //set positions for each entity
        _player.transform.position = new Vector3(originX + _player.x * horizontalSpace, originY + _player.y * -verticalSpace, _player.transform.position.z);

        Array.ForEach(_enemyArr, en =>
        {
            en.transform.position = en.transform.position = new Vector3(originX + en.x * horizontalSpace, originY + en.y * -verticalSpace, en.transform.position.z);
        });

    }

    private void updateAnimationCompleteListWith(bool value)
    {
        for (int i = 0; i < animationComplete.Count; i++)
        {

            List<bool> step = new List<bool>();

            if (i == 0)
            {
                step.Add(value);
            }
            else
            {
                Enemy enemy = _enemyArr[i - 1];
                for (int j = 0; j < enemy.stepsPerMove; j++)
                    step.Add(value);
            }
            animationComplete[i] = step;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsUserActionEnabled() &&
            (Input.GetKeyDown(KeyCode.UpArrow)
            || Input.GetKeyDown(KeyCode.LeftArrow)
            || Input.GetKeyDown(KeyCode.DownArrow)
            || Input.GetKeyDown(KeyCode.RightArrow))
            )
        {

            updateAnimationCompleteListWith(false);

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _player.Do_MoveUp(verticalSpace, horizontalSpace);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _player.Do_MoveLeft(verticalSpace, horizontalSpace);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _player.Do_MoveDown(verticalSpace, horizontalSpace);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _player.Do_MoveRight(verticalSpace, horizontalSpace);
            }
            Debug.Log(_player.x + " , " + _player.y);

            for (int i = 0; i < _enemyArr.Length; i++)
            {
                Enemy enemy = _enemyArr[i];
                enemy.Do_React(verticalSpace, horizontalSpace);
            }

        }

        if (endGame && IsUserActionEnabled())
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }

    private bool IsUserActionEnabled()
    {
        return animationComplete.All(list => list.All(b => b));
    }

    //Event Listeners
    public void AnimateGameObject(Entity entity, Entity.Direction direction, int stepOrder)
    {
        int order = GetOrder(entity);

        Vector3 endPosition = new Vector3(originX + entity.x * horizontalSpace, originY + entity.y * -verticalSpace, entity.transform.position.z);
        StartCoroutine(MoveOverSeconds(entity, endPosition, direction, ANIMATION_DELAY, order, stepOrder));
    }
    //End Event Listener

    public void Look(Entity entity, Entity.Direction direction)
    {
        //TODO: not the right way to do it but leave for testing
        if (direction == Entity.Direction.UP)
            entity.transform.rotation = Quaternion.Euler(0, 0, 90);
        if (direction == Entity.Direction.LEFT)
            entity.transform.rotation = Quaternion.Euler(0, 0, 180);
        if (direction == Entity.Direction.DOWN || direction == Entity.Direction.NEUTRAL)
            entity.transform.rotation = Quaternion.Euler(0, 0, 270);
        if (direction == Entity.Direction.RIGHT)
            entity.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public bool IsAnEnemyInTheWay(Predicate<Enemy> predicate)
    {
        return !Array.Exists(_enemyArr, predicate);
    }

    private int GetOrder(Entity entity)
    {
        int order = entity == _player ? 0 : 1 + Array.FindIndex(_enemyArr, en => en == entity);
        return order;
    }

    public void CheckForEndGame(Entity anEnemyEntity)
    {
        if (_player.x == anEnemyEntity.x && _player.y == anEnemyEntity.y)
            endGame = true;
    }

    // speed should be 1 unit per second
    private IEnumerator moveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator MoveOverSeconds(Entity objectToMove, Vector3 end, Entity.Direction direction, float seconds, int order, int stepOrder)
    {


        yield return new WaitUntil(() => order == 0 ? true : stepOrder == 0 ? animationComplete[order - 1][animationComplete[order - 1].Count - 1] : animationComplete[order][stepOrder - 1]);

        if (direction == Entity.Direction.NEUTRAL)
            updateAnimationCompleteListWith(true);

        Look(objectToMove, direction);
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;

        animationComplete[order][stepOrder] = true;
        Enemy enemyTouchingPlayer = Array.Find(_enemyArr, en => en.x == _player.x && en.y == _player.y);
        if (enemyTouchingPlayer != null && (animationComplete[0][0] && animationComplete[GetOrder(enemyTouchingPlayer)].All(b=>b)))
        {
            updateAnimationCompleteListWith(true);
        }

    }



}
