using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    internal class TaskKit : FrameworKit
    {
        public TaskLogic timerTask;
        internal override void OnInit()
        {
            timerTask = TaskLogic.Instance = new TaskLogic();
            timerTask.checkDelay = FrameworkMain.instance.task_tick;
        }

        internal override void OnUpdate(float deltaTime)
        {
            timerTask.Update();
        }

        internal override void OnDestory()
        {

        }
    }
}
