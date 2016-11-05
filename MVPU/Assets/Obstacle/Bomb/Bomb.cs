using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {


    public int x;
    public int y;
    public bool affectsEnemy;
    public bool affectsPlayer;
    public int numOfUses;

    public bool inactive
    {
        get;set;
    }

    //TODO: maybe allow this, come back later
    //public int radius;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
