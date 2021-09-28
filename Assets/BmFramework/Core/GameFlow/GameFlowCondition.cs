using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public enum ConditionType
    {
        less,
        equal,
        greater,
        not_equal,
    }

    public class GameFlowCondition
    {
        public List<string> keys;
        public List<ConditionType> types;
    }
}
