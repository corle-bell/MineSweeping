using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public class BmDebug : MonoBehaviour
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

        internal static string Format(string flag, object message)
        {
            return string.Format("[{0}] : {1}", flag, message);
        }

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
    }
}
