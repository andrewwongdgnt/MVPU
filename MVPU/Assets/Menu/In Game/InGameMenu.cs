using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{

    public GameModel gameModel;
    public GameObject winMenu;
    public GameObject loseMenu;
    public GameObject pauseMenu;

    public RatingBoard ratingBoard;
    public GameObject pauseBG;
    public Image[] winVariationKongoImages;
    public Image[] winVariationPurpleMonkeyImages;
    public Image[] winVariationNeutralImages;

    public Image[] loseVariationKongoImages;
    public Image[] loseVariationPurpleMonkeyImages;
    public Image[] loseVariationNeutralImages;

    public AudioSource musicAudioSource;
    public AudioClip loseMusic;
    public AudioClip winMusic;
    public AudioClip pauseMusic;
    public AudioSource sfxAudioSource;
    public AudioClip loseSfx;
    public AudioClip winSfx;



    // Use this for initialization
    void Start()
    {
        SetPause(false, 0);
    }


    //Need a delay so that the swipe manager doesn't pick up the taps as a swipe
    private IEnumerator SetPauseWithDelay(bool pause, int delay = 30)
    {
        yield return delay;
        Time.timeScale = pause ? 0 : 1;
        pauseMenu.SetActive(pause);
        pauseBG.SetActive(pause);

        winMenu.SetActive(false);
        loseMenu.SetActive(false);
    }

    //Exists for the button's on click
    public void SetPauseWithDelay(bool pause)
    {
        SetPause(pause);
    }

    private void SetPause(bool pause, int delay = 30)
    {
        if (pause)
        {
            musicAudioSource.UnPause();
            if (!musicAudioSource.isPlaying)
                AudioUtil.PlayMusic(musicAudioSource, pauseMusic);
        }
        else
            musicAudioSource.Stop();

        sfxAudioSource.Stop();
        StartCoroutine(SetPauseWithDelay(pause, delay));

    }
    

    public void ShowLoseMenu(bool showKongo, bool showPurpleMonkey)
    {
        StartCoroutine(ShowLoseMenuWithDelay(showKongo, showPurpleMonkey));
    }
    private IEnumerator ShowLoseMenuWithDelay(bool showKongo, bool showPurpleMonkey)
    {
        yield return 30;
        Time.timeScale = 0;

        PlayMusic(loseMusic);
        AudioUtil.PlaySFX(sfxAudioSource, loseSfx);

        winMenu.SetActive(false);
        loseMenu.SetActive(true);
        pauseMenu.SetActive(false);
        pauseBG.SetActive(true);
        ToggleImageVisibility(loseVariationKongoImages, false);
        ToggleImageVisibility(loseVariationPurpleMonkeyImages, false);
        ToggleImageVisibility(loseVariationNeutralImages, false);

        if (showKongo)
        {
            ToggleImageVisibility(loseVariationKongoImages, true);
        }
        if (showPurpleMonkey)
        {
            ToggleImageVisibility(loseVariationPurpleMonkeyImages, true);
        }
        if (!showKongo && !showPurpleMonkey)
        {
            ToggleImageVisibility(loseVariationNeutralImages, true);
        }
    }
    
    public void ShowWinMenu(ScoringModel.ScoreTypes scoreType, bool showKongo, bool showPurpleMonkey)
    {
        StartCoroutine(ShowWinMenuWithDelay(scoreType, showKongo, showPurpleMonkey));
    }
    public IEnumerator ShowWinMenuWithDelay(ScoringModel.ScoreTypes scoreType, bool showKongo, bool showPurpleMonkey)
    {
        yield return 30;
        Time.timeScale = 0;

        PlayMusic(winMusic);
        AudioUtil.PlaySFX(sfxAudioSource, winSfx);

        winMenu.SetActive(true);
        loseMenu.SetActive(false);
        pauseMenu.SetActive(false);
        pauseBG.SetActive(true);
        ToggleImageVisibility(winVariationKongoImages, false);
        ToggleImageVisibility(winVariationPurpleMonkeyImages, false);
        ToggleImageVisibility(winVariationNeutralImages, false);

        if (showKongo)
        {
            ToggleImageVisibility(winVariationKongoImages, true);
        }
        if (showPurpleMonkey)
        {
            ToggleImageVisibility(winVariationPurpleMonkeyImages, true);
        }
        if (!showKongo && !showPurpleMonkey)
        {
            ToggleImageVisibility(winVariationNeutralImages, true);
        }
        ratingBoard.Display(scoreType);


    }

    private void ToggleImageVisibility(Image[] images, bool show)
    {
        Array.ForEach(images, im => im.enabled = show);
    }

    private void PlayMusic(AudioClip music)
    {
        musicAudioSource.UnPause();
        if (!musicAudioSource.isPlaying)
            AudioUtil.PlayMusic(musicAudioSource, music);    
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


    public void NextLevel()
    {
        LevelUtil.levelToLoad = LevelUtil.LevelPrereq[LevelUtil.levelToLoad].second;
        SceneManager.LoadScene("Load Screen");
    }

    public void Undo(bool removeLastState)
    {
        SetPause(false);
        gameModel.Undo(removeLastState);
    }

    public void Redo()
    {
        SetPause(false);
        gameModel.Redo();
    }
}
