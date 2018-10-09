using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortalService {


    public enum DeathAnimation { None, Slip, Dead }

    private IMortal mortal;
	public MortalService(IMortal mortal)
    {
        this.mortal = mortal;
    }

    public void StartDieAnimation(DeathAnimation deathAnimation)
    {
        mortal.animator.SetBool(deathAnimation.ToString(), true);
    }
    public void StopDieAnimation()
    {
        mortal.animator.SetBool("Dead", false);
        mortal.animator.SetBool("Slip", false);
    }
}
