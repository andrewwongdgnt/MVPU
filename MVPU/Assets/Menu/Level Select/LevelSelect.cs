using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
    public Text level1_1Text;
    void Start()
    {
        LevelScore levelScore = LevelManager.LevelScoreMap["Level 1 - 1"];

        SaveStateManager.LevelState levelState = SaveStateManager.LoadLevel("Level 1 - 1");
        level1_1Text.text = (levelState !=null ? "Beaten" : "New") + " " + levelState.moveCount+"/"+ levelScore.minMoveCount;
    }

    public void SelectLevel(string level)
    {
        LevelManager.levelToLoad = level;
        SceneManager.LoadScene("Level");
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
