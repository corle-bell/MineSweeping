using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SDK_FB

using Facebook.Unity;

namespace Bm.Sdk.Helper
{
    public class AnalysisHelper_FaceBook : AnalysisBaseHelper
    {
        public const string player_level = "player_level";
        public const string mission_status = "status";
        public const string mission_status_begin = "begin";
        public const string mission_status_complete = "complete";
        public const string mission_status_fail = "fail";

        bool isInit;
        public override void Init()
        {
            
        }

        public override void OnApplication(bool isPause)
        {
            if (!isPause)
            {
                if (FB.IsInitialized)
                {
                    FB.ActivateApp();
                }
                else if (!isInit)
                {
                    InitFacebook();
                }
            }
            else
            {
                isInit = false;
            }
        }

        public void InitFacebook()
        {
            if (isInit) return;
            isInit = true;

            //Handle FB.Init
            FB.Init(() => {

                Debug.Log("FaceBook Init  Over");

                FB.ActivateApp();
            });
        }

        public override void OnEvent(string _event, string _flag, string _params)
        {
            var parameters = new Dictionary<string, object>();
            parameters[_flag] = _params;
            FB.LogAppEvent(
                _event,
                1,
                parameters
            );
        }

        public override void OnEvent(string _event, Dictionary<string, object> _params)
        {
            FB.LogAppEvent(
                _event,
                1,
                _params
            );
        }

        public override void OnLevelBegin(string _levelName)
        {
            var parameters = new Dictionary<string, object>();
            parameters[AppEventParameterName.Level] = _levelName;
            parameters[mission_status] = mission_status_begin;
            FB.LogAppEvent(
                AppEventName.AchievedLevel,
                1,
                parameters
            );
        }

        public override void OnLevelComplete(string _levelName)
        {
            var parameters = new Dictionary<string, object>();
            parameters[AppEventParameterName.Level] = _levelName;
            parameters[mission_status] = mission_status_complete;
            FB.LogAppEvent(
                AppEventName.AchievedLevel,
                1,
                parameters
            );
        }

        public override void OnLevelFail(string _levelName, string _reason)
        {
            var parameters = new Dictionary<string, object>();
            parameters[AppEventParameterName.Level] = _levelName;
            parameters[mission_status] = mission_status_fail;
            parameters["reason"] = _reason;
            FB.LogAppEvent(
                AppEventName.AchievedLevel,
                1,
                parameters
            );
        }

        public override void PlayLevel(int _level)
        {
            var parameters = new Dictionary<string, object>();
            parameters[player_level] = _level;
            FB.LogAppEvent(
                player_level,
                1,
                parameters
            );
        }
        public override void OnAdEvent(AdType adType, AdStatus _status, string _place, string _sdk, string _info)
        {

        }

        public override void OnGDPRSet(bool isAgree)
        {

        }

    }
}

#endif


