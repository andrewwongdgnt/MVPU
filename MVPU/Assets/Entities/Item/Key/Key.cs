using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Key : Entity
{

    public bool usableByEnemy;
    public bool hold;
    [Tooltip("-1 for infinite uses")]
    public int numOfUses;

    public enum Animation { None, Used, Consumed }

    public bool consumed
    {
        get; set;
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log(this + " Created:" + " x=" + x + " y=" + y + " usableByEnemy=" + usableByEnemy + " hold=" + hold + " numOfUses=" + numOfUses + " consumed=" + consumed);
    }

    protected override void BuildAdditionalStateDict(Dictionary<string, object> dict)
    {
        dict.Add("usableByEnemy", usableByEnemy);
        dict.Add("hold", hold);
        dict.Add("numOfUses", numOfUses);
        dict.Add("consumed", consumed);
    }
    protected override void RestoreAdditionalState(Dictionary<string, object> dict)
    {
        foreach (KeyValuePair<string, object> entry in dict)
        {
            if (entry.Key == "usableByEnemy" && entry.Value is bool)
                usableByEnemy = (bool)entry.Value;
            else if (entry.Key == "hold" && entry.Value is bool)
                hold = (bool)entry.Value;
            else if (entry.Key == "numOfUses" && entry.Value is int)
                numOfUses = (int)entry.Value;
            else if (entry.Key == "consumed" && entry.Value is bool)
                consumed = (bool)entry.Value;
        }
    }    

   
    public void StartConsumedAnimation()
    {
        animator.SetBool("Consumed", true);
    }
    public void StopConsumedAnimation()
    {
        animator.SetBool("Consumed", false);
    }
    public void StartUsedAnimation()
    {
        animator.SetTrigger("Used");
    }

}
