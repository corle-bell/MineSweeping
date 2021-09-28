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
        public void Post()
        {
            NotifacitionManager.Post(type, this, msg);
        }
    }
}

