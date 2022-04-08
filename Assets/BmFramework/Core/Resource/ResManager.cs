using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    internal class ResLoadCell
    {
        public string path;
        public int number;
        public Object preload;
        public IResLoadCallback callback;
        public object data;
        public bool isInstantiate;
    }

    public interface IResLoadCallback
    {
        void OnLoad(string path, Object _obj, object _data);
    }

    public sealed class ResManager : MonoBehaviour
    {
        public static ResManager instance;
        public const int Frame_Load_Count = 15;
        public int frameLoadNum = 15;

        List<ResLoadCell> resLoadCells = new List<ResLoadCell>();

        int number;
        int status;

        internal void Init()
        {

        }

        /// <summary>
        /// 逻辑更新  分帧控制实例化数量
        /// </summary>
        internal void Logic()
        {
            switch (status)
            {
                case 1:
                    {
                        int count = frameLoadNum;
                        for (int i = 0; i < resLoadCells.Count; i++)
                        {
                            LoadOne(resLoadCells[i], ref count);
                            if (count <= 0) break;
                        }

                        if(resLoadCells.Count<=0)
                        {
                            status = 0;
                            NotifacitionManager.Post(NotifyType.Msg_Resouce_Load, "All");
                        }
                    }
                    break;
            }

        }

        /// <summary>
        /// 加载一个Cell的资源
        /// </summary>
        /// <param name="_res"></param>
        /// <param name="_left_count"></param>
        void LoadOne(ResLoadCell _res, ref int _left_count)
        {
            try
            {
                while (_left_count > 0 && _res.number > 0 && status==1)
                {
                    Object tmp = _res.preload==null?null:(_res.isInstantiate ? Instantiate(_res.preload) : _res.preload);

                    _res.number--;
                    _left_count--;

                    _res.callback?.OnLoad(_res.path, tmp, _res.data);
                    NotifacitionManager.Post(NotifyType.Msg_Resouce_Load, _res.path, tmp);
                    
                }

                if (_res.number <= 0)
                {
                    number--;
                    _res.callback = null;
                    _res.data = null;
                    _res.preload = null;
                    resLoadCells.Remove(_res);
                }
            }
            catch (System.Exception e)
            {
                BmDebug.Error("Res Load Error: "+_res.path+" number:"+ _res.number+" left:"+ _left_count+"  "+e.ToString());
            }
        }

        /// <summary>
        /// 加载资源  
        /// 每当有新的资源进来那么  
        /// 暂停当前的分帧实例化操作 
        /// 用携程进行资源的异步加载
        /// 结束后继续分帧实例化的流程
        /// </summary>
        /// <param name="_path">路径</param>
        /// <param name="_number">数量</param>
        /// <param name="_callback">回调</param>
        /// <param name="_data">传入参数</param>
        public void LoadRes<T>(string _path, int _number, IResLoadCallback _callback=null, bool _isInstantiate = true, object _data=null) where T:UnityEngine.Object
        {

            status = 0;

            ResLoadCell resLoadCell = new ResLoadCell();
            resLoadCell.path = _path;
            resLoadCell.number = _number;
            resLoadCell.callback = _callback;
            resLoadCell.data = _data;
            resLoadCell.isInstantiate = _isInstantiate;

            resLoadCells.Add(resLoadCell);

            StartCoroutine(LoadResouce<T>(resLoadCell));


        }

        public void LoadRes(string _path, int _number, IResLoadCallback _callback = null, bool _isInstantiate = true, object _data = null)
        {

            status = 0;

            ResLoadCell resLoadCell = new ResLoadCell();
            resLoadCell.path = _path;
            resLoadCell.number = _number;
            resLoadCell.callback = _callback;
            resLoadCell.data = _data;
            resLoadCell.isInstantiate = _isInstantiate;

            resLoadCells.Add(resLoadCell);

            StartCoroutine(LoadResouce(resLoadCell));

        }

        /// <summary>
        /// 预加载资源,但是这里不进行实例化
        /// </summary>
        /// <param name="_res"></param>
        /// <returns></returns>
        IEnumerator LoadResouce<T>(ResLoadCell _res) where T:UnityEngine.Object
        {
            ResourceRequest request = Resources.LoadAsync<T>(_res.path);
            yield return request;
            _res.preload = request.asset;
            PreloadOver();
        }

        /// <summary>
        /// 预加载资源,但是这里不进行实例化
        /// </summary>
        /// <param name="_res"></param>
        /// <returns></returns>
        IEnumerator LoadResouce(ResLoadCell _res)
        {
            ResourceRequest request = Resources.LoadAsync(_res.path);
            yield return request;
            _res.preload = request.asset;
            PreloadOver();
        }

        /// <summary>
        /// 预加载结束
        /// </summary>
        void PreloadOver()
        {

            number++;
            if(number>=resLoadCells.Count)
            {
                status = 1;
            }
        }

        private void OnDestroy()
        {
            this.StopAllCoroutines();
        }
    }

}
