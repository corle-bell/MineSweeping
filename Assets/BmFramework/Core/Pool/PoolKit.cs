using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    internal class PoolKit : FrameworKit
    {
        PoolManager poolManager;
        internal override void OnInit()
        {
            PoolManager.instance = ManagerHelper.AcitveManager<PoolManager>(FrameworkMain.instance.transform);
            poolManager = PoolManager.instance;
        }

        internal override void OnUpdate(float deltaTime)
        {
        }

        internal override void OnDestory()
        {

        }
    }
}