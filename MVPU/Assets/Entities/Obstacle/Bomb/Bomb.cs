using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bomb : Entity {



    public bool affectsEnemy;
    public bool affectsPlayer;
    public int numOfUses;

    public bool inactive
    {
        get;set;
    }

    //TODO: maybe allow this, come back later
    //public int radius;

    protected override void BuildAdditionalStateDict(Dictionary<string, object> dict)
    {
        dict.Add("affectsEnemy", affectsEnemy);
        dict.Add("affectsPlayer", affectsPlayer);
        dict.Add("numOfUses", numOfUses);
        dict.Add("inactive", inactive);
    }
    protected override void RestoreAdditionalState(Dictionary<string, object> dict)
    {
        foreach (KeyValuePair<string, object> entry in dict)
        {
            if (entry.Key == "affectsEnemy" && entry.Value is bool)
                affectsEnemy = (bool)entry.Value;
            else if (entry.Key == "affectsPlayer" && entry.Value is bool)
                affectsPlayer = (bool)entry.Value;
            else if (entry.Key == "numOfUses" && entry.Value is int)
                numOfUses = (int)entry.Value;
            else if (entry.Key == "inactive" && entry.Value is bool)
                inactive = (bool)entry.Value;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
