using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Serialization;

public class LevelModel : MonoBehaviour
{

    private static readonly float MAGIC_DISTANCE_NUMBER = 1.1275f; //Check the width of each isometric square in the flash file

    public LevelUtil.LevelID levelID;
    public Material mat;

    public Tutorial.TutorialEnum tutorialEnum;
    public Tutorial tutorial { get; set; }

    public AudioClip musicClip;

    [FormerlySerializedAs("playerLocation")]
    public PlayerInfo playerInfo;

    [FormerlySerializedAs("goalLocation")]
    public GoalInfo goalInfo;

    [FormerlySerializedAs("enemyLocationArr")]
    public EnemyInfo[] enemyInfos;

    [FormerlySerializedAs("bombLocationArr")]
    public BombInfo[] bombInfos;

    [FormerlySerializedAs("keyLocationArr")]
    public KeyInfo[] keyInfos;

    [FormerlySerializedAs("wallLocationArr")]
    public WallInfo[] wallInfos;

    [Tooltip("The base distance from one cell to another in the X direction")]
    public float baseDistanceX = MAGIC_DISTANCE_NUMBER;

    [Tooltip("X and Y coordinate of the top left cell in the level")]
    public Coordinate origin;


    public void Init()
    {


        //Update skybox
        RenderSettings.skybox = mat;


        gameObject.SetActive(true);


        GameModel gameModel = GameObject.Find("Game").GetComponent<GameModel>();


        gameModel.levelScore = LevelUtil.LevelScoreMap[levelID];

        Coordinate distance = new Coordinate();
        distance.x = baseDistanceX;
        distance.y = distance.x / 2;
        gameModel.distance = distance;
        gameModel.origin = origin;

        gameModel.grid = LevelUtil.LevelGridMap[levelID];

        float scale = baseDistanceX / MAGIC_DISTANCE_NUMBER;

        Player player = Instantiate(playerInfo.player);
        player.x = playerInfo.x;
        player.y = playerInfo.y;
        player.facingDirection = playerInfo.facingDirection == Entity.Direction.NONE ? Entity.Direction.RIGHT : playerInfo.facingDirection;
        player.gameObject.transform.localScale = new Vector3(scale, scale, scale);
        gameModel.player = player;
        SetGameModelToAnimatorEvent(player.GetComponentInChildren<AnimatorEvent>(), gameModel, player);        

        Goal goal = Instantiate(goalInfo.goal);
        goal.x = goalInfo.x;
        goal.y = goalInfo.y;
        goal.gameObject.transform.localScale = new Vector3(scale, scale, scale);
        gameModel.goal = goal;
        SetGameModelToAnimatorEvent(goal.GetComponentInChildren<AnimatorEvent>(), gameModel, goal);

        Enemy[] enemyArr = new Enemy[enemyInfos.Length];
        for (int i = 0; i < enemyInfos.Length; i++)
        {
            Enemy enemy = Instantiate(enemyInfos[i].enemy);
            enemy.x = enemyInfos[i].x;
            enemy.y = enemyInfos[i].y;
            enemy.gameObject.transform.localScale = new Vector3(scale, scale, scale);
            enemy.facingDirection = enemyInfos.Length <= i || enemyInfos[i].facingDirection == Entity.Direction.NONE ? Entity.Direction.RIGHT : enemyInfos[i].facingDirection;
            SetGameModelToAnimatorEvent(enemy.GetComponentInChildren<AnimatorEvent>(), gameModel, enemy);

            enemyArr[i] = enemy;
        }
        gameModel.enemyArr = enemyArr;

        Bomb[] bombArr = new Bomb[bombInfos.Length];
        for (int i = 0; i < bombInfos.Length; i++)
        {
            Bomb bomb = Instantiate(bombInfos[i].bomb);
            bomb.x = bombInfos[i].x;
            bomb.y = bombInfos[i].y;
            bomb.gameObject.transform.localScale = new Vector3(scale, scale, scale);
            bomb.facingDirection = Entity.Direction.RIGHT;
            SetGameModelToAnimatorEvent(bomb.GetComponentInChildren<AnimatorEvent>(), gameModel, bomb);

            bombArr[i] = bomb;
        }
        gameModel.bombArr = bombArr;

        Key[] keyArr = new Key[keyInfos.Length];
        for (int i = 0; i < keyInfos.Length; i++)
        {
            Key key = Instantiate(keyInfos[i].key);
            key.x = keyInfos[i].x;
            key.y = keyInfos[i].y;
            key.gameObject.transform.localScale = new Vector3(scale, scale, scale);

            keyArr[i] = key;
        }
        gameModel.keyArr = keyArr;

        Wall[] wallArr = new Wall[wallInfos.Length];
        for (int i = 0; i < wallInfos.Length; i++)
        {
            Wall wall = wallInfos[i].wall;
            wall.x = wallInfos[i].x;
            wall.y = wallInfos[i].y;

            wallArr[i] = wall;
        }
        gameModel.wallArr = wallArr;

        gameModel.currentLevelId = levelID;

        gameModel.currentLevelMusic = musicClip;


        //Disable entities not related to this level
        GameObject[] tutorialEntities = GameObject.FindGameObjectsWithTag("TutorialEntity");
        Array.ForEach(tutorialEntities, tEnt =>
        {
            if (tEnt.GetComponent<Tutorial>() != tutorial)
            {
                tEnt.SetActive(false);
            }
        });

        bool tutorialObjectsOn = false;
        if (SettingsUtil.IsTutorialOn())
        {
            if (tutorial != null)
            {
                tutorialObjectsOn = true;
                tutorial.Init(LevelUtil.TutorialContent.ContainsKey(levelID) ? LevelUtil.TutorialContent[levelID] : null);
            }
            gameModel.tutorial = tutorial;
        }

        GameObject[] tutorialGameObjects = GameObject.FindGameObjectsWithTag("Tutorial");
        Array.ForEach(tutorialGameObjects, tGo =>
        {
            tGo.SetActive(tutorialObjectsOn);
        });
        GameObject[] tutorialEntityGameObjects = GameObject.FindGameObjectsWithTag("TutorialEntity");
        Array.ForEach(tutorialEntityGameObjects, teGo =>
        {
            teGo.SetActive(tutorialObjectsOn);
        });

        gameModel.Commence();
        

    }

    private void  SetGameModelToAnimatorEvent(AnimatorEvent animatorEvent, GameModel gameModel, Entity entity)
    {
        if (animatorEvent != null)
        {
            animatorEvent.gameModel = gameModel;
        
            if (entity is IAttacker)
            {
                animatorEvent.attacker = (IAttacker)entity;
            }
            if (entity is IWalker)
            {
                animatorEvent.walker = (IWalker)entity;
            }
            if (entity is IMortal)
            {
                animatorEvent.mortal = (IMortal)entity;
            }
        }
    }
}