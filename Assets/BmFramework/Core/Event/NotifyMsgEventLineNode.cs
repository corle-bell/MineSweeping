using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BmFramework.Core
{
	[System.Serializable]
	public class NotifyMsgEventLineNode
	{
		[EnumName("名称")]
		public string name = "";

		[Range(0, 1)]
		public float progress = 0;

		[HideInInspector]
		public bool isDo = false;

		

		[EnumName("指令")]
		public NotifyType cmd;

		[EnumName("附加物体")]
		public GameObject sender;

		[EnumName("附加信息")]
		public string SubCmd;

		public UnityEvent mEvent;
		
	}
}
