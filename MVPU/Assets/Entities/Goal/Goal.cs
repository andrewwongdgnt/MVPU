﻿using UnityEngine;
using System.Collections;

public class Goal : Entity
{

	// Use this for initialization
	void Start () {
        Debug.Log(this + " Created:" + " x=" + x + " y=" + y);
    }

    public void StartWinAnimation()
    {
        animator.SetBool("Win", true);
    }


    public void StopWinAnimation()
    {
        animator.SetBool("Win", false);

    }

}
