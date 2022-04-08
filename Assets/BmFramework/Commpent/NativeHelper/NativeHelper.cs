using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeHelper
{
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
}
