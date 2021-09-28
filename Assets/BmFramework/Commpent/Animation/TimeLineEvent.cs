using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;	

namespace Bm
{	
	[System.Serializable]
	public struct TimeLineEventNode{
		[EnumName("插入时间(S):")]
		public float time;

		[Serializable]
        public class TimeLineEvent:UnityEvent{ }
 
        [FormerlySerializedAs("TimeLineEvent")]
        [SerializeField]
        public TimeLineEvent mEvent;
	}

	public class TimeLineEvent : MonoBehaviour {
		public TimeLineEventNode [] events;
		private int id;
		private float nextTime;
		private void Awake() {
			id = 0;

			nextTime = events[0].time;
		}

		private void FrameEvent()
		{
			events[id].mEvent.Invoke();
			EndOnce();
		}

		public void Begin()
		{
			id = 0;
			Step();
		}

		private void Step()
		{
			if(nextTime==0)
			{
				FrameEvent();
			}
			else
			{
				Invoke("FrameEvent", nextTime);
			}
		}

		private void EndOnce()
		{
			id ++;
			if(id<events.Length)
			{
				nextTime = events[id].time;
				Step();
			}
		}
	}
}
