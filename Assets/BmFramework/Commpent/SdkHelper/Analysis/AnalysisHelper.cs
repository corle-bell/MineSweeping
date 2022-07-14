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
        public abstract void OnEvent(string _event, Dictionary<string, object> _params);
        public abstract void OnLevelBegin(string _levelName);

        public abstract void OnLevelComplete(string _levelName);

        public abstract void OnLevelFail(string _levelName, string _reason);

        public abstract void PlayLevel(int _level);

        public abstract void OnAdEvent(AdType adType, AdStatus _status, string _place, string _sdk, string _info=null);

        public abstract void OnGDPRSet(bool isAgree);
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

#if SDK_Voodoo
            analyses.Add(new AnalysisHelper_Voodoo());
#endif

#if SDK_GA
            analyses.Add(new AnalysisHelper_GameAnalytics());
#endif
#if SDK_Tenjin
            analyses.Add(new AnalysisHelper_Tenjin());
#endif

#if SDK_TalkingData
            analyses.Add(new AnalysisHelper_TalkingData());
#endif

#if UNITY_EDITOR
            analyses.Add(new AnalysisHelper_Editor());
#endif


            foreach (var item in analyses)
            {
                item.Init();
            }
        }

        public override void OnApplication(bool isPause)
        {
            try
            {
                foreach (var item in analyses)
                {
                    item.OnApplication(isPause);
                }
            }
            catch (System.Exception e)
            {

            }
            
        }

        public void OnEvent(string _event, string _flag, string _params)
        {
            try
            {
                foreach (var item in analyses)
                {
                    item.OnEvent(_event, _flag, _params);
                }
            }
            catch (System.Exception e)
            {

            }
        }

        public void OnLevelBegin(string _levelName)
        {
            try
            {
                foreach (var item in analyses)
                {
                    item.OnLevelBegin(_levelName);
                }
            }
            catch (System.Exception e)
            {

            }
            
        }

        public void OnLevelComplete(string _levelName)
        {
            try
            {
                foreach (var item in analyses)
                {
                    item.OnLevelComplete(_levelName);
                }
            }
            catch (System.Exception e)
            {

            }
            
        }

        public void OnLevelFail(string _levelName, string _reason)
        {
            try
            {
                foreach (var item in analyses)
                {
                    item.OnLevelFail(_levelName, _reason);
                }
            }
            catch (System.Exception e)
            {

            }
        }

        public void PlayLevel(int _level)
        {
            try
            {
                foreach (var item in analyses)
                {
                    item.PlayLevel(_level);
                }
            }
            catch (System.Exception e)
            {

            }
        }

        public void OnGDPRSet(bool isAgree)
        {
            try
            {
                foreach (var item in analyses)
                {
                    item.OnGDPRSet(isAgree);
                }
            }
            catch (System.Exception e)
            {

            }
        }

        public void OnAdEvent(AdType adType, AdStatus _status, string _place, string _sdk, string _info=null)
        {
            try
            {
                foreach (var item in analyses)
                {
                    item.OnAdEvent(adType, _status, _place, _sdk, _info);
                }
            }
            catch (System.Exception e)
            {

            }
        }

    }
}
