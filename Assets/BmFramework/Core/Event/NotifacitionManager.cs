using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


namespace BmFramework.Core
{

    // 消息事件类，使用中传递的信息
    public class NotifyEvent : IRef
    {
        public NotifyType Type;
        public string Cmd;
        public System.Object Sender;
        public UnityEngine.Object GameObj;

        public int ParamsInt;
        public float ParamsFloat;
        public string ParamsString;

        public int retainNum=0;
        
        public NotifyEvent()
        {
            Reset();
        }

        // 常用函数
        public override string ToString()
        {
            return Type + " [ " + ((Sender == null) ? "null" : Sender.ToString()) + " ] ";
        }



        public NotifyEvent Clone()
        {
            return new NotifyEvent();
        }

        public void Reset()
        {
            Sender = null;
            Cmd = null;
            Type = NotifyType.Msg_None;
            GameObj = null;
            ParamsInt = 0;
            ParamsFloat = 0;
            ParamsString = null;
            retainNum = 0;
        }

        public void Destroy()
        {

        }

        public NotifyEvent Retain()
        {
            retainNum++;
            return this;
        }

        public bool Release()
        {
            retainNum--;
            return retainNum <= 0;
        }
    }



    // 消息监听者，这是一个delegate，也就是一个函数，当事件触发时，对应注册的delegate就会触发

    public delegate void EventListenerDelegate(NotifyEvent evt);


    // 消息中心

    public class NotifacitionManager
    {
        // 单例
        private static NotifacitionManager instance;
        private NotifacitionManager() { }

        private RefPool eventPool;
        public static NotifacitionManager getInstance()
        {
            if (instance == null)
            {
                instance = new NotifacitionManager();
                instance.eventPool = new RefPool(100);
            }
            return instance;
        }


        // 成员变量
        Dictionary<NotifyType, EventListenerDelegate> notifications = new Dictionary<NotifyType, EventListenerDelegate>(); // 所有的消息

        public static string GetRefPool()
        {
            return instance.eventPool.ToString();
        }

        /// <summary>
        /// 获得一个NotifyEvent
        /// </summary>
        /// <returns></returns>
        public static NotifyEvent AllocateEvent()
        {
            return instance.eventPool.Get<NotifyEvent>().Retain();
        }

