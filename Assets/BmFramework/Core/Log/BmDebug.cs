using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    [ExecuteInEditMode]
    public static class BmDebug
    {
        internal static BmBaseLog logInstance;

        internal static void Init()
        {
            if(FrameworkMain.instance.isDev)
            {
                logInstance = new BmDebugLog();
            }
            else
            {
                logInstance = new BmReleaseLog();
            }
        }

        internal static string Format(string flag, object message, string _color= "#ffffff")
        {
            return string.Format("<color={0}>[{1}] : {2}</color>", _color, flag, message);
        }

#if UNITY_EDITOR
        public static void Info(object message)
        {
            if(!Application.isPlaying)
            {
                Debug.Log(BmDebug.Format("Info", message, "#00ff00"));
            }
            else
            {
                logInstance.Info(message);
            }
        }
        public static void Warnning(object message)
        {
            if (!Application.isPlaying)
            {
                Debug.Log(BmDebug.Format("Warning", message, "#ffff00"));
            }
            else
            {
                logInstance.Warning(message);
            }
            
        }
        public static void Error(object message)
        {
            if (!Application.isPlaying)
            {
                Debug.Log(BmDebug.Format("Error", message, "#ff0000"));
            }
            else
            {
                logInstance.Error(message);
            }
        }
#else 
        public static void Info(object message)
        {
            logInstance.Info(message);
        }
        public static void Warnning(object message)
        {
            logInstance.Warning(message);
        }
        public static void Error(object message)
        {
            logInstance.Error(message);
        }
#endif
    }
}
