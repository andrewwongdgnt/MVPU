using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;
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

    public Bomb[] bombArr
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

    private Goal _goal;
    public Goal goal
    {
        set
        {
            _goal = value;
            _goal.gameModel = this;
        }
    }

    public float goalX
    {
        get
        {
            return _goal.x;
        }
    }

    public float goalY
    {

        get
        {
            return _goal.y;
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


    private List<List<bool>> animationComplete = new List<List<bool>>();
    private List<Triple<int, int, Enemy>> dozedEnemiesList = new List<Triple<int, int, Enemy>>();
    //win=true
    private Triple<int, int, bool> gameEndInfo;

    private List<Pair<int, int>> blockedEnemiesList = new List<Pair<int, int>>();
    private List<Triple<int, int, Bomb>> bombedEnemiesList = new List<Triple<int, int, Bomb>>();



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
        _goal.transform.position = new Vector3(originX + _goal.x * horizontalSpace, originY + _goal.y * -verticalSpace, _goal.transform.position.z);

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
        if (areAllAnimationsComplete() &&
            (Input.GetKeyDown(KeyCode.UpArrow)
            || Input.GetKeyDown(KeyCode.LeftArrow)
            || Input.GetKeyDown(KeyCode.DownArrow)
            || Input.GetKeyDown(KeyCode.RightArrow))
            )
        {

            updateAnimationCompleteListWith(false);
            dozedEnemiesList.Clear();
            blockedEnemiesList.Clear();
            bombedEnemiesList.Clear();

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _player.Do_MoveUp();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _player.Do_MoveLeft();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _player.Do_MoveDown();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _player.Do_MoveRight();
            }
            Debug.Log("Player new position (" + _player.x + ", " + _player.y + ")");

            for (int i = 0; i < _enemyArr.Length; i++)
            {
                Enemy enemy = _enemyArr[i];
                enemy.Do_React();
                Debug.Log(enemy + " new position (" + enemy.x + ", " + enemy.y + ")");
            }

        }

        if (areAllAnimationsComplete())
        {
            if (gameEndInfo != null)
            {
                if (gameEndInfo.third)
                {
                    Debug.Log("Game Win");
                }
                else
                {

                    Debug.Log("Game Over");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }


    }

    private bool areAllAnimationsComplete()
    {
        return animationComplete.All(list => list.All(b => b));
    }


    public void Look(Entity entity, Entity.Direction direction)
    {
        //TODO: not the right way to do it but leave for testing
        if (direction == Entity.Direction.UP)
            entity.transform.rotation = Quaternion.Euler(0, 0, 90);
        if (direction == Entity.Direction.LEFT)
            entity.transform.rotation = Quaternion.Euler(0, 0, 180);
        if (direction == Entity.Direction.DOWN || direction == Entity.Direction.NONE)
            entity.transform.rotation = Quaternion.Euler(0, 0, 270);
        if (direction == Entity.Direction.RIGHT)
            entity.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public bool IsAnEnemyInTheWay(Predicate<Enemy> predicate)
    {
        return Array.Exists(_enemyArr, predicate);
    }

    public bool IsGoalInTheWay(Func<Goal, bool> predicate)
    {
        return predicate(_goal);
    }

    private int GetOrder(Entity entity)
    {
        int order = entity == _player ? 0 : 1 + Array.FindIndex(_enemyArr, en => en == entity);
        return order;
    }

    public void CheckForEndGame(Entity entity, int stepOrder)
    {
        if (gameEndInfo == null)
        {
            if (_player.x == goalX && _player.y == goalY)
            {
                gameEndInfo = new Triple<int, int, bool>(GetOrder(_player), stepOrder, true);

            }
            else
            {
                if (entity == _player)
                {
                    Enemy theKiller = Array.Find(_enemyArr, en => en.x == _player.x && en.y == _player.y);
                    Bomb bomb = Array.Find(bombArr, bo => bo.x == _player.x && bo.y == _player.y);
                    if (theKiller != null || (bomb != null && !bomb.inactive && bomb.affectsPlayer))
                    {
                        gameEndInfo = new Triple<int, int, bool>(GetOrder(_player), stepOrder, false);
                    }
                    else if (Array.Exists(bombArr, bo => bo.x == _player.x && bo.y == _player.y))
                    {
                        gameEndInfo = new Triple<int, int, bool>(GetOrder(_player), stepOrder, false);
                    }
                }
                else if (_player.x == entity.x && _player.y == entity.y)
                    gameEndInfo = new Triple<int, int, bool>(GetOrder(entity), stepOrder, false);

            }
        }
    }

    public void CheckIfOtherEnemiesGotDozed(Enemy enemy, int stepOrder)
    {
        Enemy dozedEnemy = Array.Find(_enemyArr, en => en != enemy && en.x == enemy.x && en.y == enemy.y);
        if (dozedEnemy != null && !dozedEnemy.inactive)
        {
            dozedEnemy.inactive = true;

            dozedEnemiesList.Add(new Triple<int, int, Enemy>(GetOrder(enemy), stepOrder, dozedEnemy));

        }
    }

    public void SetBlocked(Enemy enemy, int stepOrder)
    {
        blockedEnemiesList.Add(new Pair<int, int>(GetOrder(enemy), stepOrder));
    }

    public void CheckIfBombed(Enemy enemy, int stepOrder)
    {
        Bomb bomb = Array.Find(bombArr, bo => bo.x == enemy.x && bo.y == enemy.y);
        if (bomb != null && !bomb.inactive && bomb.affectsEnemy)
        {
            if (bomb.numOfUses > 0)
                bomb.numOfUses--;
            if (bomb.numOfUses == 0)
                bomb.inactive = true;

            enemy.inactive = true;

            bombedEnemiesList.Add(new Triple<int, int, Bomb>(GetOrder(enemy), stepOrder, bomb));

        }
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


    public void AnimateGameObject(Entity entity, Entity.Direction direction, int stepOrder)
    {
        int order = GetOrder(entity);
        if (entity is Enemy && ((Enemy)entity).inactive && !bombedEnemiesList.Exists(bo => bo.first == order && bo.second == stepOrder))
        {
            Enemy enemy = (Enemy)entity;

            List<bool> step = animationComplete[order];
            for (int j = stepOrder; j < enemy.stepsPerMove; j++)
                step[j] = true;

        }
        else
        {

            Vector3 endPosition = new Vector3(originX + entity.x * horizontalSpace, originY + entity.y * -verticalSpace, entity.transform.position.z);
            StartCoroutine(MoveOverSeconds(entity, endPosition, direction, ANIMATION_DELAY, order, stepOrder));
        }
    }

    private bool isLatestAnimationComplete(int order, int stepOrder)
    {

        int currentOrder = order;
        int currentStepOrder = stepOrder;
        while (currentOrder > 0)
        {
            if (currentStepOrder == 0)
            {
                currentOrder--;
                currentStepOrder = animationComplete[currentOrder].Count - 1;

            }
            else
            {
                currentStepOrder--;
            }
            if (!animationComplete[currentOrder][currentStepOrder])
            {
                return false;
            }

        }

        return true;
    }

    private IEnumerator MoveOverSeconds(Entity objectToMove, Vector3 end, Entity.Direction direction, float seconds, int order, int stepOrder)
    {


        yield return new WaitUntil(() => isLatestAnimationComplete(order, stepOrder));

        if (!areAllAnimationsComplete())
        {
            //Update sprite to face the proper direction
            Look(objectToMove, direction);

            Pair<int, int> blockedEnemyInfo = blockedEnemiesList.Find(b => b.first == order && b.second == stepOrder);

            //Logic to move the object
            float elapsedTime = 0;
            Vector3 startingPos = objectToMove.transform.position;
            if (blockedEnemyInfo == null)
            {
                while (elapsedTime < seconds)
                {
                    objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
                    elapsedTime += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
            }
            objectToMove.transform.position = end;

            //this animation is done
            animationComplete[order][stepOrder] = true;

            //Check for game end
            if (gameEndInfo != null && gameEndInfo.first == order && gameEndInfo.second == stepOrder)
            {
                updateAnimationCompleteListWith(true);
            }

            //update view of bombed enemies and bomb
            Triple<int, int, Bomb> bombedEnemyInfo = bombedEnemiesList.Find(bo => bo.first == order && bo.second == stepOrder);
            if (bombedEnemyInfo != null && bombedEnemyInfo.third != null)
            {
                Color color = objectToMove.GetComponent<SpriteRenderer>().material.color;
                color.a = 0.5f;

                objectToMove.GetComponent<SpriteRenderer>().material.color = color;
                Look(objectToMove, Entity.Direction.DOWN);

                Bomb bomb = bombedEnemyInfo.third;
                if (bomb.inactive)
                {
                    Color color2 = bomb.GetComponent<SpriteRenderer>().material.color;
                    color2.a = 0.0f;

                    bomb.GetComponent<SpriteRenderer>().material.color = color2;
                }
            }

            //update view of dozed enemies
            Triple<int, int, Enemy> dozedEnemyInfo = dozedEnemiesList.Find(d => d.first == order && d.second == stepOrder);
            if (dozedEnemyInfo != null && dozedEnemyInfo.third != null)
            {
                Enemy dozedEnemy = dozedEnemyInfo.third;

                Color color = dozedEnemy.GetComponent<SpriteRenderer>().material.color;
                color.a = 0.5f;
                dozedEnemy.GetComponent<SpriteRenderer>().material.color = color;

                Look(dozedEnemy, Entity.Direction.DOWN);

            }
        }



    }



}
