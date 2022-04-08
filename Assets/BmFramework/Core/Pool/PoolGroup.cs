using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public class PoolGroup : IResLoadCallback
    {
        Transform parent;

        Dictionary<string, List<PoolNode>> freeData = new Dictionary<string, List<PoolNode>>();
        Dictionary<string, List<PoolNode>> usingData = new Dictionary<string, List<PoolNode>>();

        List<PoolNode> logicData = new List<PoolNode>();

        public void SetParent(Transform _p)
        {
            parent = _p;
        }

        /// <summary>
        /// 加载资源到缓存池
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_number"></param>
        public void LoadRes(string _path, int _number)
        {
            if (parent != null)
            {
                ResManager.instance.LoadRes<GameObject>(_path, _number, this);
            }
            else
            {
                Debug.LogError("父容器为空！！！！！ 请设置");
            }
        }

        /// <summary>
        /// OnLoad 获得ResManager加载后的Object
        /// </summary>
        /// <param name="path"></param>
        /// <param name="_obj"></param>
        public void OnLoad(string path, Object _obj, object _data)
        {
            List<PoolNode> freelist;
            if (!freeData.TryGetValue(path, out freelist))
            {
                freelist = new List<PoolNode>();
                freeData.Add(path, freelist);

                var useringList = new List<PoolNode>();
                usingData.Add(path, useringList);
            }
            GameObject tmp = _obj as GameObject;
            if (tmp != null)
            {
                PoolNode node = tmp.GetComponent<PoolNode>();
                if (node == null) node = tmp.AddComponent<PoolNode>();

                node.manager = this;
                node.path = path;
                tmp.transform.parent = parent;
                freelist.Add(node);
                node.gameObject.SetActive(false);
            }

        }

        /// <summary>
        /// 获得节点
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        public PoolNode Get(string _path)
        {
            PoolNode ret = null;
            List<PoolNode> freelist;
            List<PoolNode> usinglist;
            if (freeData.TryGetValue(_path, out freelist) && usingData.TryGetValue(_path, out usinglist))
            {
                if (freelist.Count > 0)
                {
                    ret = freelist[0];
                    usinglist.Add(ret);
                    freelist.Remove(ret);
                    ret.Acitve();
                }
                else
                {
                    //Debug.Log("No Count "+ _path);
                }
            }
            else
            {
                //Debug.Log("No Key "+ _path);
            }
            return ret;
        }

        /// <summary>
        /// 回收节点
        /// </summary>
        /// <param name="_node"></param>
        public void Recycle(PoolNode _node, bool ChangeParent=false)
        {
            List<PoolNode> freelist;
            List<PoolNode> usinglist;
            if (freeData.TryGetValue(_node.path, out freelist) && usingData.TryGetValue(_node.path, out usinglist))
            {
                usinglist.Remove(_node);
                freelist.Add(_node);

                if(ChangeParent)
                {
                    _node.transform.parent = parent;
                }
            }
        }

        public void RecycleUsing(bool ChangeParent = false)
        {
            foreach (var item in usingData)
            {
                for (int i=0; i<item.Value.Count; i++)
                {
                    var node = item.Value[i];
                    node.Recycle(ChangeParent);
                }
            }
        }

        internal void UsingLogic(float deltaTime)
        {
            for (int i = 0; i < logicData.Count; i++)
            {
                logicData[i].Logic(deltaTime);
            }
        }

        internal void JoinLogic(PoolNode node)
        {
            logicData.Add(node);
        }

        internal void ExitLogic(PoolNode node)
        {
            logicData.Remove(node);
        }

        public void Clear(bool isDestory = false)
        {
            if (isDestory)
            {
                foreach (var item in freeData)
                {
                    foreach (var node in item.Value)
                    {
                        GameObject.Destroy(node.gameObject);
                    }
                    item.Value.Clear();
                }

                foreach (var item in usingData)
                {
                    foreach (var node in item.Value)
                    {
                        GameObject.Destroy(node.gameObject);
                    }
                    item.Value.Clear();
                }

                logicData.Clear();

                freeData.Clear();
                usingData.Clear();
            }
            else
            {
                foreach (var item in freeData)
                {
                    item.Value.Clear();
                }

                foreach (var item in usingData)
                {
                    item.Value.Clear();
                }

                logicData.Clear();
                freeData.Clear();
                usingData.Clear();
            }
            parent = null;
        }

    }
}
