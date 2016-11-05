using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    GameObject[] pauseObjects;

    // Use this for initialization
    void Start()
    {
       
        pauseObjects = GameObject.FindGameObjectsWithTag("Pause");

        SetPause(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            //Time.timeScale = Time.timeScale==1 ? 0 : 1;
            SetPause(Time.timeScale > 0);

        }
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetPause(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
        if (Time.timeScale == 1)
        {
            HidePaused();
        }
        else if (Time.timeScale == 0)
        {
            ShowPaused();
        }
    }

    public void ShowPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void HidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void Quit()
    {
        SetPause(false);
        SceneManager.LoadScene("Main Menu");
    }
}
