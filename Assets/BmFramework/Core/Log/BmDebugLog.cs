using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    internal sealed class BmDebugLog : BmBaseLog
    {
        internal override void Error(object message)
        {
            Debug.Log(BmDebug.Format("Error", message, "#00ff00"));
        }

        internal override void Info(object message)
        {
            Debug.Log(BmDebug.Format("Info", message, "#00ff00"));
        }

        internal override void Warning(object message)
        {
            Debug.Log(BmDebug.Format("Warning", message, "#ffff00"));
        }
    }
}

