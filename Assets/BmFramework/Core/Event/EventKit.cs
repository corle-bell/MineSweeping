using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    internal class EventKit : FrameworKit
    {
        internal override void OnInit()
        {
            NotifacitionManager.getInstance();
        }

        internal override void OnUpdate(float deltaTime)
        {

        }

        internal override void OnDestory()
        {

        }
    }
}
