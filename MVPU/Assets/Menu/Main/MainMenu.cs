using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void StartGame()
    {
        LevelManager.levelToLoad = "Level 1 - 1";
        SceneManager.LoadScene("Level");
    }
}
