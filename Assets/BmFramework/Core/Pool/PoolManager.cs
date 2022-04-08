using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public sealed class PoolManager : MonoBehaviour
    {
        public static PoolManager instance = null;
        public PoolGroup DestoryPool;
        public PoolGroup DontDestoryPool;
        void Awake()
        {
            DestoryPool = new PoolGroup();
            DontDestoryPool = new PoolGroup();
        }

        internal void UsingLogic(float deltaTime)
        {
            DestoryPool.UsingLogic(deltaTime);
            DontDestoryPool.UsingLogic(deltaTime);
        }

        public void SetParent(Transform _p, bool isDestoryPool = true)
        {
            if (isDestoryPool)
            {
                DestoryPool.SetParent(_p);
            }
            else
            {
                DontDestoryPool.SetParent(_p);
            }
        }

        /// <summary>
        /// 加载资源到缓存池
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_number"></param>
        public void LoadRes(string _path, int _number, bool isDestoryPool = true)
        {
            if (isDestoryPool)
            {
                DestoryPool.LoadRes(_path, _number);
            }
            else
            {
                DontDestoryPool.LoadRes(_path, _number);
            }
        }
    


        /// <summary>
        /// 获得节点
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        public PoolNode Get(string _path, bool isDestoryPool = true)
        {
            if (isDestoryPool)
            {
                return DestoryPool.Get(_path);
            }
            else
            {
                return DontDestoryPool.Get(_path);
            }
        }

        /// <summary>
        /// 回收节点
        /// </summary>
        /// <param name="_node"></param>
        public void Recycle(PoolNode _node, bool isDestoryPool = true)
        {
            if (isDestoryPool)
            {
                DestoryPool.Recycle(_node);
            }
            else
            {
                DontDestoryPool.Recycle(_node);
            }
        }

   
        internal void JoinLogic(PoolNode node, bool isDestoryPool = true)
        {
            if (isDestoryPool)
            {
                DestoryPool.JoinLogic(node);
            }
            else
            {
                DontDestoryPool.JoinLogic(node);
            }
        }

        internal void ExitLogic(PoolNode node, bool isDestoryPool = true)
        {
            if (isDestoryPool)
            {
                DestoryPool.ExitLogic(node);
            }
            else
            {
                DontDestoryPool.ExitLogic(node);
            }
        }

        public void Clear(bool isDestory = false, bool isDestoryPool=true)
        {
            if(isDestoryPool)
            {
                DestoryPool.Clear(isDestory);
            }
            else
            {
                DontDestoryPool.Clear(isDestory);
            }
        }

        private void OnDestroy()
        {
            this.StopAllCoroutines();
        }
    }
}

