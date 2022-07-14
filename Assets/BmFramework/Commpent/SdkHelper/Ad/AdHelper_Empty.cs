using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.Sdk.Helper
{
    public class AdHelper_Empty : Object, IAdCommpent
    {
        public IAdHelperCallback adHelperCallback { get => _adHelperCallback; set => _adHelperCallback = value; }
        IAdHelperCallback _adHelperCallback;
        
        public void Init()
        {

        }

        public void BeginLoad()
        {
           
        }

        public void SetGDPRFlag(bool isConsent)
        {

        }

        public bool IsRewardReady()
        {
            return true;
        }

        public bool IsInterstitialReady()
        {
            return true;
        }
        public void ShowBanner()
        {

        }
        public void HideBanner()
        {

        }
        public void ShowInterstitial()
        {
            adHelperCallback.Interstitial(AdType.Interstitial, AdStatus.Playing);
            adHelperCallback.Interstitial(AdType.Interstitial, AdStatus.Close);
        }
        public void ShowRewardVideo()
        {
            adHelperCallback.RewardedVideo(AdType.RewardVideo, AdStatus.Playing);
            adHelperCallback.RewardedVideo(AdType.RewardVideo, AdStatus.Reward);
        }

        public string GetSdkName(AdType adType)
        {
            return "empty";
        }
    }
}
