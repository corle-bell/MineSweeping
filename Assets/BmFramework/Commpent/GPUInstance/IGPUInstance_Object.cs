using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core.Rendering
{
    public interface IGPUInstance_Object
    {
        Vector3 position { set; get; }
        Vector3 scale { set; get; }
        Vector4 color { set; get; }
        Vector4 rotate { set; get; }
    }
}

