﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Image loadImage;
    private void Start()
    {
        StartCoroutine(LoadLevel());
    }
    

    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadLevel()
    {


        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync("Level");
        //async.allowSceneActivation = false;
        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            loadImage.transform.Rotate(Vector3.forward*3);
            Debug.Log("async.progress: " + async.progress);
            yield return null;
        }

    }

}