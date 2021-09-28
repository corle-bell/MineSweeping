using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;	

namespace Bm
{
	[Serializable]
	public class AnimationFrameFun
	{
		[EnumName("动画名称:")]
		public string name;

		[EnumName("事件插入时间(S):")]		
		public float progress;

		[Serializable]
        public class AnimtionFrameFun:UnityEvent{ }
 
        [FormerlySerializedAs("AnimtionFrameFun")]
        [SerializeField]

        public AnimtionFrameFun mEvent;
	}
	

	public class AnimationFrameEventController : MonoBehaviour
	{
		public AnimationFrameFun[] events;

		[EnumName("动画控制器:")]
		public Animator animator;

		[EnumName("是否采用百分比计算:")]
		public bool isPercent=true;

		[EnumName("是否为队列响应:")]
		public bool isQueue = true;

		public bool isDebug = false;
		private int index = 0;
		private void Awake()
		{
			if (this.events != null)
			{
				for (int i = 0; i < this.events.Length; i++)
				{
					AnimationClip animationClip = this.GetAnimationClip(this.events[i].name);
					if (animationClip != null)
					{
						/*if (isQueue)
                        {
							int[] slip = new int[] {GetInstanceID(), i};
							animationClip.AddEvent(new AnimationEvent
							{
								functionName = "FrameFunEvent",
								time = isPercent ? (animationClip.length * this.events[i].progress) : this.events[i].progress,
								stringParameter = BmTools.IntArrayToString(slip),
							});
						}
						else
                        {
							animationClip.AddEvent(new AnimationEvent
							{
								functionName = "FrameFunEventId",
								time = isPercent ? (animationClip.length * this.events[i].progress) : this.events[i].progress,
								intParameter = i
							});
						}*/

						int[] slip = new int[] { GetInstanceID(), i };
						animationClip.AddEvent(new AnimationEvent
						{
							functionName = "FrameFunEvent",
							time = isPercent ? (animationClip.length * this.events[i].progress) : this.events[i].progress,
							stringParameter = FrameworkTools.IntArrayToString(slip),
						});
					}
				}
			}
		}

		private void FrameFunEventId(int id)
        {
			if (this.events[id].mEvent != null)
			{
				this.events[id].mEvent.Invoke();
			}
		}

		private void Log(string _string)
        {
#if BM_DEBUG
			if(isDebug)
            {
				Debug.Log(_string);
            }
#endif
		}

		public void FrameFunEvent(string _data)
		{
			Log("FrameFunEvent: "+ _data);

			int[] data = FrameworkTools.stringToIntArray(_data);
			if (data[0]== GetInstanceID())
			{
				this.events[data[1]].mEvent.Invoke();
			}
		}

		private void EventCall()
        {
			if (index < this.events.Length)
			{
				if (this.events[index].mEvent != null)
				{
					this.events[index].mEvent.Invoke();
				}
				index++;
				if (index >= this.events.Length)
				{
					index = 0;
				}
			}
		}

		private AnimationClip GetAnimationClip(string name)
		{
			AnimationClip[] animationClips = this.animator.runtimeAnimatorController.animationClips;
			for (int i = 0; i < animationClips.Length; i++)
			{
				if (animationClips[i].name == name)
				{
					return animationClips[i];
				}
			}
			return null;
		}

        private void OnDestroy()
        {
			if (this.events != null)
			{
				for (int i = 0; i < this.events.Length; i++)
				{
					AnimationClip animationClip = this.GetAnimationClip(this.events[i].name);
					if (animationClip != null)
					{
						animationClip.events = null;
					}
				}
			}
		}

        public void Reset()
        {
			animator = GetComponent<Animator>();
        }
    }
}
