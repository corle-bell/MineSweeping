using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BmFramework.Core
{
    public class NotifyMsgTrigger_SetActive : MonoBehaviour
    {

        [EnumName("开局自动隐藏")]
        public bool autoHide;

        public UnityEvent showEvent;
        // Start is called before the first frame update
        void Start()
        {
            if (autoHide) gameObject.SetActive(false);
            NotifacitionManager.AddObserver(NotifyType.Msg_AcitveCmd_Show, NotifacitionMsgShow);
            NotifacitionManager.AddObserver(NotifyType.Msg_AcitveCmd_Hide, NotifacitionMsgHide);

        }

        private void OnDestroy()
        {
            NotifacitionManager.RemoveObserver(NotifyType.Msg_AcitveCmd_Show, NotifacitionMsgShow);
            NotifacitionManager.RemoveObserver(NotifyType.Msg_AcitveCmd_Hide, NotifacitionMsgHide);
        }

        void NotifacitionMsgHide(NotifyEvent _event)
        {
            if (name == _event.Cmd)
            {
                gameObject.SetActive(false);
            }
        }

        void NotifacitionMsgShow(NotifyEvent _event)
        {
            if (name == _event.Cmd)
            {
                gameObject.SetActive(true);
                showEvent?.Invoke();
            }
        }
    }
}
