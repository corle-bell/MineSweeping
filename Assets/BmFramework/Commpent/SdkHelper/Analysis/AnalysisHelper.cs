using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.Sdk.Helper
{
    public abstract class AnalysisBaseHelper
    {
        public abstract void Init();

        public abstract void OnApplication(bool isPause);

        public abstract void OnEvent(string _event, string _flag, string _params);

        public abstract void OnLevelBegin(string _levelName);

        public abstract void OnLevelComplete(string _levelName);

        public abstract void OnLevelFail(string _levelName, string _reason);

        public abstract void PlayLevel(int _level);
    }

    public class AnalysisHelper : SdkBaseHelper
    {
        List<AnalysisBaseHelper> analyses = new List<AnalysisBaseHelper>();
        public override void Init()
        {

            //create the analysises
#if SDK_FB
            analyses.Add(new AnalysisHelper_FaceBook());
#endif

#if SDK_Crazylabs
            analyses.Add(new AnalysisHelper_Crazylab());
#endif




            foreach (var item in analyses)
            {
                item.Init();
            }
        }

        public override void OnApplication(bool isPause)
        {
            foreach (var item in analyses)
            {
                item.OnApplication(isPause);
            }
        }

        public void OnEvent(string _event, string _flag, string _params)
        {
            foreach (var item in analyses)
            {
                item.OnEvent(_event, _flag, _params);
            }
        }

        public void OnLevelBegin(string _levelName)
        {
            foreach (var item in analyses)
            {
                item.OnLevelBegin(_levelName);
            }
        }

        public void OnLevelComplete(string _levelName)
        {
            foreach (var item in analyses)
            {
                item.OnLevelComplete(_levelName);
            }
        }

        public void OnLevelFail(string _levelName, string _reason)
        {
            foreach (var item in analyses)
            {
                item.OnLevelFail(_levelName, _reason);
            }
        }

        public void PlayLevel(int _level)
        {
            foreach (var item in analyses)
            {
                item.PlayLevel(_level);
            }
        }
    }
}
