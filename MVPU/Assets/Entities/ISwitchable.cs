using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwitchable : IEntity {

    void StartOnAnimation(bool animate);

    void StopOnAnimation(bool animate);
}
