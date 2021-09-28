using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    internal class ManagerHelper
    {
        public static T CreateManager<T>(Transform _p) where T:MonoBehaviour
        {
            System.Type t = typeof(T);
            GameObject obj = new GameObject(t.Name);
            obj.transform.parent = _p;
            return obj.AddComponent<T>();
        }

        public static T AcitveManager<T>(Transform _p) where T:MonoBehaviour
        {
            T t = _p.GetComponentInChildren<T>();
            if(t==null)
            {
                t = CreateManager<T>(_p);
            }
            return t;
        }
    }
}

