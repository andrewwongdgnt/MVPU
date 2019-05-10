using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public Text title;
    public GameObject levelSelectGroup;
    public GameObject settingsGroup;


    // Use this for initialization
    void Start()
    {
        GoToLevelSelect();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToSettings()
    {
        title.text = "Settings";
        DeactivateObjects(exclusion: settingsGroup);
    }

    public void GoToLevelSelect()
    {

        title.text = "Level Select";
        DeactivateObjects(exclusion: levelSelectGroup);
    }

    private void DeactivateObjects(GameObject exclusion)
    {

        levelSelectGroup.SetActive(levelSelectGroup == exclusion);
        settingsGroup.SetActive(settingsGroup == exclusion);
    }
}
