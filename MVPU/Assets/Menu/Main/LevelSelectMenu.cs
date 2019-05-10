using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour {

    public LevelSelectButton levelSelectButton1;
    public LevelSelectButton levelSelectButton2;
    public LevelSelectButton levelSelectButton3;
    public LevelSelectButton levelSelectButton4;
    public LevelSelectButton levelSelectButton5;
    public LevelSelectButton levelSelectButton6;
    public LevelSelectButton levelSelectButton7;
    public LevelSelectButton levelSelectButton8;
    public LevelSelectButton levelSelectButton9;
    public LevelSelectButton levelSelectButton10;

    public Text levelsComingSoonTxt;

    // Use this for initialization
    void Start()
    {
        UpdateAllLevelSelectButtons();
        DisplayWorldComingSoonMessage();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateAllLevelSelectButtons()
    {
        UpdateLevelSelectButton(levelSelectButton1);
        UpdateLevelSelectButton(levelSelectButton2);
        UpdateLevelSelectButton(levelSelectButton3);
        UpdateLevelSelectButton(levelSelectButton4);
        UpdateLevelSelectButton(levelSelectButton5);
        UpdateLevelSelectButton(levelSelectButton6);
        UpdateLevelSelectButton(levelSelectButton7);
        UpdateLevelSelectButton(levelSelectButton8);
        UpdateLevelSelectButton(levelSelectButton9);
        UpdateLevelSelectButton(levelSelectButton10);
    }

    void UpdateLevelSelectButton(LevelSelectButton levelSelectButton)
    {
        levelSelectButton.label.text = (LevelSelectUtil.GetCurrentWorld() * 10 + levelSelectButton.levelIndex + 1).ToString();
        levelSelectButton.UpdateDisplay();
    }

    public void ChangeWorld(bool up)
    {
        LevelSelectUtil.ChangeWorld(up);

        UpdateAllLevelSelectButtons();
        DisplayWorldComingSoonMessage();
    }

    private void DisplayWorldComingSoonMessage()
    {
        levelsComingSoonTxt.gameObject.SetActive(LevelSelectUtil.GetCurrentWorld() >= LevelUtil.UNAVAILABLE_WORLD_START_INDEX);
    }
}
