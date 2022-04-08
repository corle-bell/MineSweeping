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
        [EnumName("缓存池父级")]
        public Transform poolParent;
        [EnumName("是否随场景销毁")]
        public bool isDestoryPool=true;
        [EnumName("是否实例化")]
        public bool isInstantiate = true;

        public object data;
    }
}
