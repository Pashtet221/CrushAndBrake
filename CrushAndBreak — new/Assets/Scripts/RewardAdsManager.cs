using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using TMPro;

public class RewardAdsManager : MonoBehaviour
{
    public YandexGame sdk;

    private void Start()
    {
        //Debug.Log(BinarySerializer.persistentDataPath);
    }


    public void RewardAd()
    {
        sdk._RewardedShow(1);
    }

    public void InterstitialAd()
    {
        sdk._FullscreenShow();
    }

    public void AdButtonCul()
    {
        Bomb.instance.bombs += 1;
    }

    public void OpenVideoAd()
    {
        
    }
}
