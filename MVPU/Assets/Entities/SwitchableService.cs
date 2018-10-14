using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchableService  {

    private ISwitchable switchable;
    public SwitchableService (ISwitchable switchable)
    {
        this.switchable = switchable;
    }
    public void StartOnAnimation(bool animate)
    {
        switchable.animator.SetBool("Immediate", !animate);
        switchable.animator.SetBool("On", true);
    }

    public void StopOnAnimation(bool animate)
    {
        switchable.animator.SetBool("Immediate", !animate);
        switchable.animator.SetBool("On", false);
    }
}
