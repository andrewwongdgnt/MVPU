using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectUtil
{

    public static readonly int MAX_WORLD_INDEX=9;

    private readonly static string CURRENT_WORLD_KEY = "CurrentWorldKey";

    public static void ChangeWorld(bool up)
    {
        int currentWorld = GetCurrentWorld();
        if (up && currentWorld < MAX_WORLD_INDEX)
            currentWorld++;
        else if (!up && currentWorld > 0)
            currentWorld--;
        SaveCurrentWorld(currentWorld);
    }

    public static void SaveCurrentWorld(int currentWorld)
    {
        PlayerPrefs.SetInt(CURRENT_WORLD_KEY, currentWorld);
    }

    public static int GetCurrentWorld()
    {
        int rtn = PlayerPrefs.HasKey(CURRENT_WORLD_KEY) ? PlayerPrefs.GetInt(CURRENT_WORLD_KEY) : 0;
        if (rtn < 0)
            return 0;
        else if (rtn > MAX_WORLD_INDEX)
            return MAX_WORLD_INDEX;
        else
            return rtn;              
    }
    

}
