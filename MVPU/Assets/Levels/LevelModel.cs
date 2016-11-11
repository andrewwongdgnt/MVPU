using UnityEngine;
using System.Collections;
using System;

public abstract class LevelModel : MonoBehaviour
{

    public GameModel gameModel;

    public Player player;
    public Point playerLocation;

    public Goal goal;
    public Point goalLocation;

    public Enemy[] enemyArr;
    public Point[] enemyLocationArr;

    public Bomb[] bombArr;
    public Point[] bombLocationArr;

    public float verticalSpace;
    public float horizontalSpace;

    public float originX;
    public float originY;

    // Use this for initialization
    void Start()
    {
        if (LevelManager.levelToLoad == LevelId())
        {
            //Disable entities not related to this level
            GameObject[] entity = GameObject.FindGameObjectsWithTag("Entity");
            Array.ForEach(entity, ent =>
            {
                if (ent.GetComponent<Player>() != player && ent.GetComponent<Goal>() != goal && !Array.Exists(enemyArr, en => en== ent.GetComponent<Enemy>()) && !Array.Exists(bombArr, bo => bo == ent.GetComponent<Bomb>()))
                {
                    ent.SetActive(false);
                }
            });

            gameObject.SetActive(true);

            gameModel.verticalSpace = verticalSpace;
            gameModel.horizontalSpace = horizontalSpace;
            gameModel.originX = originX;
            gameModel.originY = originY;

            gameModel.grid = Grid();

            player.x = playerLocation.x;
            player.y = playerLocation.y;
            gameModel.player = player;

            goal.x = goalLocation.x;
            goal.y = goalLocation.y;
            gameModel.goal = goal;

            for (int i =0; i< enemyLocationArr.Length; i++)
            {
                enemyArr[i].x = enemyLocationArr[i].x;
                enemyArr[i].y = enemyLocationArr[i].y;
            }
            gameModel.enemyArr = enemyArr;

            for (int i = 0; i < bombLocationArr.Length; i++)
            {
                bombArr[i].x = bombLocationArr[i].x;
                bombArr[i].y = bombLocationArr[i].y;
            }
            gameModel.bombArr = bombArr;

            gameModel.Commence();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    protected abstract Cell[,] Grid();
    protected abstract string LevelId();



}