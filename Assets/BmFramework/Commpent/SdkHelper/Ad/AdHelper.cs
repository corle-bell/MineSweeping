using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Bm.Sdk.Helper
{
    public interface IAdCommpent
    {
        IAdHelperCallback adHelperCallback { get; set; }

        void Init();
        bool IsRewardReady();
        bool IsInterstitialReady();
        void ShowBanner();
        void HideBanner();
        void ShowInterstitial();
        void ShowRewardVideo();

        string GetSdkName(AdType adType);
    }

    public interface IAdHelperCallback
    {
        void RewardedVideo(AdType type, AdStatus status);
        void Interstitial(AdType type, AdStatus status);
        void Banner(AdType type, AdStatus status);
    }

    public enum AdType
    {
        Banner,
        Interstitial,
        RewardVideo,
    }

    public enum AdStatus
    {
        Playing,
        Fail,
        Ready,
        Reward,
        Close,
        PlayFail,
        Loaded,
        Click,
    }

    public class AdHelper : SdkBaseHelper, IAdHelperCallback
    {
        IAdCommpent adHelper;

        private UnityAction<AdType, AdStatus> InterstitialCallback;
        private UnityAction<AdType, AdStatus> RewardCallback;

        public override void Init()
        {
            #if AD_MAX
                adHelper = SdkHelper.instance.gameObject.AddComponent<AdHelper_Max>();
                adHelper.Init();
                adHelper.adHelperCallback = this;
            #endif

            if (adHelper==null)
            {
                adHelper = new AdHelper_Empty();
                adHelper.Init();
                adHelper.adHelperCallback = this;
            }
        }

        public override void OnApplication(bool isPause)
        {

        }

        public bool IsRewardReady()
        {
            return adHelper.IsRewardReady();
        }

        public bool IsInterstitialReady()
        {
            return adHelper.IsInterstitialReady();
        }

        public void ShowBanner()
        {
            adHelper.ShowBanner();
        }

        public void HideBanner()
        {
            adHelper.HideBanner();
        }

        public void ShowInterstitial(UnityAction<AdType, AdStatus> callback)
        {
            if(IsInterstitialReady())
            {
                InterstitialCallback = callback;
                adHelper.ShowInterstitial();
            }
        }

        public void ShowRewardVideo(UnityAction<AdType, AdStatus> callback)
        {
            RewardCallback = callback;
            adHelper.ShowRewardVideo();
        }

#region IAdHelperCallback
        public void RewardedVideo(AdType type, AdStatus status)
        {
            RewardCallback?.Invoke(type, status);
        }

        public void Interstitial(AdType type, AdStatus status)
        {
            InterstitialCallback?.Invoke(type, status); 
        }

        public void Banner(AdType type, AdStatus status)
        {
            
        }

        public string GetSdkName(AdType adType)
        {
            return adHelper.GetSdkName(adType);
        }
#endregion
    }
}
