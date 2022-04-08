using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SDK_TalkingData

namespace Bm.Sdk.Helper
{
    public class AnalysisHelper_TalkingData : AnalysisBaseHelper
    {
        const string key = "519005C9213C43698F8E14C84C0CFF1A";
        public override void OnApplication(bool isPause)
        {

        }

      
        public override void Init()
        {
            TalkingDataSDK.BackgroundSessionEnabled();            
            TalkingDataSDK.Init(key, SdkHelper.ChannelData.channel, "");            
        }
        public override void OnEvent(string _event, string _flag, string _params)
        {
            Dictionary<string, object> tmp = new Dictionary<string, object>();
            tmp.Add(_flag, _params);
            TalkingDataSDK.OnEvent(_event, 0, tmp);
        }

        public override void OnEvent(string _event, Dictionary<string, object> _params)
        {
            TalkingDataSDK.OnEvent(_event, 0, _params);
        }

        public override void OnLevelBegin(string _levelName)
        {
            TalkingDataSDK.OnPageBegin(_levelName);
        }

        public override void OnLevelComplete(string _levelName)
        {
            TalkingDataSDK.OnPageEnd(_levelName);
        }

        public override void OnLevelFail(string _levelName, string _reason)
        {
            //TalkingDataSDK.OnPageEnd(_levelName, _reason);
        }

        public override void PlayLevel(int _level)
        {
            //account.SetLevel(_level);
        }

        public override void OnAdEvent(AdType adType, AdStatus _status, string _place, string _sdk, string _info)
        {
            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("Lv", _info);
            _params.Add("Placement", _place);

            this.OnEvent(string.Format("{0}_{1}", adType.ToString(), GetStringByAdStatus(_status)), _params);
        }

        private string GetStringByAdStatus(AdStatus adStatus)
        {
            switch (adStatus)
            {
                case AdStatus.Close:
                    return "End";
                case AdStatus.Reward:
                    return "End";
                case AdStatus.Playing:
                    return "Start";
            }
            return "Undefine";
        }
    }
}

#endif


