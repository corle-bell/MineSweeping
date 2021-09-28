using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    internal abstract class FrameworKit
    {
        internal abstract void OnInit();
        internal abstract void OnUpdate(float deltaTime);
        internal abstract void OnDestory();
    }
}

