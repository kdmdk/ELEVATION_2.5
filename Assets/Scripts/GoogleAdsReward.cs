using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoogleAdsReward : MonoBehaviour
{
    private RewardedAd rewardedAd;
    public Text messageText;
    int count = 0;
    public static bool isRewarded;

    public GameObject gold_object;
    Text gold_text;

    bool adStop = true;

    public GameObject adWindow;
    public GameObject GETPanel;
    public Text GETText;

    void Start()
    {
        adWindow.SetActive(false);
        GETPanel.SetActive(false);
        //messageText.gameObject.SetActive(false);

#if UNITY_ANDROID
        string appId = "ca-app-pub-4115951976307377~6026726803"; //ca-app-pub-4115951976307377~6026726803 // ca-app-pub-3940256099942544~3347511713
#elif UNITY_IPHONE
        string appId = "ca-app-pub-3940256099942544~1458002511";
#else
        string appId = "unexpected_platform";
#endif
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
        RequestRewardAd();
    }

    private void Update()
    {
        if (isRewarded)
        {
            
            ShowRewardResult();
        }
        /*
        else
        {
            if (!AdButton.AdOK)
            {
                SceneManager.LoadScene("NewGame");
                NewGame.refloor();
                NewGame.reLife();
            }
        }
        */
    }


    public void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            SoundManager.instance.StopBGM();
            this.rewardedAd.Show();
            //NewGame.refloor();
            //NewGame.reLife();
        }
    }


    void RequestRewardAd()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4115951976307377/4069221223"; //ca-app-pub-4115951976307377/4069221223  // ダミー　ca-app-pub-3940256099942544/5224354917
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif
        this.rewardedAd = new RewardedAd(adUnitId);

        // Load成功時に実行する関数の登録
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Load失敗時に実行する関数の登録
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // 表示時に実行する関数の登録
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // 表示失敗時に実行する関数の登録
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // 報酬受け取り時に実行する関数の登録
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // 広告を閉じる時に実行する関数の登録
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }






    public void ShowRewardResult()
    {
        //count++;
        //messageText.text = count.ToString();

        if (!AdButton.AdOK)
        {

            SoundManager.instance.PlaySE(5);
            GETPanel.SetActive(true);
            GETText.text = (NewGame.SCORE * 2).ToString() + " GOLD!";
            NewGame.GOLD += NewGame.SCORE * 2;
            PlayerPrefs.SetInt("GOLD-POINT", NewGame.GOLD);
            PlayerPrefs.Save();
            //SceneManager.LoadScene("NewGame");
            //messageText.text = NewGame.SCORE + "GOLD GET";
            //messageText.gameObject.SetActive(true);
        }
        else
        {
            AdButton.AdOK = false;
            NewGame.GOLD += 500;
            PlayerPrefs.SetInt("GOLD-POINT", NewGame.GOLD);
            PlayerPrefs.Save();
            gold_text = gold_object.GetComponent<Text>();
            gold_text.text = "" + PlayerPrefs.GetInt("GOLD-POINT", NewGame.GOLD);
            SoundManager.instance.PlayBGM(0);
            SoundManager.instance.PlaySE(7);
        }
        isRewarded = false;
        adStop = false;
    }


    public void CreateAndLoadRewardedAd()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4115951976307377/4069221223"; //ca-app-pub-4115951976307377/4069221223 // ダミー　ca-app-pub-3940256099942544/5224354917
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }


    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);

    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        CreateAndLoadRewardedAd();

        if (!AdButton.AdOK)
        {

            Invoke("SceneChange", 3f);
        }
        //SceneManager.LoadScene("NewGame");
        //AdButton.AdOK = false;
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
        isRewarded = true;
    }

    public void OnClickPanel()
    {
        adWindow.SetActive(true);
    }
    public void closeButton()
    {
        SoundManager.instance.PlaySE(5);
        GETPanel.SetActive(true);
        GETText.text = NewGame.SCORE.ToString() + " GOLD!";
        NewGame.GOLD += NewGame.SCORE;
        PlayerPrefs.SetInt("GOLD-POINT", NewGame.GOLD);
        PlayerPrefs.Save();
        Invoke("SceneChange", 3f);
    }
    void SceneChange()
    {
        SceneManager.LoadScene("NewGame");
    }
}
