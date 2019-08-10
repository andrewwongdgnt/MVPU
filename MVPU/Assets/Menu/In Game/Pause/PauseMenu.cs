using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public class PauseMenu : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip music;

    GameObject[] pauseObjects;

    // Use this for initialization
    void Start()
    {

        pauseObjects = GameObject.FindGameObjectsWithTag("Pause").Concat(GameObject.FindGameObjectsWithTag("GenericMenu")).ToArray();


        SetPause(false, 0);
    }


    //Need a delay so that the swipe manager doesn't pick up the taps as a swipe
    private IEnumerator SetPauseWithDelay(bool pause, int delay = 30)
    {
        yield return delay;
        Time.timeScale = pause ? 0 : 1;
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(pause);
        }
    }

    //Exists for the button's on click
    public void SetPauseWithDelay(bool pause)
    {
        SetPause(pause);
    }

    public void SetPause(bool pause, int delay = 30)
    {
        if (pause)
        {
            audioSource.UnPause();
            if (!audioSource.isPlaying)
                AudioUtil.PlayMusic(audioSource, music);
        }
        else
            audioSource.Stop();
        StartCoroutine(SetPauseWithDelay(pause, delay));

    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        SetPause(false);
        SceneManager.LoadScene("Main");
    }



}
