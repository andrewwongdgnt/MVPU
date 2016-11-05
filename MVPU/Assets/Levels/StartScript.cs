using UnityEngine;
using System.Collections;
using System;

public class StartScript : MonoBehaviour {

    GameObject[] levels;

    // Use this for initialization
    void Start () {

        levels = GameObject.FindGameObjectsWithTag("Level");
        Array.ForEach(levels, l =>
        {
            l.SetActive(true);
        });
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
