using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public class GameFlowBase : MonoBehaviour
    {
        public List<GameFlowBase> nextFlow = new List<GameFlowBase>();
        public List<GameFlowCondition> nextFlowCondition = new List<GameFlowCondition>();


        public void Begin()
        {

        }


        public void End()
        {

        }
    }
}
