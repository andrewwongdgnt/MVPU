using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Key : Entity, IConsumable, ISwitchable
{

    public bool usableByEnemy;
    public bool hold;
    [Tooltip("-1 for infinite uses")]
    public int numOfUses;
    public AudioClip sfxConsumedClip;
    public AudioClip sfxUsedClip;
    public AudioClip sfxTransitionOnClip;
    public AudioClip sfxTransitionOffClip;

    public enum Animation { None, Used, Consumed, On, Off }

    public bool consumed
    {
        get; set;
    }

    public bool on
    {
        get; set;
    }

    private ConsumableService _consumableService;
    private ConsumableService consumableService
    {
        get
        {
            if (_consumableService == null)
            {
                _consumableService = new ConsumableService(this);
            }
            return _consumableService;
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
        Debug.Log(this + " Created:" + " x=" + x + " y=" + y + " usableByEnemy=" + usableByEnemy + " hold=" + hold + " numOfUses=" + numOfUses + " consumed=" + consumed + " on=" + on);
    }

    protected override void BuildAdditionalStateDict(Dictionary<string, object> dict)
    {
        dict.Add("usableByEnemy", usableByEnemy);
        dict.Add("hold", hold);
        dict.Add("numOfUses", numOfUses);
        dict.Add("consumed", consumed);
        dict.Add("on", on);
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
            else if (entry.Key == "on" && entry.Value is bool)
                on = (bool)entry.Value;
        }
    }

    //---------------
    // IConsumable Impl
    //---------------

    public void StartConsumedAnimation()
    {
        consumableService.StartConsumedAnimation();
    }
    public void StopConsumedAnimation()
    {
        consumableService.StopConsumedAnimation();
    }
    public void StartUsedAnimation()
    {
        consumableService.StartUsedAnimation();
    }

    AudioClip IConsumable.sfxUsedClip
    {
        get
        {
            return sfxUsedClip;
        }
    }

    AudioClip IConsumable.sfxConsumedClip
    {
        get
        {
            return sfxConsumedClip;
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

    AudioClip ISwitchable.sfxTransitionOnClip
    {
        get
        {
            return sfxTransitionOnClip;
        }
    }

    AudioClip ISwitchable.sfxTransitionOffClip
    {
        get
        {
            return sfxTransitionOffClip;
        }
    }

}
