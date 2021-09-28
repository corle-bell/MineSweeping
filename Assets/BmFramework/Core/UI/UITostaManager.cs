using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BmFramework.Core
{
    public class UITostaManager : MonoBehaviour, IResLoadCallback
    {
        public static UITostaManager instance;
        Canvas canvas;
        internal void Init()
        {
            canvas = GetComponentInChildren<Canvas>();
            CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();

            scaler.referenceResolution = FrameworkMain.instance.screenSize;

            float w_p = scaler.referenceResolution.x;
            float h_p = scaler.referenceResolution.y;

            float _my_p = h_p / w_p;

            float _t = (float)Screen.height / (float)Screen.width;

            if (_t < _my_p)
            {
                scaler.matchWidthOrHeight = 1.0f;
            }
            else
            {
                scaler.matchWidthOrHeight = 0;
            }
        }
        
        public void ShowToast(string _text)
        {
            ResManager.instance.LoadRes("FrameworkRes/Toast/Toast", 1, this, true, _text);
        }

        public void OnLoad(string path, Object _obj, object _data)
        {
            GameObject tmp = _obj as GameObject;
            tmp.transform.SetParent(canvas.transform, false);
            UIToast toast = tmp.GetComponent<UIToast>();
            toast.Begin(_data as string);           
        }
    }
}
