using UnityEngine;
using System.Collections;
using System;

public class LevelModel : MonoBehaviour
{

    private static readonly float MAGIC_DISTANCE_NUMBER = 1.1275f; //Check the width of each isometric square in the flash file

    public LevelUtil.LevelID levelID;
    public Material mat;

    public Tutorial tutorial;

    public AudioClip musicClip;

    public PlayerInfo playerLocation;

    public GoalInfo goalLocation;

    public EnemyInfo[] enemyLocationArr;

    public BombInfo[] bombLocationArr;

    public KeyInfo[] keyLocationArr;

    public WallInfo[] wallLocationArr;

    [Tooltip("The base distance from one cell to another in the X direction")]
    public float baseDistanceX = MAGIC_DISTANCE_NUMBER;

    [Tooltip("X and Y coordinate of the top left cell in the level")]
    public Coordinate origin;

    // Use this for initialization
    void Start()
    {
        if (LevelUtil.levelToLoad == levelID)
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

            Player player = Instantiate(playerLocation.player);
            player.x = playerLocation.x;
            player.y = playerLocation.y;
            player.facingDirection = playerLocation.facingDirection == Entity.Direction.NONE ? Entity.Direction.RIGHT : playerLocation.facingDirection;
            player.gameObject.transform.localScale = new Vector3(scale, scale, scale);
            gameModel.player = player;

            Goal goal = Instantiate(goalLocation.goal);
            goal.x = goalLocation.x;
            goal.y = goalLocation.y;
            goal.gameObject.transform.localScale = new Vector3(scale, scale, scale);
            gameModel.goal = goal;

            Enemy[] enemyArr = new Enemy[enemyLocationArr.Length];
            for (int i = 0; i < enemyLocationArr.Length; i++)
            {
                Enemy enemy = Instantiate(enemyLocationArr[i].enemy);
                enemy.x = enemyLocationArr[i].x;
                enemy.y = enemyLocationArr[i].y;
                enemy.gameObject.transform.localScale = new Vector3(scale, scale, scale);
                enemy.facingDirection = enemyLocationArr.Length <= i || enemyLocationArr[i].facingDirection == Entity.Direction.NONE ? Entity.Direction.RIGHT : enemyLocationArr[i].facingDirection;

                enemyArr[i] = enemy;
            }
            gameModel.enemyArr = enemyArr;

            Bomb[] bombArr = new Bomb[bombLocationArr.Length];
            for (int i = 0; i < bombLocationArr.Length; i++)
            {
                Bomb bomb = Instantiate(bombLocationArr[i].bomb);
                bomb.x = bombLocationArr[i].x;
                bomb.y = bombLocationArr[i].y;
                bomb.gameObject.transform.localScale = new Vector3(scale, scale, scale);
                bomb.facingDirection = Entity.Direction.RIGHT;

                bombArr[i] = bomb;
            }
            gameModel.bombArr = bombArr;

            Key[] keyArr = new Key[keyLocationArr.Length];
            for (int i = 0; i < keyLocationArr.Length; i++)
            {
                Key key = Instantiate(keyLocationArr[i].key);
                key.x = keyLocationArr[i].x;
                key.y = keyLocationArr[i].y;
                key.gameObject.transform.localScale = new Vector3(scale, scale, scale);

                keyArr[i] = key;
            }
            gameModel.keyArr = keyArr;

            Wall[] wallArr = new Wall[wallLocationArr.Length];
            for (int i = 0; i < wallLocationArr.Length; i++)
            {
                Wall wall = Instantiate(wallLocationArr[i].wall);
                wall.x = wallLocationArr[i].x;
                wall.y = wallLocationArr[i].y;

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
        else
        {
            gameObject.SetActive(false);
        }
    }




}