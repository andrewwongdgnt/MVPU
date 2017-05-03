using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHelp : MonoBehaviour {

    public Animator anim;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Stop()
    {
        anim.SetTrigger("Stop");
    }
    public void Tap()
    {
        anim.SetTrigger("Tap");
    }
    public void Swipe()
    {
        anim.SetTrigger("Swipe");
    }
    public void HEnemy()
    {
        anim.SetTrigger("HEnemy");
    }
    public void VEnemy()
    {
        anim.SetTrigger("VEnemy");
    }


}
