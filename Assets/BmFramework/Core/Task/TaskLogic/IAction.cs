using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public interface IAction
    {
        void Update();
        void Init();
        void Destory();
    }
}

