using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeHelper
{
    public static AndroidJavaClass unityClass;
    public static AndroidJavaClass helperClass;
    public static void Init()
    {
        unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        helperClass = new AndroidJavaClass("com.unity3d.player.NativeBridge");
    }

    public static string GetAppVersion()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.IPhonePlayer:
               return Application.version;
            case RuntimePlatform.Android:
                return Application.version;
        }
        return Application.version;
    }

    public static string GetCountry()
    {
#if UNITY_ANDROID

        AndroidJavaObject _unityContext = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        var ret = helperClass.CallStatic<string>("GetDeviceCountry", _unityContext);
        return ret;
#else
    return "en";
#endif
    }

    public static string GetLanguage()
    {
        Debug.Log("GetLanguage Begin");
#if UNITY_ANDROID
        AndroidJavaObject _unityContext = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        var ret = helperClass.CallStatic<string>("GetDeviceLanguage", _unityContext);
        Debug.Log("GetLanguage Out 1  " + ret);
        return ret;
#else
    Debug.Log("GetLanguage Out 1  " + "");
    return "en";
#endif
    }
}
