using UnityEngine;
using System.Collections;


public abstract class LevelModel : MonoBehaviour
{

    public GameModel gameModel;
    public Player player;
    public Goal goal;
    public Enemy[] enemyArr;
    public Bomb[] bombArr;

    public float verticalSpace;
    public float horizontalSpace;

    public float originX;
    public float originY;

    // Use this for initialization
    void Start()
    {
        if (LevelManager.levelToLoad == LevelId())
        {

            gameObject.SetActive(true);

            gameModel.verticalSpace = verticalSpace;
            gameModel.horizontalSpace = horizontalSpace;
            gameModel.originX = originX;
            gameModel.originY = originY;

            gameModel.grid = Grid();

            gameModel.bombArr = bombArr;

            gameModel.player = player;
            gameModel.goal = goal;

            gameModel.enemyArr = enemyArr;

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