using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    internal sealed class BmReleaseLog : BmBaseLog
    {
        internal override void Error(object message)
        {
            Debug.LogError(BmDebug.Format("Error", message));
        }

        internal override void Info(object message)
        {
            
        }

        internal override void Warning(object message)
        {
            Debug.LogWarning(BmDebug.Format("Warning", message));
        }
    }
}