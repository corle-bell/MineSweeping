using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SDK_Tenjin

namespace Bm.Sdk.Helper
{
    public class AnalysisHelper_Tenjin : AnalysisBaseHelper
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
            BaseTenjin instance = Tenjin.getInstance("HJNVUOAPZKVFHZ1APSY7BYY1SZPMNMRT");
            instance.Connect();
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
            
        }

        public override void OnLevelFail(string _levelName, string _reason)
        {
            
        }

        public override void PlayLevel(int _level)
        {
           
        }

        public override void OnAdEvent(AdType adType, AdStatus _status, string _place, string _sdk, string _info)
        {

        }
    }
}

#endif


