using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public LevelModel[] levels;

    public Tutorial[] tutorials;


    // Use this for initialization
    void Start()
    {
        LevelModel level = Array.Find(levels, l => LevelUtil.levelToLoad == l.levelID);
        if (level != null) { 
           LevelModel levelClone = Instantiate(level);
            Tutorial tutorial = Array.Find(tutorials, t => levelClone.tutorialEnum == t.tutorialType);
            levelClone.tutorial = tutorial;
            levelClone.Init();
        }
    }
}
