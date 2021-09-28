using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SDK_Crazylabs
using Tabtale.TTPlugins;

namespace Bm.Sdk.Helper
{
    public class AnalysisHelper_Crazylab : AnalysisBaseHelper
    {
        public override void Init()
        {
            TTPCore.Setup();
        }

        public override void OnApplication(bool isPause)
        {

        }

        public override void OnEvent(string _event, string _flag, string _params)
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
    }
}

#endif


