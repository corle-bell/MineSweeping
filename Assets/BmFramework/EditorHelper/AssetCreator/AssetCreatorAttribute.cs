using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.EditorTool
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public sealed class AssetCreatorAttribute : System.Attribute
    {
        public string Title = "2";
    }
}