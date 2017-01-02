using UnityEngine;
using System;

public class ScaleButton : MonoBehaviour {

    public GameObject[] childrenToScale;
    public float scaleTo;
	
    public void ScaleChildren(Boolean shrink)
    {
        Array.ForEach(childrenToScale, go =>
        {
            go.transform.localScale = shrink ? new Vector3(scaleTo, scaleTo, scaleTo) : new Vector3(1, 1, 1);
        });
    }
}
