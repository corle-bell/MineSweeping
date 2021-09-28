using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;	

namespace Bm
{
	[Serializable]
	public struct AnimationFrameEvent
	{
		[EnumName("动画名称:")]
		public string name;

		[EnumName("事件插入时间(S):")]
		public float progress;

		[EnumName("触发节点:")]
		public GameObject effect;

		[EnumName("调用函数:")]
		public string effectFun;

		[EnumName("特效生存时间(S):")]
		public float effectLive;
	}
	

	public class AnimationFrameController : MonoBehaviour
	{
		public AnimationFrameEvent[] events;

		[EnumName("动画控制器:")]
		public Animator animator;

		[EnumName("是否采用百分比计算:")]
		public bool isPercent=false;

		private void Awake()
		{
			if (this.events != null)
			{
				for (int i = 0; i < this.events.Length; i++)
				{
					AnimationClip animationClip = this.GetAnimationClip(this.events[i].name);
					if (animationClip != null)
					{
						animationClip.AddEvent(new AnimationEvent
						{
							functionName = "FrameEvent",
							time = isPercent?(animationClip.length*this.events[i].progress):this.events[i].progress,
							intParameter = i
						});
					}
				}
			}
		}


		private void FrameEvent(int id)
		{
			this.events[id].effect.SetActive(true);
			if (this.events[id].effectFun != string.Empty)
			{
				this.events[id].effect.SendMessage(this.events[id].effectFun);
			}
			if (this.events[id].effectLive != 0f)
			{
				base.StartCoroutine(this.DelayHide(this.events[id].effectLive, this.events[id].effect));
			}
		}

		private IEnumerator DelayHide(float time, GameObject obj)
		{
			yield return new WaitForSecondsRealtime(time);
			obj.SetActive(false);
			yield break;
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
	}
}
