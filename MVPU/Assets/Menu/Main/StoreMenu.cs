using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class StoreMenu : MonoBehaviour
{
    public IAPManager iapManager;

    public void BuyNoAds()
    {
        iapManager.BuyNoAds();
    }

    public void BuyUnlockAllLevels()
    {
        iapManager.BuyUnlockAllLevels();
    }

    public void BuyAllOfTheAbove()
    {
        iapManager.BuyAllOfTheAbove();
    }

}
