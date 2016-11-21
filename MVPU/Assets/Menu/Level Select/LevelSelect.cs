using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
    public Text level1_1Text;
    void Start()
    {
        level1_1Text.text = PlayerPrefs.GetInt("Level 1 - 1") == 1 ? "Beaten" : "New";
    }

    public void SelectLevel(string level)
    {
        Debug.Log("PlayerPRef: " + PlayerPrefs.GetInt(level));
        LevelManager.levelToLoad = level;
        SceneManager.LoadScene("Level");
    }
}
