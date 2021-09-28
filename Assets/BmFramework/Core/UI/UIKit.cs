using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    internal class UIKit : FrameworKit
    {
        UIManager manager;
        UITostaManager tostaManager;
        internal override void OnInit()
        {
            UIManager.instance = ManagerHelper.AcitveManager<UIManager>(FrameworkMain.instance.transform);
            manager = UIManager.instance;
            manager.Init();

            UITostaManager.instance = ManagerHelper.AcitveManager<UITostaManager>(FrameworkMain.instance.transform);
            tostaManager = UITostaManager.instance;
            tostaManager.Init();
        }

        internal override void OnUpdate(float deltaTime)
        {
            manager.Logic();
        }

        internal override void OnDestory()
        {

        }
    }
}

