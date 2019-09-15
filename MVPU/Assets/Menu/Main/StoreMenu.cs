using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class StoreMenu : MonoBehaviour
{
    public IAPManager iapManager;

    public IAPButton noAdsBtn;
    public IAPButton unlockAllLevelsBtn;
    public IAPButton allOfTheAboveBtn;

    public void BuyNoAds()
    {
        if (!LevelUtil.disableAds)
            iapManager.BuyNoAds();
    }

    public void BuyUnlockAllLevels()
    {
        if (!LevelUtil.allLevelsUnlocked)
            iapManager.BuyUnlockAllLevels();
    }

    public void BuyAllOfTheAbove()
    {
        if (!LevelUtil.disableAds || !LevelUtil.allLevelsUnlocked)
            iapManager.BuyAllOfTheAbove();
    }

    private void Update()
    {
        if (Debug.isDebugBuild || (LevelUtil.disableAds && LevelUtil.allLevelsUnlocked))
        {
            noAdsBtn.SetAvailable(false);
            unlockAllLevelsBtn.SetAvailable(false);
            allOfTheAboveBtn.SetAvailable(false);
        }
        else if (LevelUtil.disableAds)
        {
            noAdsBtn.SetAvailable(false);
            unlockAllLevelsBtn.SetAvailable(true);
            allOfTheAboveBtn.SetAvailable(true);
        }
        else if (LevelUtil.allLevelsUnlocked)
        {
            noAdsBtn.SetAvailable(true);
            unlockAllLevelsBtn.SetAvailable(false);
            allOfTheAboveBtn.SetAvailable(true);
        }
        else
        {
            noAdsBtn.SetAvailable(true);
            unlockAllLevelsBtn.SetAvailable(true);
            allOfTheAboveBtn.SetAvailable(true);
        }
    }



}
