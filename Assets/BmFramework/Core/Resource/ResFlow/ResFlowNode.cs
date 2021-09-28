using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    [System.Serializable]
    public class ResFlowNode
    {
        [EnumName("要加载的数量")]
        public int count;
        [EnumName("要加载的路径")]
        public string path;
        [EnumName("单帧实例化个数")]
        public int frameLoad;
        [EnumName("是否加入缓存池")]
        public bool isPoolNode;
    }
}
