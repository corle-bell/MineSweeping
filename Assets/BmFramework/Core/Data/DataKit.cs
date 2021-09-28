using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    internal class DataKit : FrameworKit
    {
        DataManager dataManager;
        internal override void OnInit()
        {
            DataManager.instance = ManagerHelper.AcitveManager<DataManager>(FrameworkMain.instance.transform);
            dataManager = DataManager.instance;
            dataManager.Create();
        }

        internal override void OnUpdate(float deltaTime)
        {

        }

        internal override void OnDestory()
        {

        }
    }
}
