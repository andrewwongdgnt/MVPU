using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour {
    public Text level1_1Text;
    void Start()
    {
        LevelScore levelScore = LevelManager.LevelScoreMap[LevelManager.LevelID.LEVEL_1_1];

        SaveStateManager.LevelState levelState = SaveStateManager.LoadLevel(LevelManager.LevelID.LEVEL_1_1);
        level1_1Text.text = levelState !=null ? "Beaten " + levelState.moveCount+"/"+ levelScore.minMoveCount : "New";
    }
   

    [EnumAction(typeof(LevelManager.LevelID))]
    public void SelectLevel(int argument)
    {
        LevelManager.levelToLoad = (LevelManager.LevelID) argument;   
        SceneManager.LoadScene("Load Screen");
    }
}
