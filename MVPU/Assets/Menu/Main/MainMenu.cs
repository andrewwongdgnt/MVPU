using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void StartGame()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene("Settings");
    }
}
