using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager {

    public static string levelToLoad = "Level 1 - 1";

    public static Dictionary<string, LevelScore> LevelScoreMap = new Dictionary<string, LevelScore>
        {
            { "Level 1 - 1", new LevelScore(10) },
            { "Level 1 - 2", new LevelScore(8) },
            { "Level 1 - 3", new LevelScore(9) },
            { "Level 1 - 4", new LevelScore(9) },
            { "Level 1 - 5", new LevelScore(10) },
        };
}
