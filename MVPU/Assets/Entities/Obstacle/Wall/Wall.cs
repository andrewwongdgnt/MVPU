using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Wall : Entity, ISwitchable
{
    [System.Serializable]
    public class KeyRelationship : System.Object
    {        
        public int[] arr;
    }

    public Direction blocking;

    [Tooltip("Specify which keys control this wall. For each inner array, one of the keys open that lock. All locks must be open for the wall to retract. EG [[0,1],[2]] means key 0 or key 1 unlocks lock 0 and key 2 unlocks lock 1. ")]
    public KeyRelationship[] keyRelationshipIndexArr;

    public bool[] locksOpened;

    public enum Animation { None,Retract, DontRetract }

    public bool retracted
    {
        get
        {
            return Array.TrueForAll(locksOpened, l => l);
        }
    }

    private SwitchableService _switchableService;
    private SwitchableService switchableService
    {
        get
        {
            if (_switchableService == null)
            {
                _switchableService = new SwitchableService(this);
            }
            return _switchableService;
        }
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log(this + " Created:" + " blocking=" + blocking + " keyRelationshipIndexArr=" + keyRelationshipIndexArr);
    }

    protected override void BuildAdditionalStateDict(Dictionary<string, object> dict)
    {

        dict.Add("locksOpened", locksOpened.Clone());
    }
    protected override void RestoreAdditionalState(Dictionary<string, object> dict)
    {
        foreach (KeyValuePair<string, object> entry in dict)
        {
            if (entry.Key == "locksOpened" && entry.Value is bool[]) {
                bool[] temp = (bool[])entry.Value;
                locksOpened = (bool[]) temp.Clone();
            }
            

        }
    }
    
    //---------------
    // ISwitchable Impl
    //---------------

    public void StartOnAnimation(bool animate)
    {
        switchableService.StartOnAnimation(animate);
    }

    public void StopOnAnimation(bool animate)
    {
        switchableService.StopOnAnimation(animate);
    }
}
