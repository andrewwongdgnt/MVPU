﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdUtil
{

    private readonly static string COUNTER_KEY = "counter";
    private readonly static int COUNT_TO_RESET = 3;

    public static void WatchAd(Action action)
    {
        int currentCount = PlayerPrefs.GetInt(COUNTER_KEY, 0);
            currentCount++;

        if (currentCount >= COUNT_TO_RESET )
        {
            WatchAdInternal(action);
            currentCount = 0;
        }
        else
        {
            action.Invoke();
        }

        PlayerPrefs.SetInt(COUNTER_KEY, currentCount);

    }

    public static void WatchAdWithCondition(Action action, bool condition)
    {


        if (condition)
        {
            WatchAdInternal(action);
        }
        else
        {
            action.Invoke();
        }
        
    }

    private static void WatchAdInternal(Action action)
    {
        if (Advertisement.IsReady() && !Debug.isDebugBuild && !LevelUtil.disableAds)
        {
            Debug.Log("Ad is ready");
            Advertisement.Show("video", new ShowOptions()
            {
                resultCallback = (result) =>
                {
                    switch (result)
                    {
                        case ShowResult.Finished:
                            Debug.Log("Ad has finished");
                            break;
                        case ShowResult.Skipped:
                            Debug.Log("Ad has been skipped");
                            break;
                        case ShowResult.Failed:
                            Debug.Log("Ad has failed for some reason");
                            break;

                    }

                    action.Invoke();
                }
            });

        }
        else
        {
            if (Debug.isDebugBuild)
                Debug.Log("Not showing ad because it is a debug build");
            else if (LevelUtil.disableAds)            
                Debug.Log("Not showing ad because ads are disabled");
            else
                Debug.Log("Ad not ready");

            action.Invoke();
        }
    }
}
