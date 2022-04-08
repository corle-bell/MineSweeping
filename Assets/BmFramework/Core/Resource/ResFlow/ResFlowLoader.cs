using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public interface IResFlowLoaderCallback
    {
        void OnObjectLoad(Object target, string path, int left, object data);
        void OnStepComplete(string path);

        void OnAllComplete();
    }
    public class ResFlowLoader
    {
        List<ResFlowNode> resFlowNodes = new List<ResFlowNode>();
        int id = 0;
        System.Action callback;
        public string currentPath;
        public int currentCount;
        public bool isPool;
        public IResFlowLoaderCallback flowLoaderCallback;
        public Transform currentParent;

        public ResFlowLoader()
        {
            
        }

        public ResFlowLoader(IResFlowLoaderCallback callback)
        {
            this.flowLoaderCallback = callback;
        }

        public void Begin(System.Action action)
        {
            callback = action;
            NotifacitionManager.AddObserver(NotifyType.Msg_Resouce_Load, this.MsgEvent);
            id = 0;
            LoadOne(id);
        }

        public void Add(ResFlowNode[] nodes)
        {
            resFlowNodes.AddRange(nodes);
        }

        /// <summary>
        /// 添加一个节点
        /// </summary>
        /// <param name="_path">路径</param>
        /// <param name="count">数量</param>
        /// <param name="frameLoad">单帧实例化数量</param>
        /// <param name="_isInPool">是否加载到缓存池</param>
        public ResFlowNode Add(string _path, int count, int frameLoad, bool _isInPool=false, Transform _parent=null, bool _isDestoryPool=true, bool _isInstantiate=true)
        {
            var t = new ResFlowNode();
            t.count = count;
            t.path = _path;
            t.frameLoad = frameLoad;
            t.isPoolNode = _isInPool;
            t.poolParent = _parent;
            t.isDestoryPool = _isDestoryPool;
            t.isInstantiate = _isInstantiate;
            resFlowNodes.Add(t);
            return t;
        }


        #region private
        private void LoadOne(int _id)
        {
            if(_id>=resFlowNodes.Count)
            {
                LoadOver();
                return;
            }
            ResFlowNode node = resFlowNodes[_id];
            currentPath = node.path;
            currentCount = node.count;
            currentParent = node.poolParent;
            isPool = node.isPoolNode;

            ResManager.instance.frameLoadNum = node.frameLoad;
            if (node.isPoolNode)
            {
                if(node.isDestoryPool)
                {
                    PoolManager.instance.DestoryPool.SetParent(node.poolParent);
                    PoolManager.instance.DestoryPool.LoadRes(node.path, node.count);
                }
                else
                {
                    PoolManager.instance.DontDestoryPool.SetParent(node.poolParent);
                    PoolManager.instance.DontDestoryPool.LoadRes(node.path, node.count);
                }
            }
            else
            {
                ResManager.instance.LoadRes(node.path, node.count, null, node.isInstantiate, node.data);
            }
        }

        private void LoadOver()
        {
            flowLoaderCallback?.OnAllComplete();
            callback?.Invoke();
            callback = null;
            NotifacitionManager.RemoveObserver(NotifyType.Msg_Resouce_Load, this.MsgEvent);
        }
        #endregion

        #region event msg
        void MsgEvent(NotifyEvent _data)
        {
            if (_data.Cmd == currentPath)
            {
                currentCount--;

                if (!isPool)
                {
                    GameObject t = _data.GameObj as GameObject;
                    t?.transform.SetParent(currentParent);
                }

                flowLoaderCallback?.OnObjectLoad(_data.GameObj, currentPath, currentCount, resFlowNodes[id].data);
                if (currentCount<=0)
                {
                    flowLoaderCallback?.OnStepComplete(currentPath);

                    LoadOne(++id);
                }
            }
        }
        #endregion
    }
}
