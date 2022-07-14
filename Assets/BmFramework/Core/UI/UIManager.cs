using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BmFramework.Core
{
    public class UILoadParams
    {
        public object data;
        public System.Action<UIRoot> callback;
    }

    public sealed class UIManager : MonoBehaviour, IResLoadCallback
    {
        public static UIManager instance;

        internal Canvas canvas;

        [HideInInspector]
        public List<UIRoot> uiList = new List<UIRoot>();
        [HideInInspector]
        public List<string> NameList = new List<string>();

        public float scaleFactor = 1.0f;
        public float matchWidthOrHeight = 1.0f;
        internal void Init()
        {
            canvas = GetComponentInChildren<Canvas>();
            CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();

            scaler.referenceResolution = FrameworkMain.instance.screenSize;
   

            float kLogBase = 2;
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            float logWidth = Mathf.Log(screenSize.x / scaler.referenceResolution.x, kLogBase);
            float logHeight = Mathf.Log(screenSize.y / scaler.referenceResolution.y,kLogBase);
            float logWeightedAverage = Mathf.Lerp(logWidth,logHeight, scaler.matchWidthOrHeight);
            scaleFactor = Mathf.Pow(kLogBase, logWeightedAverage);


            float w_p = scaler.referenceResolution.x;
            float h_p = scaler.referenceResolution.y;

            float _my_p = h_p / w_p;

            float _t = (float)Screen.height / (float)Screen.width;

            if (_t < _my_p)
            {
                matchWidthOrHeight = scaler.matchWidthOrHeight = 1.0f;
            }
            else
            {
                matchWidthOrHeight = scaler.matchWidthOrHeight = 0;
            }
        }

        internal void Logic()
        {
            for(int i=0; i<uiList.Count; i++)
            {
                uiList[i].OnLogic();
            }
        }

        public void OpenUI<T>(object _data=null, bool _isSingle=true, System.Action<UIRoot> _callback=null)
        {
            string name = typeof(T).Name;
            OpenUI(name, _data, _isSingle, _callback);
        }

        public void OpenUI(string _name, object _data = null, bool _isSingle = true, System.Action<UIRoot> _callback = null)
        {
            string name = _name;
            if(_isSingle?(!isExist(name)):true)
            {
                if(_isSingle)NameList.Add(name);

                UILoadParams _params = new UILoadParams();
                _params.data = _data;
                _params.callback = _callback;
                ResManager.instance.LoadRes<GameObject>(string.Format("UI/{0}", name), 1, this, true, _params);
            }
        }

        public void OnLoad(string path, Object _obj, object _data)
        {
            UILoadParams _p = _data as UILoadParams;
            GameObject tmp = _obj as GameObject;
            UIRoot root = tmp.GetComponent<UIRoot>();
            root.transform.SetParent(canvas.transform, false);
            root.OnOpen(_p.data);            
            uiList.Add(root);
            _p.callback?.Invoke(root);
        }

        public void CloseUI(UIRoot _root)
        {
            _root.OnClose();
            uiList.Remove(_root);

            if(_root.isSingle)
            {
                string name = _root.GetType().Name;
                removeName(name);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < uiList.Count; i++)
            {
                var t = uiList[i];
                uiList.RemoveAt(i);
                t.transform.parent = null;
                Destroy(t.gameObject);
            }
            NameList.Clear();
        }

        private bool isExist(string _name)
        {
            return NameList.IndexOf(_name)>=0;
        }

        private void removeName(string _name)
        {
            NameList.Remove(_name);
        }
    }
}

