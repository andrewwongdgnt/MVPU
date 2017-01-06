using UnityEngine;
using System.Collections;

public interface IWalker : IEntityObtainer {
    

    void StartWalkAnimation();
    void StopWalkAnimation();

}
