using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameModel : MonoBehaviour
{
    private readonly float ANIMATION_DELAY = 1f;
    private readonly float ANIMATION_SPEED = 1.4f;
    private readonly float DOUBLE_TAP_DELAY = 0.3f;

    private ScoringModel scoringModel;
    private UndoManager undoManager;

    public EndGameMenu endGameMenu;

    public LevelScore levelScore
    {
        get; set;
    }
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


    private Bomb[] _bombArr;
    public Bomb[] bombArr
    {
        set
        {
            _bombArr = value;
            Array.ForEach(_bombArr, bo =>
            {
                bo.gameModel = this;
            });
        }
    }

    private string _currentLevelId;
    public string currentLevelId
    {
    
        set
        {
            _currentLevelId = value;
        }
    }

    public void Undo()
    {
        endGameMenu.HideEndGameMenu();
        if (AreAllAnimationsComplete())
        {
            HistoryState historyState = undoManager.Undo();
            if (historyState == null)
                historyState = undoManager.initialHistoryState;

            scoringModel.SubtractMove();

            RestoreStateToEntities(historyState);
        }
    }

    public void Redo()
    {
        endGameMenu.HideEndGameMenu();
        if (AreAllAnimationsComplete())
        {
            HistoryState historyState = undoManager.Redo();
            if (historyState != null)
            {
                RestoreStateToEntities(historyState);
                scoringModel.AddMove();
            }
        }
    }

    private void RestoreStateToEntities(HistoryState historyState)
    {
        _player.RestoreState(historyState.playerState);
        _goal.RestoreState(historyState.goalState);
        for (int i = 0; i < _enemyArr.Length; i++)
        {
            _enemyArr[i].RestoreState(historyState.enemyArrState[i]);
        }
        for (int i = 0; i < _bombArr.Length; i++)
        {
            _bombArr[i].RestoreState(historyState.bombArrState[i]);
        }
        SetViewForEntities();
    }

    private void AddToHistory()
    {
        scoringModel.AddMove();
        undoManager.AddToHistory(_player, _goal, _enemyArr, _bombArr);
    }

    public Text scoreGuiText;
    public void UpdateScoreText(string text)
    {
        if (scoreGuiText != null)
        {
            scoreGuiText.text = text;
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
        SetViewForEntities();

        scoringModel = new ScoringModel(levelScore,this);
        undoManager = new UndoManager();
        undoManager.AddInitialState(_player, _goal, _enemyArr, _bombArr);
    }

    private void SetViewForEntities()
    {
        _player.transform.position = GetPositionForEntity(_player);
        _goal.transform.position = GetPositionForEntity(_goal);

        Array.ForEach(_enemyArr, en =>
        {
            en.transform.position = GetPositionForEntity(en);
            SetViewForEnemy(en);
        });
        Array.ForEach(_bombArr, bo =>
        {
            bo.transform.position = GetPositionForEntity(bo);
            SetViewForBomb(bo);
        });
    }

    private Vector3 GetPositionForEntity(Entity entity)
    {
        float newX = originX + entity.x * horizontalSpace + entity.y * horizontalSpace;
        float newY = originY + entity.y * -verticalSpace + entity.x * verticalSpace;
        float newZ = newY;
        return new Vector3(newX, newY, newZ);
    }

    private void SetViewForEnemy(Enemy enemy)
    {

        Color color = enemy.GetComponent<SpriteRenderer>().material.color;
        color.a = enemy.inactive ? 0.5f : 1f;

        enemy.GetComponent<SpriteRenderer>().material.color = color;
        Look(enemy, Entity.Direction.DOWN);
    }

    private void SetViewForBomb(Bomb bomb)
    {
        Color color2 = bomb.GetComponent<SpriteRenderer>().material.color;
        color2.a = bomb.inactive ? 0.0f : 1f;

        bomb.GetComponent<SpriteRenderer>().material.color = color2;
    }
    private void UpdateAnimationCompleteListWith(bool value)
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
    float touchDuration;
    Touch touch;
    bool doubleTapOccurred;

    private IEnumerator singleOrDouble()
    {
        yield return new WaitForSeconds(DOUBLE_TAP_DELAY);

        if (touch.tapCount == 2)
        {
            //this coroutine has been called twice. We should stop the next one here otherwise we get two double tap
            StopCoroutine("singleOrDouble");
            doubleTapOccurred = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            bool areAllAnimationsComplete = AreAllAnimationsComplete();

            //Check for double tap
            if (areAllAnimationsComplete)
            {
                if (Input.touchCount > 0)
                { //if there is any touch
                    touchDuration += Time.deltaTime;
                    touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Ended && touchDuration < 0.2f) //making sure it only check the touch once && it was a short touch/tap and not a dragging.
                        StartCoroutine("singleOrDouble");
                }
                else
                    touchDuration = 0.0f;
            }

            if (areAllAnimationsComplete &&
                (Input.GetAxis("Vertical") > 0
                || Input.GetAxis("Horizontal") < 0
                || Input.GetAxis("Vertical") < 0
                || Input.GetAxis("Horizontal") > 0
                || Input.GetButtonDown("Cancel")
                || Input.GetKeyDown(KeyCode.Z)
                || Input.GetKeyDown(KeyCode.Y)
                || SwipeManager.IsSwipingUpLeft()
                || SwipeManager.IsSwipingDownLeft()
                || SwipeManager.IsSwipingDownRight()
                || SwipeManager.IsSwipingUpRight()
                || doubleTapOccurred)
                )
            {
                if (Input.GetKeyDown(KeyCode.Z))
                    Undo();
                else if (Input.GetKeyDown(KeyCode.Y))
                    Redo();
                else
                {
                    UpdateAnimationCompleteListWith(false);
                    dozedEnemiesList.Clear();
                    blockedEnemiesList.Clear();
                    bombedEnemiesList.Clear();

                    if (Input.GetAxis("Vertical") > 0 || SwipeManager.IsSwipingUpLeft())
                    {
                        _player.Do_MoveUp();
                    }
                    else if (Input.GetAxis("Horizontal") < 0 || SwipeManager.IsSwipingDownLeft())
                    {
                        _player.Do_MoveLeft();
                    }
                    else if (Input.GetAxis("Vertical") < 0 || SwipeManager.IsSwipingDownRight())
                    {
                        _player.Do_MoveDown();
                    }
                    else if (Input.GetAxis("Horizontal") > 0 || SwipeManager.IsSwipingUpRight())
                    {
                        _player.Do_MoveRight();
                    }
                    else if (Input.GetButtonDown("Cancel") || doubleTapOccurred)
                    {
                        doubleTapOccurred = false;
                        _player.Do_Nothing();
                    }

                    Debug.Log("Player new position (" + _player.x + ", " + _player.y + ")");

                    for (int i = 0; i < _enemyArr.Length; i++)
                    {
                        Enemy enemy = _enemyArr[i];
                        enemy.Do_React();
                        Debug.Log(enemy + " new position (" + enemy.x + ", " + enemy.y + ")");
                    }
                    AddToHistory();
                }

            }
        }
        
    }

    private void HandleEndGamePhase()
    {
        if (gameEndInfo != null)
        {
            bool winning = gameEndInfo.third;
            gameEndInfo = null;
            if (winning)
            {
                Debug.Log(_currentLevelId + ": Game win with " + scoringModel.numberOfMoves + "/" + scoringModel.minOfMoves + " moves. \nMedal: " + scoringModel.GetResult());
                SaveStateManager.SaveLevel(_currentLevelId, scoringModel.numberOfMoves);
                endGameMenu.ShowWinMenu(true);
            }
            else
            {
                Debug.Log(_currentLevelId + ": Game Over");
                endGameMenu.ShowLoseMenu(true);
            }
        }
    }


    private bool AreAllAnimationsComplete()
    {
        return animationComplete.All(list => list.All(b => b));
    }


    private void Look(Entity entity, Entity.Direction direction)
    {




        //TODO: not the right way to do it but leave for testing
        //if (direction == Entity.Direction.UP)
        //    entity.transform.rotation = Quaternion.Euler(0, 0, 90);
        if (direction == Entity.Direction.LEFT || direction == Entity.Direction.UP)
        {
            entity.transform.localScale = new Vector3(Math.Abs(entity.transform.localScale.x) * -1, entity.transform.localScale.y, entity.transform.localScale.z);
        }
        //entity.transform.rotation = Quaternion.Euler(0, 0, 180);
        //if (direction == Entity.Direction.DOWN || direction == Entity.Direction.NONE)
        //    entity.transform.rotation = Quaternion.Euler(0, 0, 270);
        if (direction == Entity.Direction.RIGHT || direction == Entity.Direction.DOWN)
        {
            entity.transform.localScale = new Vector3(Math.Abs(entity.transform.localScale.x), entity.transform.localScale.y, entity.transform.localScale.z);
        }
        //entity.transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    private void UpdateAnimator(Entity entity, string key, bool value)
    {
        Animator animator = entity.animator;
        if (animator)
        {
            animator.SetBool(key, value);
        }
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
                    Bomb bomb = Array.Find(_bombArr, bo => bo.x == _player.x && bo.y == _player.y);
                    if ((theKiller != null && !theKiller.inactive) || (bomb != null && !bomb.inactive && bomb.affectsPlayer))
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
        Bomb bomb = Array.Find(_bombArr, bo => bo.x == enemy.x && bo.y == enemy.y);
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

            Vector3 endPosition = GetPositionForEntity(entity);
            StartCoroutine(MoveEntity(entity, endPosition, direction, order, stepOrder));
        }
    }

    private bool IsLatestAnimationComplete(int order, int stepOrder)
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

    private IEnumerator MoveEntity(Entity objectToMove, Vector3 end, Entity.Direction direction, int order, int stepOrder)
    {


        yield return new WaitUntil(() => IsLatestAnimationComplete(order, stepOrder));

        if (!AreAllAnimationsComplete())
        {
            //Update sprite to face the proper direction
            Look(objectToMove, direction);
            UpdateAnimator(objectToMove, "HorizontalWalk", true);

            Pair<int, int> blockedEnemyInfo = blockedEnemiesList.Find(b => b.first == order && b.second == stepOrder);
            if (blockedEnemyInfo == null)
            {
                //Logic to move the object
                /*float elapsedTime = 0;
                Vector3 startingPos = objectToMove.transform.position;

                while (elapsedTime < ANIMATION_DELAY)
                {
                    objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / ANIMATION_DELAY));
                    elapsedTime += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }*/

                while (objectToMove.transform.position != end)
                {
                    objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, ANIMATION_SPEED * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }
            }
            objectToMove.transform.position = end;

            //this animation is done
            animationComplete[order][stepOrder] = true;
            UpdateAnimator(objectToMove, "HorizontalWalk", false);

            //Check for game end
            if (gameEndInfo != null && gameEndInfo.first == order && gameEndInfo.second == stepOrder)
            {
                UpdateAnimationCompleteListWith(true);
                HandleEndGamePhase();
            }

            //update view of bombed enemies and bomb
            Triple<int, int, Bomb> bombedEnemyInfo = bombedEnemiesList.Find(bo => bo.first == order && bo.second == stepOrder);
            if (bombedEnemyInfo != null && bombedEnemyInfo.third != null)
            {
                if (objectToMove is Enemy)
                    SetViewForEnemy((Enemy)objectToMove);

                Bomb bomb = bombedEnemyInfo.third;
                SetViewForBomb(bomb);
            }

            //update view of dozed enemies
            Triple<int, int, Enemy> dozedEnemyInfo = dozedEnemiesList.Find(d => d.first == order && d.second == stepOrder);
            if (dozedEnemyInfo != null && dozedEnemyInfo.third != null)
            {
                Enemy dozedEnemy = dozedEnemyInfo.third;
                SetViewForEnemy(dozedEnemy);

            }
        }



    }



}
