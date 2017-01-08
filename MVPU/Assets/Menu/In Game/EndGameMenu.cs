using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{

    GameObject[] winObjects;
    GameObject[] loseObjects;
    GameObject[] unPauseObjects;


    public Text title;

    // Use this for initialization
    void Start()
    {
        GameObject[] endGameMenuItems = GameObject.FindGameObjectsWithTag("WinLose").Concat(GameObject.FindGameObjectsWithTag("GenericMenu")).ToArray();
        winObjects = GameObject.FindGameObjectsWithTag("Win").Concat(endGameMenuItems).ToArray();
        loseObjects = GameObject.FindGameObjectsWithTag("Lose").Concat(endGameMenuItems).ToArray();
        unPauseObjects = GameObject.FindGameObjectsWithTag("Unpause");

        HideEndGameMenu(0);
    }

    //Need a delay so that the swipe manager doesn't pick up the taps as a swipe
    private IEnumerator HideEndGameMenuWithDelay(int delay = 30)
    {
        yield return delay;
        Time.timeScale = 1;

        foreach (GameObject g in winObjects)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in loseObjects)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in unPauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void HideEndGameMenu(int delay = 30)
    {
        StartCoroutine(HideEndGameMenuWithDelay(delay));


    }

    public void ShowLoseMenu(bool show)
    {
        StartCoroutine(ShowLoseMenuWithDelay(show));
    }
    private IEnumerator ShowLoseMenuWithDelay(bool show)
    {
        yield return 30;
        Time.timeScale = show ? 0 : 1;

        if (show)
            title.text = "Game Over";
        foreach (GameObject g in winObjects)
        {
            g.SetActive(!show);
        }
        foreach (GameObject g in loseObjects)
        {
            g.SetActive(show);
        }
        foreach (GameObject g in unPauseObjects)
        {
            g.SetActive(!show);
        }
    }


    public void ShowWinMenu(bool show)
    {
        StartCoroutine(ShowWinMenuWithDelay(show));
    }
    public IEnumerator ShowWinMenuWithDelay(bool show)
    {
        yield return 30;
        Time.timeScale = show ? 0 : 1;

        if (show)
            title.text = "Win";
        foreach (GameObject g in loseObjects)
        {
            g.SetActive(!show);
        }
        foreach (GameObject g in winObjects)
        {
            g.SetActive(show);
        }
        foreach (GameObject g in unPauseObjects)
        {
            g.SetActive(!show);
        }
    }
}
