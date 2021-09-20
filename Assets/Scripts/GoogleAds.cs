using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAds : MonoBehaviour
{

    static BannerView bannerView;
    // Use this for initialization
    void Start()
    {

#if UNITY_ANDROID
            string appId = "ca-app-pub-4115951976307377~6026726803";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#else
        string appId = "unexpected_platform";
#endif
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        RequestBanner();
    }
    private void RequestBanner()
    {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-4115951976307377/6190378332";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif
        if (bannerView != null)
        {
            bannerView.Hide();
            bannerView.Destroy();
            Debug.Log("ad:バナー広告作成前に既にあるBannerViewを破棄する");
        }
        else if (bannerView == null)
        {
            Debug.Log("ad:バナー広告作成前にBannerViewは存在しない");
        }
        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);

    }
    public static void bannerHideAndDestroy()
    {
        bannerView.Hide();
        bannerView.Destroy();
    }
}