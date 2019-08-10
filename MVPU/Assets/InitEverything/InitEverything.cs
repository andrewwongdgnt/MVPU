using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitEverything : MonoBehaviour
{

    string gameId = "3237640";
    bool testMode = false;

    void Start()
    {
        if (!Advertisement.isInitialized)
        {
            Advertisement.Initialize(gameId, testMode);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
