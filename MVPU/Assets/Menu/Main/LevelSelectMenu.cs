using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour {
    void Start()
    {
 
    }
   

    [EnumAction(typeof(LevelManager.LevelID))]
    public void SelectLevel(int argument)
    {
        LevelManager.levelToLoad = (LevelManager.LevelID) argument;   
        SceneManager.LoadScene("Load Screen");
    }
}
