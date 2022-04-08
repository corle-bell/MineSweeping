using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.Sdk.Helper
{
    public abstract class SdkBaseHelper
    {
        public abstract void Init();
        public abstract void OnApplication(bool isPause);
    }

    public class SdkHelper : MonoBehaviour
    {
        public static SdkHelper instance = null;

        public static AnalysisHelper Analysis;
        public static AdHelper Ad;
        public static IapHelper Iap;

        public static Sdk_ChannelData ChannelData;

        public bool isInit = false;

        
        // Start is called before the first frame update
        void Awake()
        {
            isInit = false;
            instance = this;
            DontDestroyOnLoad(gameObject);

            InitHelpers();
        }


        public void InitHelpers()
        {
            if (isInit) return;
            isInit = true;


            ChannelData = new Sdk_ChannelData();
            ChannelData.channel = NativeHelper.GetAppVersion();

            Analysis = new AnalysisHelper();
            Analysis.Init();

            Ad = new AdHelper();
            Ad.Init();

            Iap = new IapHelper();
            Iap.Init();

        }

        public void OnApplicationPause(bool isPause)
        {
            InitHelpers();

            Analysis.OnApplication(isPause);
            Ad.OnApplication(isPause);
            Iap.OnApplication(isPause);
        }
    }

#if UNITY_EDITOR
    public class SdkHelperEditor
    {
        [UnityEditor.MenuItem("Assets/SdkHelper/Create", priority = 0)]
        public static void FrameworkCreate()
        {
            GameObject obj = new GameObject("SdkHelper");
            obj.AddComponent<SdkHelper>();
            UnityEditor.Undo.RegisterCreatedObjectUndo(obj, "SdkHelper");

            

            var t = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene();
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(t);
        }

    }
#endif
}



