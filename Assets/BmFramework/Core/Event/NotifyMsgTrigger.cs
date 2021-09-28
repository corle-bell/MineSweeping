using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BmFramework.Core
{
    [System.Serializable]
    public struct NotifyMsgTriggerData
    {
        public string key;
        public UnityEvent callback;
    }

    public class NotifyMsgTrigger : MonoBehaviour
    {
        [HideInInspector]
        public UnityEvent startEvent;

        [EnumName("类型")]
        public NotifyType type;


        public List<NotifyMsgTriggerData> eventArr = new List<NotifyMsgTriggerData>();
        // Start is called before the first frame update
        void Start()
        {
            startEvent?.Invoke();
            NotifacitionManager.AddObserver(type, NotifacitionMsg);
        }

        private void OnDestroy()
        {
            NotifacitionManager.RemoveObserver(type, NotifacitionMsg);
        }

        void NotifacitionMsg(NotifyEvent _event)
        {
            foreach (var item in eventArr)
            {
                if (item.key == _event.Cmd)
                {
                    item.callback?.Invoke();
                }
            }
        }
    }
}

