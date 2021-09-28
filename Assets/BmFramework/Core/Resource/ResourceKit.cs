using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    internal class ResourceKit : FrameworKit
    {
        ResManager resManager;
        internal override void OnInit()
        {
            ResManager.instance = ManagerHelper.AcitveManager<ResManager>(FrameworkMain.instance.transform);
            resManager = ResManager.instance;
        }

        internal override void OnUpdate(float deltaTime)
        {
            resManager.Logic();
        }

        internal override void OnDestory()
        {

        }
    }
}
