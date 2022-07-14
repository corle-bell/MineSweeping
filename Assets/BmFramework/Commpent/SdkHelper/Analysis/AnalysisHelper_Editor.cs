using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

namespace Bm.Sdk.Helper
{
    public class AnalysisHelper_Editor : AnalysisBaseHelper
    {
        public override void OnApplication(bool isPause)
        {
            if(!isPause)
            {
                Init();
            }
        }

        
        public override void Init()
        {
            //Log("Init");
        }
        public override void OnEvent(string _event, string _flag, string _params)
        {
            Log("OnEvent", _event, _flag, _params);
        }

        public override void OnEvent(string _event, Dictionary<string, object> _params)
        {
            Log("OnEvent", _event, "Dict", _params.ToString());
        }

        public override void OnLevelBegin(string _levelName)
        {
            Log("OnLevelBegin", _levelName);
        }

        public override void OnLevelComplete(string _levelName)
        {
            Log("OnLevelComplete", _levelName);
        }

        public override void OnLevelFail(string _levelName, string _reason)
        {
            Log("OnLevelFail", _levelName, _reason);
        }

        public override void PlayLevel(int _level)
        {
           
        }

        public override void OnGDPRSet(bool isAgree)
        {

        }

        public override void OnAdEvent(AdType adType, AdStatus _status, string _place, string _sdk, string _info)
        {
            Log("OnAdEvent", adType.ToString(), _status.ToString(), _place, _sdk, _info);
        }

        void Log(string _s0)
        {
            BmFramework.Core.BmDebug.Info(string.Format("{0} []", _s0));
        }

        void Log(string _s0, string _s1)
        {
            BmFramework.Core.BmDebug.Info(string.Format("{0} [{1}]", _s0, _s1));
        }

        void Log(string _s0, string _s1, string _s2)
        {
            BmFramework.Core.BmDebug.Info(string.Format("{0} [{1}, {2}]", _s0, _s1, _s2));
        }

        void Log(string _s0, string _s1, string _s2, string _s3)
        {
            BmFramework.Core.BmDebug.Info(string.Format("{0} [{1}, {2}, {3}]", _s0, _s1, _s2, _s3));
        }

        void Log(string _s0, string _s1, string _s2, string _s3, string _s4)
        {
            BmFramework.Core.BmDebug.Info(string.Format("{0} [{1}, {2}, {3}, {4}]", _s0, _s1, _s2, _s3, _s4));
        }

        void Log(string _s0, string _s1, string _s2, string _s3, string _s4, string _s5)
        {
            BmFramework.Core.BmDebug.Info(string.Format("{0} [{1}, {2}, {3}, {4}, {5}]", _s0, _s1, _s2, _s3, _s4, _s5));
        }
    }
}

#endif


