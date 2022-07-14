using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeTools 
{
    static readonly DateTime utcStart = new DateTime(1970, 1, 1);
    public static ulong TimeStampMilliseconds()
    {
        TimeSpan ts = DateTime.UtcNow - utcStart;
        return (ulong)ts.TotalMilliseconds;
    }

    public static int TimeStampSnd()
    {
        TimeSpan ts = DateTime.UtcNow - utcStart;
        return (int)ts.TotalSeconds;
    }
}
