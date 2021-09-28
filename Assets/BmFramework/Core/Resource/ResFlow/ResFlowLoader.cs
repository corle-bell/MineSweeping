using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public class ResFlowLoader
    {
        List<ResFlowNode> resFlowNodes = new List<ResFlowNode>();
        int id = 0;
        System.Action callback;

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
        public void Add(string _path, int count, int frameLoad, bool _isInPool=false)
        {
            var t = new ResFlowNode();
            t.count = count;
            t.path = _path;
            t.frameLoad = frameLoad;
            t.isPoolNode = _isInPool;
            resFlowNodes.Add(t);
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

            ResManager.instance.frameLoadNum = node.frameLoad;
            if (node.isPoolNode)
            {
                PoolManager.instance.LoadRes(node.path, node.count);
            }
            else
            {
                ResManager.instance.LoadRes(node.path, node.count);
            }
        }

        private void LoadOver()
        {
            callback?.Invoke();
            callback = null;
            NotifacitionManager.RemoveObserver(NotifyType.Msg_Resouce_Load, this.MsgEvent);
        }
        #endregion

        #region event msg
        void MsgEvent(NotifyEvent _data)
        {
            if (_data.Cmd == "All")
            {
                LoadOne(++id);
            }
        }
        #endregion
    }
}
