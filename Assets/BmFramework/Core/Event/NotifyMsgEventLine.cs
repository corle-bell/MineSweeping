using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.Playables;
using UnityEngine.Events;

namespace BmFramework.Core
{
	

	public class NotifyMsgEventLine: MonoBehaviour
	{
		[HideInInspector]
		public List<NotifyMsgEventLineNode> events = new List<NotifyMsgEventLineNode>();

		[EnumName("时间轴总长:")]
		public float duration=1.0f;

		[EnumName("进度:")]
		public float percent;

		[EnumName("运行时间:")]
		public float time;

		[EnumName("是否忽略TimeScale:")]
		public bool ignoreTimeScale=false;

		[EnumName("自动运行:")]
		public bool autoStart=true;

		private int status = 0;


        private void Start()
        {
			if(autoStart) Play();
		}

		public void Play()
        {
			percent = 0;
			time = 0;
			Clear();
			status = 1;
		}

		void End()
		{
			status = 0;
		}

		private void Update()
        {
            switch(status)
            {
				case 1:
					_Update();
					break;
            }
        }

		public void _Update()
		{
			float deltaTime = ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
			time += deltaTime;
			percent = (float)(time / duration);

			if (percent >= 1)
			{
				percent = 1;
				End();
			}
			ExecEvent(percent);
		}

		protected void ExecEvent(float _percent)
		{
			if (!Application.isPlaying) return;

			for (int i = 0; i < events.Count; i++)
			{
				if (_percent >= events[i].progress && !events[i].isDo)
				{
					events[i].isDo = true;
					events[i].mEvent?.Invoke();
					if(events[i].cmd!=NotifyType.Msg_None) NotifacitionManager.Post(events[i].cmd, events[i].sender, events[i].SubCmd);
				}
			}
		}

		private void Clear()
        {
			for (int i = 0; i < events.Count; i++)
			{
				events[i].isDo = false;
			}
		}


		public void AddEventByTime(string _name, float _time, UnityAction _event = null)
        {
			AddEventByPercent(_name, _time /duration, _event);
		}

		public void AddEventByPercent(string _name, float _progress, UnityAction _event=null)
		{
			var t = new NotifyMsgEventLineNode();
			t.name = _name;
			t.progress = _progress;
			if(_event!=null)
            {
				t.mEvent = new UnityEvent();
				t.mEvent.AddListener(_event);
			}
			events.Add(t);
		}
	}
}
