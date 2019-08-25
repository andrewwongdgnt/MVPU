using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public Text title;
    public GameObject levelSelectGroup;
    public Image levelSelectButton;
    public GameObject settingsGroup;
    public Image settingsButton;
    public GameObject helpGroup;
    public Image helpButton;
    public GameObject creditsGroup;
    public Image creditsButton;
    public GameObject storeGroup;
    public Image storeButton;


    // Use this for initialization
    void Start()
    {
        GoToLevelSelect();
    }

   

    public void GoToSettings()
    {
        title.text = "Settings";
        DeactivateObjects(exclusion: settingsGroup);
        UnhighlightButtons(settingsButton);
    }

    public void GoToLevelSelect()
    {

        title.text = "Level Select";
        DeactivateObjects(exclusion: levelSelectGroup);
        UnhighlightButtons(levelSelectButton);
    }

    public void GoToHelp()
    {

        title.text = "How to play";
        DeactivateObjects(exclusion: helpGroup);
        UnhighlightButtons(helpButton);
    }

    public void GoToCredits()
    {

        title.text = "Credits";
        DeactivateObjects(exclusion: creditsGroup);
        UnhighlightButtons(creditsButton);
    }

    public void GoToStore()
    {

        title.text = "Purchase";
        DeactivateObjects(exclusion: storeGroup);
        UnhighlightButtons(storeButton);
    }

    private void DeactivateObjects(GameObject exclusion)
    {

        levelSelectGroup.SetActive(levelSelectGroup == exclusion);
        settingsGroup.SetActive(settingsGroup == exclusion);
        helpGroup.SetActive(helpGroup == exclusion);
        creditsGroup.SetActive(creditsGroup == exclusion);
        storeGroup.SetActive(storeGroup == exclusion);
    }

    private void UnhighlightButtons(Image exclusion)
    {

        levelSelectButton.color = Color.white;
        settingsButton.color = Color.white;
        helpButton.color = Color.white;
        creditsButton.color = Color.white;
        storeButton.color = Color.white;
        exclusion.color = Color.green;
    }
}