        /// <summary>
        /// 释放NotifyEvent
        /// </summary>
        /// <returns></returns>
        public static void FreeEvent(NotifyEvent _event)
        {
            instance.eventPool.Recycle(_event);
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="evt"></param>
        public static void Post(NotifyEvent evt)
        {
            instance.postNotification(evt);
        }
        /// <summary>
        ///发送事件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sender"></param>
        public static void Post(NotifyType type, object sender)
        {
            NotifyEvent t = AllocateEvent();
            t.Type = type;
            t.Sender = sender;

            Post(t);
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="_Cmd"></param>
        public static void Post(NotifyType type, string _Cmd)
        {
            NotifyEvent t = AllocateEvent();
            t.Type = type;
            t.Cmd = _Cmd;

            Post(t);
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="_obj"></param>
        public static void Post(NotifyType type, UnityEngine.Object _obj)
        {
            NotifyEvent t = AllocateEvent();
            t.Type = type;
            t.GameObj = _obj;

            Post(t);
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="_Cmd"></param>
        /// <param name="_obj"></param>
        public static void Post(NotifyType type, string _Cmd, UnityEngine.Object _obj)
        {
            NotifyEvent t = AllocateEvent();
            t.Type = type;
            t.Cmd = _Cmd;
            t.GameObj = _obj;

            Post(t);
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sender"></param>
        /// <param name="_Cmd"></param>
        public static void Post(NotifyType type, object sender, string _Cmd)
        {
            NotifyEvent t = AllocateEvent();
            t.Type = type;
            t.Cmd = _Cmd;
            t.Sender = sender;

            Post(t);
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sender"></param>
        /// <param name="_Cmd"></param>
        /// <param name="_obj"></param>
        public static void Post(NotifyType type, object sender, string _Cmd, string _obj)
        {
            NotifyEvent t = AllocateEvent();
            t.Type = type;
            t.Cmd = _Cmd;
            t.Sender = sender;
            t.ParamsString = _obj;

            Post(t);
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sender"></param>
        /// <param name="_Cmd"></param>
        /// <param name="_params_int"></param>
        public static void Post(NotifyType type, object sender, string _Cmd, int _params_int)
        {
            NotifyEvent t = AllocateEvent();
            t.Type = type;
            t.Cmd = _Cmd;
            t.Sender = sender;
            t.ParamsInt = _params_int;

            Post(t);
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sender"></param>
        /// <param name="_Cmd"></param>
        /// <param name="_params_f"></param>
        public static void Post(NotifyType type, object sender, string _Cmd, float _params_f)
        {
            NotifyEvent t = AllocateEvent();
            t.Type = type;
            t.Cmd = _Cmd;
            t.Sender = sender;
            t.ParamsFloat = _params_f;

            Post(t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sender"></param>
        /// <param name="_Cmd"></param>
        /// <param name="_params_i"></param>
        /// <param name="_params_f"></param>
        public static void Post(NotifyType type, object sender, string _Cmd, int _params_i, float _params_f)
        {
            NotifyEvent t = AllocateEvent();
            t.Type = type;
            t.Cmd = _Cmd;
            t.Sender = sender;
            t.ParamsInt = _params_i;
            t.ParamsFloat = _params_f;

            Post(t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sender"></param>
        /// <param name="_Cmd"></param>
        /// <param name="_params_i"></param>
        /// <param name="_params_f"></param>
        /// <param name="obj"></param>
        public static void Post(NotifyType type, object sender, string _Cmd, int _params_i, float _params_f, UnityEngine.Object obj)
        {
            NotifyEvent t = AllocateEvent();
            t.Type = type;
            t.Cmd = _Cmd;
            t.Sender = sender;
            t.ParamsInt = _params_i;
            t.ParamsFloat = _params_f;
            t.GameObj = obj;

            Post(t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sender"></param>
        /// <param name="_Cmd"></param>
        /// <param name="_params_i"></param>
        /// <param name="_params_f"></param>
        /// <param name="_obj"></param>
        public static void Post(NotifyType type, object sender, string _Cmd, int _params_i, float _params_f, string _obj)
        {
            NotifyEvent t = AllocateEvent();
            t.Type = type;
            t.Cmd = _Cmd;
            t.Sender = sender;
            t.ParamsInt = _params_i;
            t.ParamsFloat = _params_f;
            t.ParamsString = _obj;

            Post(t);
        }

        /// <summary>
        /// 添加监听
        /// </summary>
        /// <param name="type"></param>
        /// <param name="listener"></param>
        public static void AddObserver(NotifyType type, EventListenerDelegate listener)
        {
            instance._AddObserver(type, listener);
        }
        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="type"></param>
        /// <param name="listener"></param>
        public static void RemoveObserver(NotifyType type, EventListenerDelegate listener)
        {
            instance._DelObserver(type, listener);
        }

        // 注册监视

        void _AddObserver(NotifyType type, EventListenerDelegate listener)
        {
            if (listener == null)
            {
                Debug.LogError("registerObserver: listener不能为空");
                return;
            }

            EventListenerDelegate myListener = null;
            notifications.TryGetValue(type, out myListener);
            notifications[type] = (EventListenerDelegate)Delegate.Combine(myListener, listener);
        }



        // 移除监视

        void _DelObserver(NotifyType type, EventListenerDelegate listener)
        {

            if (listener == null)
            {

                Debug.LogError("removeObserver: listener不能为空");
                return;
            }

            try
            {
                EventListenerDelegate t = (EventListenerDelegate)Delegate.Remove(notifications[type], listener);
                if(t==null)
                {
                    notifications.Remove(type);
                }
                else
                {
                    notifications[type] = t;
                }
            }

            catch (System.Exception e)
            {

            }

        }



        public void ClearObserver()
        {
            notifications.Clear();
        }



        // 消息触发

        void postNotification(NotifyEvent evt)
        {
            EventListenerDelegate listenerDelegate;
            if (notifications.TryGetValue(evt.Type, out listenerDelegate))
            {
                try
                {
                    // 执行调用所有的监听者
                    listenerDelegate.Invoke(evt);                    
                }

                catch (System.Exception e)
                {
                    throw new Exception(string.Concat(new string[] { "Error dispatching event", evt.Type.ToString(), ": ", e.Message, " ", e.StackTrace }), e);
                }

            }

            if (evt.Release())
            {
                FreeEvent(evt);
            }

        }
    }
}


