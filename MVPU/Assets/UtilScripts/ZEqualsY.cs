﻿using UnityEngine;
using System.Collections;

public class ZEqualsY : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
