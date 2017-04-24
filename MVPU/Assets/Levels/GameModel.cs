using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameModel : MonoBehaviour
{
    private readonly float ANIMATION_SPEED = 1.4f;
    private readonly float DOUBLE_TAP_DELAY = 0.3f;

    private readonly float LOSE_ANIMATION_DELAY_IN_SECONDS = 1f;
    private readonly float WIN_ANIMATION_DELAY_IN_SECONDS = 1f;

    private ScoringModel scoringModel;
    private UndoManager undoManager;

    private bool endGameAnimationPlaying;
    private bool allowShowEndGameMenu;

    public EndGameMenu endGameMenu;

    public Text scoreGuiText;
    public Text bestScoreGuiText;

    public TutorialAction.Action actionAllowedFromTutorial
    {
        get; set;
    }

    public string[] tutorialStringArr
    {
        get; set;
    }
    public LevelScore levelScore
    {
        get; set;
    }
    public Coordinate distance
    {
        get; set;
    }

    public Coordinate origin
    {
        get; set;
    }

    public Cell[,] grid
    {
        get; set;
    }

    private Tutorial _tutorial;
    public Tutorial tutorial
    {
        set
        {
            _tutorial = value;
        }
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


    private Key[] _keyArr;
    public Key[] keyArr
    {
        set
        {
            _keyArr = value;
            Array.ForEach(_keyArr, k =>
            {
                k.gameModel = this;
            });
        }
    }

    private LevelManager.LevelID _currentLevelId;
    public LevelManager.LevelID currentLevelId
    {

        set
        {
            _currentLevelId = value;
        }
    }


    public void Undo(bool removeLastState)
    {
        endGameMenu.HideEndGameMenu();
        if (UserInteractionAllowed())
        {
            HistoryState historyState = undoManager.Undo(removeLastState);
            if (historyState == null)
                historyState = undoManager.initialHistoryState;

            scoringModel.SubtractMove();

            RestoreStateToEntities(historyState);
        }
    }

    public void Redo()
    {
        endGameMenu.HideEndGameMenu();
        if (UserInteractionAllowed())
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
        _player.StopDieAnimation();
        _player.StopWinAnimation();
        _goal.RestoreState(historyState.goalState);
        _goal.StopWinAnimation();
        for (int i = 0; i < _enemyArr.Length; i++)
        {
            _enemyArr[i].RestoreState(historyState.enemyArrState[i]);
            _enemyArr[i].StopAttackAnimation();
        }
        for (int i = 0; i < _bombArr.Length; i++)
        {
            _bombArr[i].RestoreState(historyState.bombArrState[i]);
            _bombArr[i].StopAttackAnimation();
        }
        for (int i = 0; i < _keyArr.Length; i++)
        {
            _keyArr[i].RestoreState(historyState.keyArrState[i]);
        }
        SetViewForEntities();
    }

    private void AddToHistory()
    {
        scoringModel.AddMove();
        undoManager.AddToHistory(_player, _goal, _enemyArr, _bombArr, _keyArr);
    }

    public void UpdateScoreText(int score, int bestScore)
    {
        if (scoreGuiText != null)
        {
            scoreGuiText.text = score.ToString();
        }
        if (bestScoreGuiText != null)
        {
            bestScoreGuiText.text = bestScore.ToString();
        }
    }


    public void AdvanceTutorial(Boolean forceAdvance = false)
    {
        TutorialAction.Action actionAllowed;
        if (_tutorial != null)
            actionAllowed = _tutorial.AdvanceTutorial(forceAdvance);
        else
            actionAllowed = TutorialAction.Action.ALL;

        StartCoroutine(setActionAllowedFromTutorialWithDelay(actionAllowed));
    }

    private IEnumerator setActionAllowedFromTutorialWithDelay(TutorialAction.Action actionAllowed)
    {
        actionAllowedFromTutorial = TutorialAction.Action.NONE;
        yield return 30;
        actionAllowedFromTutorial = actionAllowed;

    }


    private List<List<bool>> animationComplete = new List<List<bool>>();
    private List<Triple<int, int, Enemy>> dozedEnemiesList = new List<Triple<int, int, Enemy>>();
    //if Entity is null, that means the player has won
    private Triple<int, int, Entity> gameEndInfo;

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

        scoringModel = new ScoringModel(levelScore, this);
        undoManager = new UndoManager();
        undoManager.AddInitialState(_player, _goal, _enemyArr, _bombArr, _keyArr);

        AdvanceTutorial();

    }


    private void SetViewForEntities()
    {
        SetViewForPlayer(_player);
        _goal.transform.position = GetPositionForEntity(_goal);

        Array.ForEach(_enemyArr, en =>
        {
            SetViewForEnemy(en);
        });
        Array.ForEach(_bombArr, bo =>
        {
            SetViewForBomb(bo);
        });
        Array.ForEach(_keyArr, k =>
        {
            SetViewForKey(k);
        });
    }

    private Vector3 GetPositionForEntity(Entity entity)
    {
        float newX = origin.x + entity.x * distance.x + entity.y * distance.x;
        float newY = origin.y + entity.y * -distance.y + entity.x * distance.y;
        float newZ = newY;
        return new Vector3(newX, newY, newZ);
    }

    private void SetViewForPlayer(Player player)
    {
        Look(player, player.facingDirection);
        player.transform.position = GetPositionForEntity(player);
    }
    private void SetViewForEnemy(Enemy enemy, bool dozeAnimation=false)
    {
        enemy.transform.position = GetPositionForEntity(enemy);
        Look(enemy, enemy.facingDirection);
        if (dozeAnimation)
            enemy.StartDozedAnimation();
        else
            enemy.StopDozedAnimation();
        SpriteRenderer[] sprites = enemy.GetComponentsInChildren<SpriteRenderer>();
        Array.ForEach(sprites, s =>
        {
            Color color = s.material.color;
            color.a = enemy.inactive && !dozeAnimation ? 0f : 1f;

            s.material.color = color;
        });
    }

    private void SetViewForBomb(Bomb bomb)
    {
        bomb.transform.position = GetPositionForEntity(bomb);
        Color color2 = bomb.GetComponent<SpriteRenderer>().material.color;
        color2.a = bomb.inactive ? 0.0f : 1f;

        bomb.GetComponent<SpriteRenderer>().material.color = color2;
    }
    private void SetViewForKey(Key key)
    {

        key.transform.position = GetPositionForEntity(key);
        SpriteRenderer[] sprites = key.GetComponentsInChildren<SpriteRenderer>();
        Array.ForEach(sprites, s =>
        {
            Color color = s.material.color;
            color.a = key.consumed ? 0f : 1f;

            s.material.color = color;
        });
        
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
        if (Time.timeScale > 0 && actionAllowedFromTutorial != TutorialAction.Action.NONE)
        {
            bool userInteractionAllowed = UserInteractionAllowed();

            //Check for double tap
            if (userInteractionAllowed)
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

            if (userInteractionAllowed &&
                (Input.GetAxis("Vertical") > 0
                || Input.GetAxis("Horizontal") < 0
                || Input.GetAxis("Vertical") < 0
                || Input.GetAxis("Horizontal") > 0
                || Input.GetButtonDown("Cancel")
                || Input.GetKeyDown(KeyCode.Z)
                || Input.GetKeyDown(KeyCode.Escape)
                || Input.GetKeyDown(KeyCode.Y)
                || SwipeManager.IsSwipingUpLeft()
                || SwipeManager.IsSwipingUp()
                || SwipeManager.IsSwipingDownLeft()
                || SwipeManager.IsSwipingLeft()
                || SwipeManager.IsSwipingDownRight()
                || SwipeManager.IsSwipingDown()
                || SwipeManager.IsSwipingUpRight()
                || SwipeManager.IsSwipingRight()
                || doubleTapOccurred)
                )
            {
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Z))
                    Undo(false);
                else if (Input.GetKeyDown(KeyCode.Y))
                    Redo();
                else
                {
                    UpdateAnimationCompleteListWith(false);
                    dozedEnemiesList.Clear();
                    blockedEnemiesList.Clear();
                    bombedEnemiesList.Clear();

                    bool unblocked = false;
                    if (actionAllowedFromTutorial == TutorialAction.Action.SWIPE || actionAllowedFromTutorial == TutorialAction.Action.ALL)
                    {
                        if (Input.GetAxis("Vertical") > 0 || SwipeManager.IsSwipingUpLeft() || SwipeManager.IsSwipingUp())
                        {
                            unblocked = _player.Do_MoveUp();
                            AdvanceTutorial(true);
                        }
                        else if (Input.GetAxis("Horizontal") < 0 || SwipeManager.IsSwipingDownLeft() || SwipeManager.IsSwipingLeft())
                        {
                            unblocked = _player.Do_MoveLeft();
                            AdvanceTutorial(true);
                        }
                        else if (Input.GetAxis("Vertical") < 0 || SwipeManager.IsSwipingDownRight() || SwipeManager.IsSwipingDown())
                        {
                            unblocked = _player.Do_MoveDown();
                            AdvanceTutorial(true);
                        }
                        else if (Input.GetAxis("Horizontal") > 0 || SwipeManager.IsSwipingUpRight() || SwipeManager.IsSwipingRight())
                        {
                            unblocked = _player.Do_MoveRight();
                            AdvanceTutorial(true);
                        }
                    }
                    if (actionAllowedFromTutorial == TutorialAction.Action.TAP || actionAllowedFromTutorial == TutorialAction.Action.ALL)
                    {
                        if (Input.GetButtonDown("Cancel") || doubleTapOccurred)
                        {
                            doubleTapOccurred = false;
                            unblocked = _player.Do_Nothing();
                            AdvanceTutorial(true);
                        }
                    }

                    if (unblocked)
                    {
                        Debug.Log("Player new position (" + _player.x + ", " + _player.y + ")");

                        for (int i = 0; i < _enemyArr.Length; i++)
                        {
                            Enemy enemy = _enemyArr[i];
                            enemy.Do_React();
                            Debug.Log(enemy + " new position (" + enemy.x + ", " + enemy.y + ")");
                        }
                        AddToHistory();
                    }
                    else
                    {

                        UpdateAnimationCompleteListWith(true);
                        Debug.Log("Player is blocked so no reaction from Enemies");
                    }
                }

            }
        }

    }

    private void HandleEndGamePhase()
    {
        if (gameEndInfo != null)
        {
            bool winning = gameEndInfo.third==null;
            IAttacker attacker = gameEndInfo.third is IAttacker ? (IAttacker)gameEndInfo.third : null;
            gameEndInfo = null;
           endGameAnimationPlaying = true;
            allowShowEndGameMenu = true;
            if (winning)
            {
                Debug.Log(_currentLevelId + ": Game win with " + scoringModel.numberOfMoves + "/" + scoringModel.minOfMoves + " moves. \nMedal: " + scoringModel.GetResult());
                SaveStateManager.SaveLevel(_currentLevelId, scoringModel.numberOfMoves);
                _player.StartWinAnimation();
                _goal.StartWinAnimation();
                FaceHorizontally(_goal.entity, _player.facingDirection == Entity.Direction.LEFT || _player.facingDirection == Entity.Direction.UP ? Entity.Direction.RIGHT : Entity.Direction.LEFT);
            }
            else
            {
                Debug.Log(_currentLevelId + ": Game Over");                
                if (attacker!=null)
                {
                    _player.StartDieAnimation(true);
                    attacker.StartAttackAnimation();
                    FaceHorizontally(attacker.entity, _player.facingDirection == Entity.Direction.LEFT || _player.facingDirection == Entity.Direction.UP ? Entity.Direction.RIGHT : Entity.Direction.LEFT);
                } else
                {
                    _player.StartDieAnimation();
                }
            }
        }
    }

    public void ShowLoseMenu()
    {
        if (allowShowEndGameMenu)
        {
            StartCoroutine(ShowLoseMenuWithDelay());
            allowShowEndGameMenu = false;
        }
    }

    public IEnumerator ShowLoseMenuWithDelay()
    {
        yield return new WaitForSeconds(LOSE_ANIMATION_DELAY_IN_SECONDS);
        endGameAnimationPlaying = false;
        endGameMenu.ShowLoseMenu(true);
    }

    public void ShowWinMenu()
    {
        if (allowShowEndGameMenu)
        {
            StartCoroutine(ShowWinMenuWithDelay());
            allowShowEndGameMenu = false;
        }
    }

    public IEnumerator ShowWinMenuWithDelay()
    {
        yield return new WaitForSeconds(WIN_ANIMATION_DELAY_IN_SECONDS);
        endGameAnimationPlaying = false;
        endGameMenu.ShowWinMenu(true, ScoringModel.GetResult(scoringModel.numberOfMoves, _currentLevelId));
    }


    private bool AllAnimationsComplete()
    {
        return animationComplete.All(list => list.All(b => b));
    }
    private bool UserInteractionAllowed()
    {
        return AllAnimationsComplete() && !endGameAnimationPlaying;
    }


    private void Look(IWalker walker, Entity.Direction direction)
    {

        Entity entity = walker.entity;

        if (direction == Entity.Direction.LEFT || direction == Entity.Direction.UP)
        {
            entity.transform.localScale = new Vector3(Math.Abs(entity.transform.localScale.x) * -1, entity.transform.localScale.y, entity.transform.localScale.z);
        }

        if (direction == Entity.Direction.RIGHT || direction == Entity.Direction.DOWN)
        {
            entity.transform.localScale = new Vector3(Math.Abs(entity.transform.localScale.x), entity.transform.localScale.y, entity.transform.localScale.z);
        }

    }

    private void FaceHorizontally(Entity entity, Entity.Direction direction)
    {

        if (direction == Entity.Direction.LEFT )
        {
            entity.transform.localScale = new Vector3(Math.Abs(entity.transform.localScale.x) * -1, entity.transform.localScale.y, entity.transform.localScale.z);
        }

        if (direction == Entity.Direction.RIGHT )
        {
            entity.transform.localScale = new Vector3(Math.Abs(entity.transform.localScale.x), entity.transform.localScale.y, entity.transform.localScale.z);
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

    private int GetOrder(IWalker walker)
    {
        int order = walker.entity == _player ? 0 : 1 + Array.FindIndex(_enemyArr, en => en == walker.entity);
        return order;
    }

    public void CheckForEndGame(IWalker walker, int stepOrder)
    {
        if (gameEndInfo == null)
        {
            if (_player.x == goalX && _player.y == goalY)
            {
                gameEndInfo = new Triple<int, int, Entity>(GetOrder(_player), stepOrder, null);

            }
            else
            {
                if (walker.entity == _player)
                {
                    Enemy theKiller = Array.Find(_enemyArr, en => en.x == _player.x && en.y == _player.y);
                    Bomb bomb = Array.Find(_bombArr, bo => bo.x == _player.x && bo.y == _player.y);
                    //Player walks into lose state
                    if (theKiller != null && !theKiller.inactive)
                    {
                        gameEndInfo = new Triple<int, int, Entity>(GetOrder(_player), stepOrder, theKiller);
                    }
                    else if (bomb != null && !bomb.inactive && bomb.affectsPlayer)
                    {
                        gameEndInfo = new Triple<int, int, Entity>(GetOrder(_player), stepOrder, bomb);
                    }
                }
                //Walker walks into player to cause lose state
                else if (_player.x == walker.entity.x && _player.y == walker.entity.y )
                    gameEndInfo = new Triple<int, int, Entity>(GetOrder(walker), stepOrder, walker.entity);

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

    public void AnimateGameObject(IWalker walker, Entity.Direction direction, int stepOrder)
    {
        int order = GetOrder(walker);
        if (walker is Enemy && ((Enemy)walker).inactive && !bombedEnemiesList.Exists(bo => bo.first == order && bo.second == stepOrder))
        {
            Enemy enemy = (Enemy)walker;

            List<bool> step = animationComplete[order];
            for (int j = stepOrder; j < enemy.stepsPerMove; j++)
                step[j] = true;

        }
        else
        {

            Vector3 endPosition = GetPositionForEntity(walker.entity);
            StartCoroutine(MoveWalker(walker, endPosition, direction, order, stepOrder));
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

    private IEnumerator MoveWalker(IWalker walker, Vector3 end, Entity.Direction direction, int order, int stepOrder)
    {


        yield return new WaitUntil(() => IsLatestAnimationComplete(order, stepOrder));

        if (!AllAnimationsComplete())
        {
            Entity entity = walker.entity;

            //Update sprite to face the proper direction
            Look(walker, direction);

            walker.StartWalkAnimation();

            Pair<int, int> blockedEnemyInfo = blockedEnemiesList.Find(b => b.first == order && b.second == stepOrder);
            if (blockedEnemyInfo == null)
            {
  
                float speedMultiplier = SettingsManager.GetEntitySpeedMultipler();

                while (entity.transform.position != end)
                {
                    entity.transform.position = Vector3.MoveTowards(entity.transform.position, end, ANIMATION_SPEED * speedMultiplier * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }
            }
            entity.transform.position = end;

            //this animation is done
            animationComplete[order][stepOrder] = true;

            walker.StopWalkAnimation();

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
                if (walker is Enemy)
                    SetViewForEnemy((Enemy)walker, true);

                Bomb bomb = bombedEnemyInfo.third;
                SetViewForBomb(bomb);
            }

            //update view of dozed enemies
            Triple<int, int, Enemy> dozedEnemyInfo = dozedEnemiesList.Find(d => d.first == order && d.second == stepOrder);
            if (dozedEnemyInfo != null && dozedEnemyInfo.third != null)
            {
                Enemy dozedEnemy = dozedEnemyInfo.third;
                SetViewForEnemy(dozedEnemy, true);

            }
        }



    }



}
