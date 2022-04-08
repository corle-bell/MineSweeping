using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public class NotifyMsgSenderAgent : MonoBehaviour
    {
        [EnumName("指令:")]
        public NotifyType type;
        public string msg;
        public Object ParamsObj;
        public int ParamsInt;
        public float ParamsFloat;
        public void Post()
        {
            NotifacitionManager.Post(type, this, msg, ParamsInt, ParamsFloat, ParamsObj);
        }
    }
}

