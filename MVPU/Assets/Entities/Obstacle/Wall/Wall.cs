using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Wall : Entity
{

    public Direction blocking;

    [Tooltip("Specify which keys control this wall")]
    public int[] keyRelationshipIndexArr;

    public bool[] locksOpened;

    public bool retracted
    {
        get
        {
            return Array.TrueForAll(locksOpened, l => l);
        }
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log(this + " Created:" + " blocking=" + blocking + " keyRelationshipIndexArr=" + keyRelationshipIndexArr);
    }

    protected override void BuildAdditionalStateDict(Dictionary<string, object> dict)
    {
        dict.Add("blocking", blocking);
        dict.Add("locksOpened", locksOpened.Clone());
    }
    protected override void RestoreAdditionalState(Dictionary<string, object> dict)
    {
        foreach (KeyValuePair<string, object> entry in dict)
        {
            if (entry.Key == "blocking" && entry.Value is Direction)
                blocking = (Direction)entry.Value;
            else if (entry.Key == "locksOpened" && entry.Value is bool[]) {
                bool[] temp = (bool[])entry.Value;
                locksOpened = (bool[]) temp.Clone();
            }
            

        }
    }    
    
   

    public void StartAnimation(bool animate)
    {
        animator.SetBool("Immediate", !animate);
        animator.SetBool("Off", retracted);
    }
}
