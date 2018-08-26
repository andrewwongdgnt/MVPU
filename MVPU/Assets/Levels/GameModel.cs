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

    public AudioSource currentLevelAudioSource;
    public AudioSource pauseAudioSource;
    public AudioClip pauseMusicClip;

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
        get
        {
            return _player;
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

    private Wall[] _wallArr;
    public Wall[] wallArr
    {
        set
        {
            _wallArr = value;
            Array.ForEach(_wallArr, w =>
            {
                w.gameModel = this;
            });
        }
    }

    private LevelUtil.LevelID _currentLevelId;
    public LevelUtil.LevelID currentLevelId
    {

        set
        {
            _currentLevelId = value;
        }
    }

    private AudioClip _currentLevelMusic;
    public AudioClip currentLevelMusic
    {

        set
        {
            _currentLevelMusic = value;
        }
    }


    public void Undo(bool removeLastState)
    {
        endGameMenu.HideEndGameMenu();
        if (UserInteractionAllowed())
        {
            HistoryState historyState = undoManager.Undo();
            if (removeLastState)
                undoManager.RemoveLastState();
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
        for (int i = 0; i < _wallArr.Length; i++)
        {
            _wallArr[i].RestoreState(historyState.wallArrState[i]);
        }
        SetViewForEntities();
    }

    private void AddToHistory()
    {
        scoringModel.AddMove();
        undoManager.AddToHistory(_player, _goal, _enemyArr, _bombArr, _keyArr, _wallArr);
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

    public void PlayMusic()
    {
        if (Time.timeScale > 0)
        {
            pauseAudioSource.Stop();
           currentLevelAudioSource.UnPause();
            if (!currentLevelAudioSource.isPlaying)
            AudioUtil.PlayMusic(currentLevelAudioSource, _currentLevelMusic);
        }
        else
        {
            currentLevelAudioSource.Pause();
           pauseAudioSource.UnPause();
            if (!pauseAudioSource.isPlaying)
                AudioUtil.PlayMusic(pauseAudioSource, pauseMusicClip);
        }
    }

    private IEnumerator setActionAllowedFromTutorialWithDelay(TutorialAction.Action actionAllowed)
    {
        actionAllowedFromTutorial = TutorialAction.Action.NONE;
        yield return 30;
        actionAllowedFromTutorial = actionAllowed;

    }


    private List<List<bool>> animationComplete = new List<List<bool>>();
    private List<Triple<int, int, Enemy>> fallenEnemiesList = new List<Triple<int, int, Enemy>>();
    //if Entity is null, that means the player has won
    private Triple<int, int, Entity> gameEndInfo;

    private List<Pair<int, int>> blockedEnemiesList = new List<Pair<int, int>>();
    private List<Quadruple<int, int, Bomb, Bomb.Animation>> bombList = new List<Quadruple<int, int, Bomb, Bomb.Animation>>();
    private List<Quadruple<int, int, Key, Key.Animation>> keyList = new List<Quadruple<int, int, Key, Key.Animation>>();
    private List<Quadruple<int, int, Wall, Wall.Animation>> wallList = new List<Quadruple<int, int, Wall, Wall.Animation>>();


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
        undoManager.AddInitialState(_player, _goal, _enemyArr, _bombArr, _keyArr, _wallArr);

        AdvanceTutorial();

        //Play Music
        PlayMusic();

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
        Array.ForEach(_wallArr, w =>
        {
            SetViewForWall(w);
        });
    }

    private Vector3 GetPositionForEntity(Entity entity)
    {
        float newX = origin.x + entity.x * distance.x + entity.y * distance.x;
        float newY = origin.y + entity.y * -distance.y + entity.x * distance.y;
        float newZ = newY + (entity is Player ? 0 : entity is Enemy ? -0.0001f : -0.002f); //Player is behind enemies, enemies are behind everything else.
        return new Vector3(newX, newY, newZ);
    }

    private void SetViewForPlayer(Player player)
    {
        Look(player, player.facingDirection);
        player.transform.position = GetPositionForEntity(player);
    }
    private void SetViewForEnemy(Enemy enemy, string animationName= null)
    {
        enemy.transform.position = GetPositionForEntity(enemy);
        Look(enemy, enemy.facingDirection);
        if (animationName != null)
        {
            enemy.StartDieAnimation(animationName);
        }
        else
        {
            enemy.StopDieAnimation();
        }
        SpriteRenderer[] sprites = enemy.GetComponentsInChildren<SpriteRenderer>();
        Array.ForEach(sprites, s =>
        {
            Color color = s.material.color;
            color.a = enemy.inactive && animationName == null ? 0f : 1f;

            s.material.color = color;
        });
    }

    private void SetViewForBomb(Bomb bomb, Entity.Direction direction = Entity.Direction.RIGHT, Bomb.Animation bombAnimation = Bomb.Animation.None)
    {
        bomb.transform.position = GetPositionForEntity(bomb);
        Look(bomb, bomb.facingDirection);
        if (bombAnimation != Bomb.Animation.None)
        {
            FaceHorizontally(bomb, direction);
            if (bombAnimation == Bomb.Animation.Explode)
                bomb.StartAttackAnimation(); 
        }
        else
        {
            bomb.StopAttackAnimation();
        }
        SpriteRenderer[] sprites = bomb.GetComponentsInChildren<SpriteRenderer>();
        Array.ForEach(sprites, s =>
        {
            Color color = s.material.color;
            color.a = bomb.inactive && bombAnimation == Bomb.Animation.None ? 0.0f : 1f;

            s.material.color = color;
        });

    }
    private void SetViewForKey(Key key, Key.Animation keyAnimation = Key.Animation.None)
    {

        key.transform.position = GetPositionForEntity(key);
        bool animate = keyAnimation != Key.Animation.None;
        if (animate)
        {
            if (keyAnimation == Key.Animation.Consumed)
                key.StartConsumedAnimation();
            else if (!key.hold)
                key.StartUsedAnimation();
        }
        else if (key.numOfUses > 0) { 
            key.StopConsumedAnimation();
        }
        if (key.hold)
        {
            if (keyAnimation == Key.Animation.On || key.on)
                key.StartOnAnimation(animate);
            else if (keyAnimation == Key.Animation.Off || !key.on)
                key.StopOnAnimation(animate);
        }
        SpriteRenderer[] sprites = key.GetComponentsInChildren<SpriteRenderer>();
        Array.ForEach(sprites, s =>
        {
            Color color = s.material.color;
            color.a = key.consumed && !animate ? 0f : 1f;

            s.material.color = color;
        });        
    }

    private void SetViewForWall(Wall wall, Wall.Animation wallAnimation = Wall.Animation.None)
    {
        bool animate = wallAnimation != Wall.Animation.None;
        if (animate)
        {
            if (wallAnimation == Wall.Animation.Retract)
                wall.StartRetract(animate);
            else
                wall.StopRetract(animate);
        }
        else
        {
            if (!wall.retracted)
                wall.StartRetract(animate);
            else
                wall.StopRetract(animate);
        }
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
        PlayMusic();
        if (Time.timeScale > 0 && !TutorialAction.isNoAction(actionAllowedFromTutorial))
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
               // || SwipeManager.IsSwipingUp()
                || SwipeManager.IsSwipingDownLeft()
               // || SwipeManager.IsSwipingLeft()
                || SwipeManager.IsSwipingDownRight()
              //  || SwipeManager.IsSwipingDown()
                || SwipeManager.IsSwipingUpRight()
               // || SwipeManager.IsSwipingRight()
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
                    fallenEnemiesList.Clear();
                    blockedEnemiesList.Clear();
                    bombList.Clear();
                    keyList.Clear();
                    wallList.Clear();

                    bool unblocked = false;
                    if (actionAllowedFromTutorial == TutorialAction.Action.SWIPE || actionAllowedFromTutorial == TutorialAction.Action.ALL)
                    {
                        if (Input.GetAxis("Vertical") > 0 || SwipeManager.IsSwipingUpLeft() )//|| SwipeManager.IsSwipingUp())
                        {
                            unblocked = _player.Do_MoveUp();
                            AdvanceTutorial(true);
                        }
                        else if (Input.GetAxis("Horizontal") < 0 || SwipeManager.IsSwipingDownLeft())// || SwipeManager.IsSwipingLeft())
                        {
                            unblocked = _player.Do_MoveLeft();
                            AdvanceTutorial(true);
                        }
                        else if (Input.GetAxis("Vertical") < 0 || SwipeManager.IsSwipingDownRight() )//|| SwipeManager.IsSwipingDown())
                        {
                            unblocked = _player.Do_MoveDown();
                            AdvanceTutorial(true);
                        }
                        else if (Input.GetAxis("Horizontal") > 0 || SwipeManager.IsSwipingUpRight() )//|| SwipeManager.IsSwipingRight())
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
                SaveStateUtil.SaveLevel(_currentLevelId, scoringModel.numberOfMoves);
                _player.StartWinAnimation();
                _goal.StartWinAnimation();
                FaceHorizontally(_goal.entity, _player.facingDirection == Entity.Direction.LEFT || _player.facingDirection == Entity.Direction.UP ? Entity.Direction.RIGHT : Entity.Direction.LEFT);
            }
            else
            {
                Debug.Log(_currentLevelId + ": Game Over");

                if (attacker!=null)
                {
                    _player.StartDieAnimation(attacker.GetPlayerLoseAnimationName(), attacker is Enemy);
                    attacker.StartAttackAnimation();
                    Entity.Direction attackerDir =  _player.facingDirection == Entity.Direction.LEFT || _player.facingDirection == Entity.Direction.UP ? Entity.Direction.RIGHT : Entity.Direction.LEFT;
                    if (!(attacker is Enemy))
                    {
                        if (attackerDir == Entity.Direction.RIGHT)
                            attackerDir = Entity.Direction.LEFT;
                        else
                            attackerDir = Entity.Direction.RIGHT;
                    }
                    FaceHorizontally(attacker.entity, attackerDir);
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


    private void Look(Entity entity, Entity.Direction direction)
    {

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

            fallenEnemiesList.Add(new Triple<int, int, Enemy>(GetOrder(enemy), stepOrder, dozedEnemy));

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

            bombList.Add(new Quadruple<int, int, Bomb, Bomb.Animation>(GetOrder(enemy), stepOrder, bomb, Bomb.Animation.Explode));

        }
    }

    public void CheckForKey(Entity entity, Entity.Direction direction, int stepOrder, Entity.Direction checkingForHoldingKeysDirection = Entity.Direction.NONE)
    {

        if (direction == Entity.Direction.NONE)
            return;

        int xModifier = 0;
        int yModifier = 0;
        if (checkingForHoldingKeysDirection == Entity.Direction.NONE)
        {
            Entity.Direction direction2 = Entity.Direction.DOWN;
            if (direction == Entity.Direction.UP)
                direction2 = Entity.Direction.DOWN;
            if (direction == Entity.Direction.DOWN)
                direction2 = Entity.Direction.UP;
            if (direction == Entity.Direction.LEFT)
                direction2 = Entity.Direction.RIGHT;
            if (direction == Entity.Direction.RIGHT)
                direction2 = Entity.Direction.LEFT;

            CheckForKey(entity, direction, stepOrder, direction2);
        }
        else if (checkingForHoldingKeysDirection == Entity.Direction.UP)
            yModifier = -1;
        else if (checkingForHoldingKeysDirection == Entity.Direction.DOWN)
            yModifier = 1;
        else if (checkingForHoldingKeysDirection == Entity.Direction.LEFT)
            xModifier = -1;
        else if (checkingForHoldingKeysDirection == Entity.Direction.RIGHT)
            xModifier = 1;

        bool checkingForHoldingKeys = checkingForHoldingKeysDirection != Entity.Direction.NONE;
        Key key = Array.Find(_keyArr, k => k.x == entity.x+ xModifier && k.y == entity.y+ yModifier);
        if (key!=null && !key.consumed
            && ((checkingForHoldingKeys && key.hold) || !checkingForHoldingKeys) 
            && (entity is Player || (entity is Enemy && key.usableByEnemy))){

            IWalker walker= (IWalker) entity;
            
            if (key.numOfUses > 0)
                key.numOfUses--;
            if (key.numOfUses == 0)
                key.consumed = true;
            if (key.hold)
                key.on = !key.on;

            Key.Animation keyAnimation;
            if (key.consumed)
                keyAnimation= Key.Animation.Consumed;
            else if (key.hold && key.on)
                keyAnimation = Key.Animation.On;
            else if (key.hold && !key.on)
                keyAnimation = Key.Animation.Off;
            else
                keyAnimation = Key.Animation.Used;

            keyList.Add(new Quadruple<int, int, Key, Key.Animation>(GetOrder(walker), stepOrder, key, keyAnimation));

            Array.ForEach(_wallArr, wall => {


                for (int lIndex = 0; lIndex<wall.keyRelationshipIndexArr.Length; lIndex++)
                {
                    Wall.KeyRelationship keyRelationship = wall.keyRelationshipIndexArr[lIndex];                    

                    if (Array.Exists(keyRelationship.arr, kIndex => key == _keyArr[kIndex]))
                    {
                        bool firstRetractedState = wall.retracted;
                        wall.locksOpened[lIndex] = !wall.locksOpened[lIndex];
                        bool secondRetractedState = wall.retracted;

                        //state changed
                        if (firstRetractedState != secondRetractedState)
                        {
                            Wall.Animation wallAnimation = wall.retracted ? Wall.Animation.DontRetract : Wall.Animation.Retract;
                            wallList.Add(new Quadruple<int, int, Wall, Wall.Animation>(GetOrder(walker), stepOrder, wall, wallAnimation));
                        }
                        break;
                    }
                }
               
            });
        }
    }

    public Entity.Direction CheckForUnretractedWall(int x, int y)
    {
        Wall wall = Array.Find(_wallArr, w => w.x == x && w.y == y);
        return wall != null && !wall.retracted ? wall.blocking : Entity.Direction.NONE;
    }

    public void AnimateGameObject(IWalker walker, Entity.Direction direction, int stepOrder)
    {
        int order = GetOrder(walker);
        if (walker is Enemy && ((Enemy)walker).inactive && !bombList.Exists(bo => bo.first == order && bo.second == stepOrder))
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
            Look(walker.entity, direction);

            walker.StartWalkAnimation();

            Pair<int, int> blockedEnemyInfo = blockedEnemiesList.Find(b => b.first == order && b.second == stepOrder);
            if (blockedEnemyInfo == null)
            {
  
                float speedMultiplier = SettingsUtil.GetEntitySpeedMultipler();

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
            else
            {

                //update view of bombed enemies and bomb
                List<Quadruple<int, int, Bomb, Bomb.Animation>> bombInfoList = bombList.FindAll(bo => bo.first == order && bo.second == stepOrder);
                bombInfoList.ForEach(bombInfo =>
                {
                    if (bombInfo != null && bombInfo.third != null)
                    {
                        if (walker is Enemy)
                        {
                            SetViewForEnemy((Enemy)walker, bombInfo.third.GetPlayerLoseAnimationName());
                        }
                        Entity.Direction bombDirection = direction == Entity.Direction.LEFT || direction == Entity.Direction.UP ? Entity.Direction.LEFT : Entity.Direction.RIGHT;

                        SetViewForBomb(bombInfo.third, bombDirection, Bomb.Animation.Explode);
                    }
                });

                //update view of dozed enemies
                List<Triple<int, int, Enemy>> dozedEnemyInfoList = fallenEnemiesList.FindAll(d => d.first == order && d.second == stepOrder);
                dozedEnemyInfoList.ForEach(dozedEnemyInfo =>
                {
                    if (dozedEnemyInfo != null && dozedEnemyInfo.third != null)
                    {
                        Enemy dozedEnemy = dozedEnemyInfo.third;
                        SetViewForEnemy(dozedEnemy, dozedEnemy.GetPlayerLoseAnimationName());

                    }
                });

                //update view of Keys
                List<Quadruple<int, int, Key, Key.Animation>> keyInfoList = keyList.FindAll(w => w.first == order && w.second == stepOrder);
                keyInfoList.ForEach(keyInfo =>
                {

                    if (keyInfo != null && keyInfo.third != null)
                    {

                        SetViewForKey(keyInfo.third, keyInfo.fourth);

                    }
                });

                //update view of Walls
                List<Quadruple<int, int, Wall, Wall.Animation>> wallInfoList = wallList.FindAll(w => w.first == order && w.second == stepOrder);
                wallInfoList.ForEach(wallInfo =>
                {

                    if (wallInfo != null && wallInfo.third != null)
                    {
                        SetViewForWall(wallInfo.third, wallInfo.fourth);

                    }
                });
            }

            
        }



    }



}
