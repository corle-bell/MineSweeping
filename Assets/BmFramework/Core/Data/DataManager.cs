using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager instance;

        public DataGroup Default;
        public Dictionary<string, DataGroup> cacheData = new Dictionary<string, DataGroup>();
        public void Create()
        {
            Default = CreateGroup("Default", false);
        }

        public DataGroup GetGroup(string _key)
        {
            DataGroup ret = null;
            cacheData.TryGetValue(_key, out ret);
            return ret;
        }

        /// <summary>
        /// 创建组
        /// </summary>
        /// <param name="_key">键值</param>
        /// <param name="isDump">是否可以Dump</param>
        /// <returns></returns>
        public DataGroup CreateGroup(string _key, bool isDump=true)
        {
            if(!cacheData.ContainsKey(_key))
            {
                var t = new DataGroup();
                t.name = _key;
                t.isDump = isDump;
                t.Init();
                cacheData.Add(_key, t);
                return t;
            }
            return null;
        }

        public void RemoveGroup(string _key)
        {
            cacheData.Remove(_key);
        }
    }
}

