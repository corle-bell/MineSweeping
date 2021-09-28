using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    internal abstract class BmBaseLog
    {
        internal abstract void Info(object message);
        internal abstract void Warning(object message);
        internal abstract void Error(object message);
    }
}

