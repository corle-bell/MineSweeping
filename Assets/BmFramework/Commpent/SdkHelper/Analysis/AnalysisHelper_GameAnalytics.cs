using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SDK_GA
using GameAnalyticsSDK;
namespace Bm.Sdk.Helper
{
    public class AnalysisHelper_GameAnalytics : AnalysisBaseHelper
    {
        public override void OnApplication(bool isPause)
        {

        }

       
        public override void Init()
        {
            GameAnalytics.Initialize();
        }
        public override void OnEvent(string _event, string _flag, string _params)
        {
            GameAnalytics.NewDesignEvent(_event, 1);
        }

        public override void OnEvent(string _event, Dictionary<string, object> _params)
        {
            GameAnalytics.NewDesignEvent(_event, 1);
        }

        public override void OnLevelBegin(string _levelName)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, _levelName);
        }

        public override void OnLevelComplete(string _levelName)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, _levelName, 1);
        }

        public override void OnLevelFail(string _levelName, string _reason)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, _levelName, 1);
        }

        public override void PlayLevel(int _level)
        {
            
        }

        public override void OnGDPRSet(bool isAgree)
        {

        }

        public override void OnAdEvent(AdType adType, AdStatus _status, string _place, string _sdk, string _info)
        {
            GameAnalytics.NewAdEvent(GetAction(_status), GetType(adType), _sdk, _place);
        }

        private GAAdType GetType(AdType _type)
        {
            switch (_type)
            {
                case AdType.Banner:
                    return GAAdType.Banner;
                case AdType.Interstitial:
                    return GAAdType.Interstitial;
                case AdType.RewardVideo:
                    return GAAdType.RewardedVideo;
            }
            return GAAdType.Undefined;
        }

        private GAAdAction GetAction(AdStatus _status)
        {
            switch (_status)
            {
                case AdStatus.Playing:
                    return GAAdAction.Show;
                case AdStatus.PlayFail:
                    return GAAdAction.FailedShow;
                case AdStatus.Loaded:
                    return GAAdAction.Loaded;
                case AdStatus.Click:
                    return GAAdAction.Clicked;
            }
            return GAAdAction.Undefined;
        }
    }
}

#endif


