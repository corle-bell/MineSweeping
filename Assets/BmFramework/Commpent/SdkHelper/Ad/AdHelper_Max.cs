using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if AD_MAX
namespace Bm.Sdk.Helper
{
    public class AdHelper_Max : MonoBehaviour, IAdCommpent
    {
        public IAdHelperCallback adHelperCallback { get => _adHelperCallback; set => _adHelperCallback = value; }
        IAdHelperCallback _adHelperCallback;


        private const string MaxSdkKey = "_nIDtARQ2QQGw1zbkXrZ-NC3M2UFVsEUdWiUh9wlZK0TM-44VvdUHrXdt1uMt6rUdT-Jhu2FD2kZ0pdWiG_-5W";
        private const string InterstitialAdUnitId = "3a7816698655c4bd";
        private const string RewardedAdUnitId = "3429904d98061a1c";
        private const string BannerAdUnitId = "22407c7ed8afddb0";
        private const string MRecAdUnitId = "ENTER_MREC_AD_UNIT_ID_HERE";


        private bool isMRecShowing;
        private bool isBannerShow;

        private int interstitialRetryAttempt;
        private int rewardedRetryAttempt;

        private int callTimes = 0;

        private string interstitialSdk;
        private string rewardSdk;
#region IAdCommpent
        public void Init()
        {
            Log("MaxSdkHelper Init");

            interstitialSdk = "max";
            rewardSdk = "max";

            MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
            {
                // AppLovin SDK is initialized, configure and start loading ads.
                Log("MAX SDK Initialized");

                InitializeInterstitialAds();
                InitializeRewardedAds();
                InitializeBannerAds();
                //InitializeMRecAds();
            };

            MaxSdk.SetSdkKey(MaxSdkKey);
            MaxSdk.InitializeSdk();

            
        }

        public bool IsRewardReady()
        {
            return MaxSdk.IsRewardedAdReady(RewardedAdUnitId);
        }

        public bool IsInterstitialReady()
        {
            return MaxSdk.IsInterstitialReady(InterstitialAdUnitId);
        }

        public void ShowBanner()
        {
            if (isBannerShow) return;
            isBannerShow = true;
            MaxSdk.ShowBanner(BannerAdUnitId);
        }
        public void HideBanner()
        {
            if (!isBannerShow) return;
            isBannerShow = false;
            MaxSdk.HideBanner(BannerAdUnitId);
        }

        public void ShowInterstitial()
        {
            if (MaxSdk.IsInterstitialReady(InterstitialAdUnitId))
            {
                MaxSdk.ShowInterstitial(InterstitialAdUnitId);
            }
            else
            {
                adHelperCallback.Interstitial(AdType.Interstitial, AdStatus.PlayFail);
            }
        }
        public void ShowRewardVideo()
        {
            if (MaxSdk.IsRewardedAdReady(RewardedAdUnitId))
            {
                callTimes = 0;
                MaxSdk.ShowRewardedAd(RewardedAdUnitId);

                Log("ShowRewardedAd");
            }
            else
            {
                adHelperCallback.RewardedVideo(AdType.RewardVideo, AdStatus.PlayFail);
            }
        }

        public string GetSdkName(AdType adType)
        {
            switch(adType)
            {
                case AdType.Interstitial:
                    return interstitialSdk;
                case AdType.RewardVideo:
                    return rewardSdk;
            }
            return "max";
        }
#endregion


#region Interstitial Ad Methods

        private void InitializeInterstitialAds()
        {
            // Attach callbacks
            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialFailedEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += InterstitialFailedToDisplayEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += InterstitialDisplayEvent;
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialDismissedEvent;

            // Load the first interstitial
            LoadInterstitial();
        }

        void LoadInterstitial()
        {
            MaxSdk.LoadInterstitial(InterstitialAdUnitId);
        }

    
        private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo info)
        {
            Log("Interstitial loaded");

            interstitialSdk = info.NetworkName;

            // Reset retry attempt
            interstitialRetryAttempt = 0;

            adHelperCallback.Interstitial(AdType.Interstitial, AdStatus.Ready);
        }

        private void InterstitialDisplayEvent(string adUnitId, MaxSdkBase.AdInfo info)
        {
            interstitialSdk = info.NetworkName;

            Log("Interstitial loaded");
            adHelperCallback.Interstitial(AdType.Interstitial, AdStatus.Playing);
        }

