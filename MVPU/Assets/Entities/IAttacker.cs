using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttacker : IEntityObtainer
{
    //Obstacles can attack too.  EG, bomb will just blow up and banana will fly away

    void StartAttackAnimation();
    void StopAttackAnimation();

}
