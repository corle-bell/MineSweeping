using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

namespace BmFramework.Core
{

	[System.Serializable]
	public class TaskNode {
		public System.Action<TaskNode> callback;
		public string _data;
		public float timestamp;
		public float time;
	}

	public class AsyncOperationNode
	{
		public System.Action<AsyncOperation> callback;
		public AsyncOperation op;
	}
	public class TaskLogic
	{

		public static TaskLogic Instance;

		public int maxTaskNum;

		public float checkDelay;
		private float tick = 0;
		private List<TaskNode> runingTask = new List<TaskNode>();
		private List<IAction> actionList = new List<IAction>();
		private List<AsyncOperationNode> asyncOperationNodes = new List<AsyncOperationNode>();
		private int status = 0;

		public void AddAction(IAction _action)
        {
			actionList.Add(_action);
        }

		public void RemoveAction(IAction _action)
		{
			actionList.Remove(_action);
		}

		public int AddTask(string _data, float _time, System.Action<TaskNode> callback)
		{
			TaskNode task = new TaskNode();
			task._data = _data;
			task.timestamp = Time.realtimeSinceStartup;
			task.time = _time;
			task.callback = callback;

			runingTask.Add(task);

			return (runingTask.Count-1);
		}

		public int AddTask(float _time, System.Action<TaskNode> callback)
		{
			return AddTask("", _time, callback);
		}

		public void RemoveTask(int id, bool isComplete)
		{
			TaskNode _node = runingTask[id];
			if(isComplete)_node.callback.Invoke(_node);
			_node.callback = null;
			runingTask.Remove(_node);
		}

		public void Pause()
		{
			status = 0;
		}

		public void Resume()
		{
			status = 1;
		}

		//---------------- 
		private void DoCheck()
		{
			List<TaskNode> completeList = runingTask.FindAll(_node => (_node.time<=(Time.realtimeSinceStartup-_node.timestamp)));
			foreach (var _node in completeList)
			{
				_node.callback.Invoke(_node);
				_node.callback = null;
				runingTask.Remove(_node);
			}
		}


		internal void Update() 
		{
			for(int i=0; i<actionList.Count; i++)
            {
				actionList[i].Update();
            }

			UpdateAsyncOperation();
			ExecUpdate();
		}


		//---------------

		public void AddAsyncOperation(AsyncOperation op, System.Action<AsyncOperation> callback)
        {
			AsyncOperationNode task = new AsyncOperationNode();
			task.op = op;
			task.callback = callback;
			asyncOperationNodes.Add(task);
		}
		private void UpdateAsyncOperation()
        {
			for (int i = 0; i < asyncOperationNodes.Count; i++)
			{
				if(asyncOperationNodes[i].op.isDone)
                {
                    try
                    {
						asyncOperationNodes[i].callback?.Invoke(asyncOperationNodes[i].op);
						asyncOperationNodes.Remove(asyncOperationNodes[i]);
					}
					catch(Exception e)
                    {

                    }
				}
			}
		}


		//-------------------
		private void Runing()
		{
			tick -= Time.unscaledDeltaTime;
			if(tick<=0)
			{
				DoCheck();
				tick = checkDelay;
			}
		}

		private void Pauseing()
		{

		}

		private void ExecUpdate()
		{
			switch(status)
			{
				case 1:
					Runing();
				break;
				default:
					Pauseing();
				break;
			}
		}
		
	}
}
