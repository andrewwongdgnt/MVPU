using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using System;

public class EndGameMenu : MonoBehaviour
{

    GameObject[] winObjects;
    GameObject[] loseObjects;

    public Animator star1;
    public Animator star2;
    public Animator star3;

    public Image[] winVariationKongoImages;
    public Image[] winVariationPurpleMonkeyImages;

    public Image[] loseVariationKongoImages;
    public Image[] loseVariationPurpleMonkeyImages;



    // Use this for initialization
    void Start()
    {
        GameObject[] endGameMenuItems = GameObject.FindGameObjectsWithTag("WinLose").Concat(GameObject.FindGameObjectsWithTag("GenericMenu")).ToArray();
        winObjects = GameObject.FindGameObjectsWithTag("Win").Concat(endGameMenuItems).ToArray();
        loseObjects = GameObject.FindGameObjectsWithTag("Lose").Concat(endGameMenuItems).ToArray();

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
    }

    public void HideEndGameMenu(int delay = 30)
    {
        StartCoroutine(HideEndGameMenuWithDelay(delay));


    }

    //TODO dont think we need the bool "show"
    public void ShowLoseMenu(bool show, bool showKongo, bool showPurpleMonkey)
    {
        StartCoroutine(ShowLoseMenuWithDelay(show, showKongo, showPurpleMonkey));
    }
    private IEnumerator ShowLoseMenuWithDelay(bool show, bool showKongo, bool showPurpleMonkey)
    {
        yield return 30;
        Time.timeScale = show ? 0 : 1;

        Array.ForEach(loseVariationKongoImages, im => im.enabled = showKongo);
        Array.ForEach(loseVariationPurpleMonkeyImages, im => im.enabled = showPurpleMonkey);

        foreach (GameObject g in winObjects)
        {
            g.SetActive(!show);
        }
        foreach (GameObject g in loseObjects)
        {
            g.SetActive(show);
        }
    }

    //TODO dont think we need the bool "show"
    public void ShowWinMenu(bool show, ScoringModel.ScoreTypes scoreType, bool showKongo, bool showPurpleMonkey)
    {
        StartCoroutine(ShowWinMenuWithDelay(show, scoreType, showKongo, showPurpleMonkey));
    }
    public IEnumerator ShowWinMenuWithDelay(bool show, ScoringModel.ScoreTypes scoreType, bool showKongo, bool showPurpleMonkey)
    {
        yield return 30;
        Time.timeScale = show ? 0 : 1;

        Array.ForEach(winVariationKongoImages, im => im.enabled = showKongo);
        Array.ForEach(winVariationPurpleMonkeyImages, im => im.enabled = showPurpleMonkey);

        foreach (GameObject g in loseObjects)
        {
            g.SetActive(!show);
        }
        foreach (GameObject g in winObjects)
        {
            g.SetActive(show);
        }

        star1.SetBool("StarOff", true);
        star2.SetBool("StarOff", true);
        star3.SetBool("StarOff", true);
        star1.SetBool("StarOff", false);
        star2.SetBool("StarOff", false);
        star3.SetBool("StarOff", false);

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
