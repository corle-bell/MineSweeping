using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SDK_Voodoo

using GameAnalyticsSDK;

namespace Bm.Sdk.Helper
{
    public class AnalysisHelper_Voodoo : AnalysisBaseHelper
    {
        public override void Init()
        {
            
        }

        public override void OnApplication(bool isPause)
        {

        }

        public override void OnEvent(string _event, string _flag, string _params)
        {

        }

        public override void OnLevelBegin(string _levelName)
        {
            TinySauce.OnGameStarted(_levelName);
        }

        public override void OnLevelComplete(string _levelName)
        {
            TinySauce.OnGameFinished(true, 1, _levelName);
        }

        public override void OnLevelFail(string _levelName, string _reason)
        {
            TinySauce.OnGameFinished(false, 1, _levelName);
        }

        public override void PlayLevel(int _level)
        {

        }

        public override void OnGDPRSet(bool isAgree)
        {

        }

        public override void OnAdEvent(AdType adType, AdStatus _status, string _place, string _sdk, string _info)
        {

        }
    }
}

#endif


