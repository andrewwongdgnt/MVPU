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

    public Player player;
    public Location playerLocation;
    public Entity.Direction playerFacingDirection;

    public Goal goal;
    public Location goalLocation;

    public Enemy[] enemyArr;
    public Location[] enemyLocationArr;
    public Entity.Direction[] enemyFacingDirectionArr;

    public Bomb[] bombArr;
    public Location[] bombLocationArr;

    public Key[] keyArr;
    public Location[] keyLocationArr;

    public Wall[] wallArr;
    public Location[] wallLocationArr;

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

            //Disable entities not related to this level
            GameObject[] entities = GameObject.FindGameObjectsWithTag("Entity");
            Array.ForEach(entities, ent =>
            {

                if (ent.GetComponent<Player>() != player 
                && ent.GetComponent<Goal>() != goal 
                && !Array.Exists(enemyArr, en => en == ent.GetComponent<Enemy>()) 
                && !Array.Exists(bombArr, bo => bo == ent.GetComponent<Bomb>())
                && !Array.Exists(keyArr, k => k == ent.GetComponent<Key>())
                && !Array.Exists(wallArr, k => k == ent.GetComponent<Wall>()))
                {
                    ent.SetActive(false);
                }
            });

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

            player.x = playerLocation.x;
            player.y = playerLocation.y;
            player.facingDirection = playerFacingDirection==Entity.Direction.NONE ? Entity.Direction.RIGHT : playerFacingDirection ;
            player.gameObject.transform.localScale = new Vector3(scale,scale,scale);
            gameModel.player = player;

            goal.x = goalLocation.x;
            goal.y = goalLocation.y;
            goal.gameObject.transform.localScale = new Vector3(scale, scale, scale);
            gameModel.goal = goal;

            for (int i = 0; i < enemyLocationArr.Length; i++)
            {
                enemyArr[i].x = enemyLocationArr[i].x;
                enemyArr[i].y = enemyLocationArr[i].y;
                enemyArr[i].facingDirection = enemyFacingDirectionArr.Length <= i || enemyFacingDirectionArr[i] == Entity.Direction.NONE ? Entity.Direction.RIGHT : enemyFacingDirectionArr[i];

                enemyArr[i].gameObject.transform.localScale = new Vector3(scale, scale, scale);
            }
            gameModel.enemyArr = enemyArr;

            for (int i = 0; i < bombLocationArr.Length; i++)
            {
                bombArr[i].x = bombLocationArr[i].x;
                bombArr[i].y = bombLocationArr[i].y;
                bombArr[i].gameObject.transform.localScale = new Vector3(scale, scale, scale);
                bombArr[i].facingDirection = Entity.Direction.RIGHT;
            }
            gameModel.bombArr = bombArr;

            for (int i = 0; i < keyLocationArr.Length; i++)
            {
                keyArr[i].x = keyLocationArr[i].x;
                keyArr[i].y = keyLocationArr[i].y;
                keyArr[i].gameObject.transform.localScale = new Vector3(scale, scale, scale);
            }
            gameModel.keyArr = keyArr;

            for (int i = 0; i < wallLocationArr.Length; i++)
            {
                wallArr[i].x = wallLocationArr[i].x;
                wallArr[i].y = wallLocationArr[i].y;
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