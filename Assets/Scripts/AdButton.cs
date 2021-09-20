using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdButton : MonoBehaviour
{
    public GameObject ADWindow;

    public static bool AdOK = false;

    public GoogleAdsReward googleAdsReward;
    // Start is called before the first frame update
    void Start()
    {
        ADWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NoButton()
    {
        SoundManager.instance.PlaySE(14);
        ADWindow.SetActive(false);

    }
    public void YesButton()
    {
        AdOK = true;
        //GoogleAdsReward.isRewarded = true;
        googleAdsReward.UserChoseToWatchAd();
        ADWindow.SetActive(false);
    }
    public void OnClick()
    {
        ADWindow.SetActive(true);
    }
}