        private void OnInterstitialFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo info)
        {
            Log("Interstitial failed to load with error code: " + info.Code);
            Log("Interstitial failed to load with error info: " + info.AdLoadFailureInfo);

            // Interstitial ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).

            interstitialRetryAttempt++;
            double retryDelay = Mathf.Pow(2, Mathf.Min(6, interstitialRetryAttempt));

            Invoke("LoadInterstitial", (float)retryDelay);


            adHelperCallback.Interstitial(AdType.Interstitial, AdStatus.Fail);
        }

        private void InterstitialFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo error, MaxSdkBase.AdInfo info)
        {
            // Interstitial ad failed to display. We recommend loading the next ad
            Log("Interstitial failed to display with error code: " + error.Code);
            Log("Interstitial failed to display with error info: " + error.AdLoadFailureInfo);
            LoadInterstitial();

            adHelperCallback.Interstitial(AdType.Interstitial, AdStatus.PlayFail);
        }

        private void OnInterstitialDismissedEvent(string adUnitId, MaxSdkBase.AdInfo info)
        {
            // Interstitial ad is hidden. Pre-load the next ad
            Log("Interstitial dismissed");
            LoadInterstitial();

            adHelperCallback.Interstitial(AdType.Interstitial, AdStatus.Close);
        }

#endregion

#region Rewarded Ad Methods

        private void InitializeRewardedAds()
        {
            // Attach callbacks
            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
            MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdFailedEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
            MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdDismissedEvent;
            MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;


            // Load the first RewardedAd
            LoadRewardedAd();
        }

        private void LoadRewardedAd()
        {
            MaxSdk.LoadRewardedAd(RewardedAdUnitId);
        }

        private void Log(string _text)
        {
            Debug.Log(_text);
        }

        private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo info)
        {
            // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(rewardedAdUnitId) will now return 'true'
            Log(" Rewarded ad loaded");
            rewardSdk = info.NetworkName;
            // Reset retry attempt
            rewardedRetryAttempt = 0;

            adHelperCallback.RewardedVideo(AdType.RewardVideo, AdStatus.Ready);
        }

        private void OnRewardedAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo info)
        {
            Log("Rewarded ad failed to load with error code: " + info.Code);
            Log("Rewarded ad failed to load with error info: " + info.AdLoadFailureInfo);

            // Rewarded ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).

            rewardedRetryAttempt++;
            double retryDelay = Mathf.Pow(2, Mathf.Min(6, rewardedRetryAttempt));

            Invoke("LoadRewardedAd", (float)retryDelay);
        }

        private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo error, MaxSdkBase.AdInfo info)
        {
            // Rewarded ad failed to display. We recommend loading the next ad
            Log("Rewarded ad failed to display with error code: " + error.Code);
            Log("Rewarded ad failed to display with error info: " + error.AdLoadFailureInfo);
            LoadRewardedAd();

            adHelperCallback.RewardedVideo(AdType.RewardVideo, AdStatus.PlayFail);
        }

        private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo info)
        {
            Log("Rewarded ad displayed");
            rewardSdk = info.NetworkName;
            adHelperCallback.RewardedVideo(AdType.RewardVideo, AdStatus.Playing);
        }

        private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo info)
        {
            Log("Rewarded ad clicked");
        }

        private void OnRewardedAdDismissedEvent(string adUnitId, MaxSdkBase.AdInfo info)
        {
            // Rewarded ad is hidden. Pre-load the next ad
            Log("Rewarded ad dismissed");
            LoadRewardedAd();

            OnReward();

            adHelperCallback.RewardedVideo(AdType.RewardVideo, AdStatus.Close);
        }

        private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo info)
        {
            // Rewarded ad was displayed and user should receive the reward
            Log("Rewarded ad received reward");

            OnReward();
        }

        private void OnReward()
        {
            callTimes++;
            if(callTimes>=2)
            {
                adHelperCallback.RewardedVideo(AdType.RewardVideo, AdStatus.Reward);
            }
        }

#endregion

#region Banner Ad Methods

        private void InitializeBannerAds()
        {
            // Banners are automatically sized to 320x50 on phones and 728x90 on tablets.
            // You may use the utility method `MaxSdkUtils.isTablet()` to help with view sizing adjustments.
            MaxSdk.CreateBanner(BannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);

            // Set background or background color for banners to be fully functional.
            MaxSdk.SetBannerBackgroundColor(BannerAdUnitId, Color.white);
        }

 

#endregion

#region MREC Ad Methods

        private void InitializeMRecAds()
        {
            // MRECs are automatically sized to 300x250.
            MaxSdk.CreateMRec(MRecAdUnitId, MaxSdkBase.AdViewPosition.BottomCenter);
        }

        private void ToggleMRecVisibility()
        {
            if (!isMRecShowing)
            {
                MaxSdk.ShowMRec(MRecAdUnitId);
            }
            else
            {
                MaxSdk.HideMRec(MRecAdUnitId);
            }

            isMRecShowing = !isMRecShowing;
        }

#endregion


        int touchTimes = 0;
        private void Update()
        {
            if(Input.touchCount>3)
            {

            }
        }
    }
}
#endif