using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public class SequenceAction : IAction
    {
        private List<Action> _list = new List<Action>();
        private List<float> _listTime = new List<float>();

        private int index=-1;
        private float timestamp;
        private float currentDelay;
        public SequenceAction()
        {
            this.Init();
        }

        public void Destory()
        {
            _list.Clear();
            _listTime.Clear();
            TaskLogic.Instance.RemoveAction(this);
        }

        public void Init()
        {
            TaskLogic.Instance.AddAction(this);
        }

        public SequenceAction Do(Action _action)
        {
            return Delay(0, _action);
        }

        public SequenceAction Delay(float _delay, Action _action)
        {
            _list.Add(_action);
            _listTime.Add(_delay);
            return this;
        }

        public void Execute()
        {
            Next();
        }

        private void Next()
        {

            index++;
            if(index>=_list.Count)
            {
                Destory();
                return;
            }
            timestamp = Time.realtimeSinceStartup;
            currentDelay = _listTime[index];
        }

        private void ExecOne()
        {
            if(Time.realtimeSinceStartup - timestamp>=currentDelay && index >= 0)
            {
                _list[index].Invoke();
                Next();
            }
        }

        public void Update()
        {
            ExecOne();
        }

        public static SequenceAction Create()
        {
            return new SequenceAction();
        }
    }
}

