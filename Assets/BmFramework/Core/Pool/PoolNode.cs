using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public class PoolNode:MonoBehaviour
    {
        [EnumName("路径")]
        public string path;

        [EnumName("生存时间")]
        public float live=3.0f;

        public PoolGroup manager;

        protected float leftTime;
        protected int p_status = 0;
        public void Acitve()
        {
            gameObject.SetActive(true);
            leftTime = live;
            BeginLogic();
            OnActive();
            if (leftTime > 0) p_status = 1;
        }

        public virtual void Logic(float _deltaTime)
        {
            switch (p_status)
            {
                case 1:
                    leftTime -= _deltaTime;
                    ChildLogic(_deltaTime);
                    if (leftTime<=0)
                    {
                        p_status = 0;
                        Recycle();
                    }
                    break;
            }
        }

        protected virtual void ChildLogic(float _deltaTime)
        {

        }

        void BeginLogic()
        {
            manager.JoinLogic(this);
        }

        protected virtual void OnActive()
        {

        }

        public virtual void Recycle(bool _isChangeParent=false)
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
            manager.ExitLogic(this);
            manager.Recycle(this, _isChangeParent);
        }
    }
}

