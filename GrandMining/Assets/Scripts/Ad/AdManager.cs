using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
public class AdManager : MonoBehaviour
{

    private RewardBasedVideoAd rewardBasedVideo;
    private string adUnitId;
    public Button digButt;

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        rewardBasedVideo = RewardBasedVideoAd.Instance;
        LoadRewardBasedAd();
        //rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        rewardBasedVideo.OnAdRewarded += HandleOnAdRewared;
        //rewardBasedVideo.OnAdRewarded += HandleOnAdClosed;
        //this.RequestRewardBasedVideo();
    }
    /*private void HandleInitCompleteAction(InitializationStatus initstatus)
    {
        // Callbacks from GoogleMobileAds are not guaranteed to be called on
        // main thread.
        // In this example we use MobileAdsEventExecutor to schedule these calls on
        // the next Update() loop.
        MobileAdsEventExecutor.ExecuteInUpdate(() => {
            //statusText.text = "Initialization complete";
            RequestRewardBasedVideo();
        });
    }*/

    private void RequestRewardBasedVideo()
    {
        #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-9108305760173781/7989736350";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-9108305760173781/9151774207";
        #elif UNITY_EDITOR
            string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        #endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }

    public void LoadRewardBasedAd()
    {
        #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-9108305760173781/7989736350";
        #elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-9108305760173781/9151774207";
        #elif UNITY_EDITOR
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        #endif

        rewardBasedVideo.LoadAd(new AdRequest.Builder().Build(), adUnitId);
    
    }

    public event EventHandler<EventArgs> OnAdRewarded;
    //public event EventHandler<EventArgs> OnAdClosed;

    public void HandleOnAdRewared(object sender, Reward args)
    {
        MonoBehaviour.print(String.Format("You just got {0} {1}!", args.Amount, args.Type));
        double amount = args.Amount;
        int amountInint = (int)amount;
        DataStore.data.gemAmount = DataStore.data.gemAmount + amountInint;
        DataStore.data.digAmount = DataStore.data.digAmount + 3;
        RequestRewardBasedVideo();
        LoadRewardBasedAd();
    }

    /*public void HandleOnAdClosed(object sender, Reward args)
    {
        LoadRewardBasedAd();
    }*/

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        int amountInint = (int)amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
        print("User rewarded with: " + amount.ToString() + " " + type);
        Manger.digRiward = true;
        DataStore.data.gemAmount = DataStore.data.gemAmount + amountInint;
        DataStore.data.digAmount = DataStore.data.digAmount + 1;
        if (DataStore.data.digAmount > 0)
            digButt.interactable = true;
            
        this.RequestRewardBasedVideo();

    }
    public void UserOptToWatchAd()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
