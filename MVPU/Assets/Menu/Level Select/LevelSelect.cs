using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    public void SelectLevel(string level)
    {
        LevelManager.levelToLoad = level;
        SceneManager.LoadScene("Level");
    }
}
