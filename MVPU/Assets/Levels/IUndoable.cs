using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IUndoable {

    Dictionary<string,object> BuildDict();

    void RestoreState(Dictionary<string, object> dict);
}
