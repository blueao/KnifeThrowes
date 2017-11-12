using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;

public class FunnyKnifeAdsManager : MonoBehaviour
{

    public bool isLoadInt = false;

    public RewardBasedVideoAd rewardBasedVideo;

    public static FunnyKnifeAdsManager Instance;

    public Action OnVideoRewared;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    void Start()
    {

        //Required
        InitializeActions();
        UM_AdManager.Init();

        InitRewardVideoAds();
    }

    public void InitRewardVideoAds()
    {
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

        this.RequestRewardedVideo();
    }

    private void RequestRewardedVideo()
    {
#if UNITY_ANDROID
        //ca-app-pub-3940256099942544/5224354917
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }

    public void ShowVideoReward(Action callback)
    {
        OnVideoRewared = callback;
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
        else
        {
            ModelHandle.Instance.ActivePanelAdsmobUn(true);
            RequestRewardedVideo();
        }
    }

    public void StartInterstitialAd()
    {
        InitializeActions();
        UM_AdManager.StartInterstitialAd();
    }

    public void LoadInterstitialAd()
    {
        InitializeActions();
        UM_AdManager.LoadInterstitialAd();
        isLoadInt = true;
        Debug.Log("LoadInterstitialAd " + UM_AdManager.IsInited);

    }

    public void ShowInterstitialAd()
    {
        Debug.Log("ShowInterstitialAd");
        UM_AdManager.ShowInterstitialAd();
        isLoadInt = false;
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        if (OnVideoRewared != null)
        {
            OnVideoRewared();
        }

        RequestRewardedVideo();
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    void InitializeActions()
    {
        UM_AdManager.ResetActions();
        UM_AdManager.OnInterstitialLoaded += HandleOnInterstitialLoaded;
        UM_AdManager.OnInterstitialLoadFail += HandleOnInterstitialLoadFail;
        UM_AdManager.OnInterstitialClosed += HandleOnInterstitialClosed;
        if (!UM_AdManager.IsInited)
        {
            UM_AdManager.Init();
        }
    }

    void HandleOnInterstitialClosed()
    {
        Debug.Log("Interstitial Ad was closed");
        UM_AdManager.OnInterstitialClosed -= HandleOnInterstitialClosed;
        ModelHandle.Instance.HandleLoseGame();

        InitializeActions();
        LoadInterstitialAd();
    }

    void HandleOnInterstitialLoadFail()
    {
        Debug.Log("Interstitial is failed to load");

        UM_AdManager.OnInterstitialLoaded -= HandleOnInterstitialLoaded;
        UM_AdManager.OnInterstitialLoadFail -= HandleOnInterstitialLoadFail;
        UM_AdManager.OnInterstitialClosed -= HandleOnInterstitialClosed;
        isLoadInt = false;
    }

    void HandleOnInterstitialLoaded()
    {
        Debug.Log("Interstitial ad content ready");

        UM_AdManager.OnInterstitialLoaded -= HandleOnInterstitialLoaded;
        UM_AdManager.OnInterstitialLoadFail -= HandleOnInterstitialLoadFail;
        isLoadInt = true;
    }
}
