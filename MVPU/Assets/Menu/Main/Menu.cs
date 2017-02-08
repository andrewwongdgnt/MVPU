using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public Text title;

    private GameObject[] level_arr;
    private GameObject[] settings_arr;
    private GameObject[] world_arr;

    // Use this for initialization
    void Start()
    {

        world_arr = GameObject.FindGameObjectsWithTag("World");
        settings_arr = GameObject.FindGameObjectsWithTag("Settings");
        level_arr = GameObject.FindGameObjectsWithTag("Level");
        GoToLevelSelect();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToSettings()
    {
        title.text = "Settings";
        DeactivateObjects(settings: false);
    }

    public void GoToLevelSelect()
    {

        title.text = "Level Select";
        DeactivateObjects(level: false, world:false);
    }

    private void DeactivateObjects(bool settings = true, bool level = true, bool world = true)
    {
        Array.ForEach(settings_arr, go => go.SetActive(!settings));
        Array.ForEach(level_arr, go => go.SetActive(!level));
        Array.ForEach(world_arr, go => go.SetActive(!world));
    }
}
