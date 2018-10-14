using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableService  {

    private IConsumable consumble;
    public ConsumableService(IConsumable consumble)
    {
        this.consumble = consumble;
    }
    public void StartConsumedAnimation()
    {
        consumble.animator.SetBool("Consumed", true);
    }
    public void StopConsumedAnimation()
    {
        consumble.animator.SetBool("Consumed", false);
    }
    public void StartUsedAnimation()
    {
        consumble.animator.SetTrigger("Used");
    }
}
