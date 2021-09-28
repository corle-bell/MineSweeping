using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.DefineSymbols
{
    [System.Serializable]
    public class DefineSymbolsDataNode
    {
        public bool status;
        public string name;
        public string desc;
    }

    public class DefineSymbolsData : ScriptableObject
    {
        public List<DefineSymbolsDataNode> list = new List<DefineSymbolsDataNode>();
    }
}
