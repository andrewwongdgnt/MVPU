using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class StoreMenu : MonoBehaviour
{
    public Button btn;

    public void ButtonPress()
    {
        if (Advertisement.IsReady())
        {
            Debug.Log("Ad is ready");
            Advertisement.Show("video");
        }
        else
        {
            Debug.Log("Ad not ready");
        }
    }
 
}
