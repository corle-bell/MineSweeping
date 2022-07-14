using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SDK_Tenjin

namespace Bm.Sdk.Helper
{
    public class AnalysisHelper_Tenjin : AnalysisBaseHelper
    {
        public int first_login_time = 0;
        BaseTenjin instance;
        public override void OnApplication(bool isPause)
        {
            if(!isPause)
            {
                Init();
            }
        }

        public override void Init()
        {
            instance = Tenjin.getInstance("HJNVUOAPZKVFHZ1APSY7BYY1SZPMNMRT");
            instance.SetAppStoreType(AppStoreType.googleplay);
            instance.Connect();

            first_login_time = StoreTools.GetInt("First_Login", first_login_time);

            if (first_login_time==0)
            {
                first_login_time = TimeTools.TimeStampSnd();
                StoreTools.SetInt("First_Login", first_login_time);
            }
            
        }
        public override void OnEvent(string _event, string _flag, string _params)
        {
           
        }

        public override void OnEvent(string _event, Dictionary<string, object> _params)
        {
            
        }

        public override void OnLevelBegin(string _levelName)
        {
            
        }

        public override void OnLevelComplete(string _levelName)
        {
            int now = TimeTools.TimeStampSnd();
            if(now - first_login_time< 86400) //Day 0
            {
                               
                switch (GameDefine.level_progress)
                {
                    case 4:
                    case 9:
                        var eventName = GameHelper.LevelFormat(GameDefine.level_progress + 1);
                        instance?.SendEvent($"LevelComplete_{eventName}", eventName);
                        break;
                }
            }
        }

        public override void OnLevelFail(string _levelName, string _reason)
        {
            
        }

        public override void PlayLevel(int _level)
        {
           
        }

        public override void OnGDPRSet(bool isAgree)
        {
            if(!isAgree)
            {
                instance.OptOut();
            }
            else
            {
                instance.OptIn();
            }
        }

        public override void OnAdEvent(AdType adType, AdStatus _status, string _place, string _sdk, string _info)
        {

        }
    }
}

#endif


