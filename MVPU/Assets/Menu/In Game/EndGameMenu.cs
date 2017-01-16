using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{

    GameObject[] winObjects;
    GameObject[] loseObjects;
    GameObject[] unPauseObjects;

    public Animator star1;
    public Animator star2;
    public Animator star3;


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


    public void ShowWinMenu(bool show, ScoringModel.ScoreTypes scoreType)
    {
        StartCoroutine(ShowWinMenuWithDelay(show, scoreType));
    }
    public IEnumerator ShowWinMenuWithDelay(bool show, ScoringModel.ScoreTypes scoreType)
    {
        yield return 30;
        Time.timeScale = show ? 0 : 1;


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

        star1.SetBool("StarOff", true);
        star2.SetBool("StarOff", true);
        star3.SetBool("StarOff", true);
        star1.SetBool("StarOff", false);
        star2.SetBool("StarOff", false);
        star3.SetBool("StarOff", false);
        Debug.Log("ssdsdsdsd");
        if (scoreType != ScoringModel.ScoreTypes.NONE)
        {
            if (scoreType == ScoringModel.ScoreTypes.ADEQUATE)
            {
                yield return new WaitForSecondsRealtime(.5f);
                star1.SetBool("StarOn", true);
            }
            else if (scoreType == ScoringModel.ScoreTypes.GOOD)
            {
                yield return new WaitForSecondsRealtime(.5f);
                star1.SetBool("StarOn", true);
                yield return new WaitForSecondsRealtime(.5f);
                star2.SetBool("StarOn", true);
            }
            else if (scoreType == ScoringModel.ScoreTypes.GREAT || scoreType == ScoringModel.ScoreTypes.MIN )
            {
                yield return new WaitForSecondsRealtime(.5f);
                star1.SetBool("StarOn", true);
                yield return new WaitForSecondsRealtime(.5f);
                star2.SetBool("StarOn", true);
                yield return new WaitForSecondsRealtime(.5f);
                star3.SetBool("StarOn", true);
            }
            if (scoreType == ScoringModel.ScoreTypes.MIN)
            {
                yield return new WaitForSecondsRealtime(.5f);

                star1.SetBool("StarSpecial", true);
                star2.SetBool("StarSpecial", true);
                star3.SetBool("StarSpecial", true);
            }
        }
    }
}
