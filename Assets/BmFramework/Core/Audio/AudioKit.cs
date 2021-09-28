using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    internal class AudioKit : FrameworKit
    {
        AudioManager manager;
        internal override void OnInit()
        {
            AudioManager.instance = ManagerHelper.AcitveManager<AudioManager>(FrameworkMain.instance.transform);
            manager = AudioManager.instance;
            manager.Init();
        }

        internal override void OnUpdate(float deltaTime)
        {
            manager.Logic(deltaTime);
        }

        internal override void OnDestory()
        {

        }
    }
}
