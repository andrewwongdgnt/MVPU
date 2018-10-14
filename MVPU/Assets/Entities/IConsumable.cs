using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumable : IEntity {

    void StartConsumedAnimation();
    void StopConsumedAnimation();
    void StartUsedAnimation();
}
