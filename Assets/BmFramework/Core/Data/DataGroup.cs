using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BmFramework.Core
{
    public class DataGroup
    {
        public Dictionary<string, int> intData = null;
        public Dictionary<string, bool> boolData = null;
        public Dictionary<string, float> floatData = null;
        public Dictionary<string, string> stringData = null;
        public Dictionary<string, object> objData = new Dictionary<string, object>();

        public string name;
        public string path;
        public bool isDump;
        #region int data

        public void Init()
        {
            if(!isDump)
            {
                CreateData();
                return;
            }

            path = string.Format("{0}/{1}/", Application.persistentDataPath, name);
            if (!FileHandle.instance.IsExistDirectory(path))
            {
                FileHandle.instance.CreateDirectory(path);
                CreateData();
            }
            else
            {
                Read();
            }
        }
        public void SetInt(string key, int _data)
        {
            if (!intData.ContainsKey(key))
            {
                intData.Add(key, _data);
            }
            else
            {
                intData[key] = _data;
            }
        }

        public int GetInt(string key, int _default)
        {
            if (intData.ContainsKey(key))
            {
                return intData[key];
            }
            return _default;
        }
        #endregion

        #region bool data
        public void SetBool(string key, bool _data)
        {
            if (!boolData.ContainsKey(key))
            {
                boolData.Add(key, _data);
            }
            else
            {
                boolData[key] = _data;
            }
        }

        public bool GetBool(string key, bool _default)
        {
            if (boolData.ContainsKey(key))
            {
                return boolData[key];
            }
            return _default;
        }

        #endregion

        #region float data
        public void SetFloat(string key, float _data)
        {
            if (!floatData.ContainsKey(key))
            {
                floatData.Add(key, _data);
            }
            else
            {
                floatData[key] = _data;
            }
        }

        public float GetFloat(string key, float _default)
        {
            if (floatData.ContainsKey(key))
            {
                return floatData[key];
            }
            return _default;
        }

        #endregion

        #region string data
        public void SetString(string key, string _data)
        {
            if (!stringData.ContainsKey(key))
            {
                stringData.Add(key, _data);
            }
            else
            {
                stringData[key] = _data;
            }
        }

        public string GetString(string key, string _default)
        {
            if (stringData.ContainsKey(key))
            {
                return stringData[key];
            }
            return _default;
        }
        #endregion

        #region obj data
        public void SetObject(string key, object _data)
        {
            if (!objData.ContainsKey(key))
            {
                objData.Add(key, _data);
            }
            else
            {
                objData[key] = _data;
            }
        }

        public object GetObject(string key, object _default)
        {
            if (objData.ContainsKey(key))
            {
                return objData[key];
            }
            return _default;
        }
        #endregion

        public void Remove(string _key)
        {
            intData.Remove(_key);
            boolData.Remove(_key);
            stringData.Remove(_key);
            objData.Remove(_key);
        }

        public void Clear()
        {
            intData.Clear();
            boolData.Clear();
            stringData.Clear();
            objData.Clear();
        }

        public void Dump()
        {
            if (!isDump) return;
            string _intData = LitJson.JsonMapper.ToJson(intData);
            string _boolData = LitJson.JsonMapper.ToJson(boolData);
            string _stringData = LitJson.JsonMapper.ToJson(stringData);
            string _floatData = LitJson.JsonMapper.ToJson(floatData);

            FileHandle.instance.WriteText(path + "/intData", _intData);
            FileHandle.instance.WriteText(path + "/boolData", _boolData);
            FileHandle.instance.WriteText(path + "/floatData", _floatData);
            FileHandle.instance.WriteText(path + "/stringData", _stringData);
        }

        public void Read()
        {
            if (!isDump) return;

            string _intData = FileHandle.instance.ReadAllString(path + "/intData");
            string _boolData = FileHandle.instance.ReadAllString(path + "/boolData");
            string _stringData = FileHandle.instance.ReadAllString(path + "/stringData");
            string _floatData = FileHandle.instance.ReadAllString(path + "/floatData");

            try
            {
                intData = LitJson.JsonMapper.ToObject<Dictionary<string, int>>(_intData);
            }
            catch (System.Exception e)
            {
                intData = new Dictionary<string, int>();
            }

            try
            {
                boolData = LitJson.JsonMapper.ToObject<Dictionary<string, bool>>(_boolData);
            }
            catch (System.Exception e)
            {
                boolData = new Dictionary<string, bool>();
            }

            try
            {
                floatData = LitJson.JsonMapper.ToObject<Dictionary<string, float>>(_floatData);
            }
            catch (System.Exception e)
            {
                floatData = new Dictionary<string, float>();
            }

            try
            {
                stringData = LitJson.JsonMapper.ToObject<Dictionary<string, string>>(_stringData);
            }
            catch (System.Exception e)
            {
                stringData = new Dictionary<string, string>();
            }


            if(intData==null) intData = new Dictionary<string, int>();
            if (boolData == null) boolData = new Dictionary<string, bool>();
            if (stringData == null) stringData = new Dictionary<string, string>();
            if (floatData == null) floatData = new Dictionary<string, float>();
        }

        private void CreateData()
        {
            intData = new Dictionary<string, int>();
            boolData = new Dictionary<string, bool>();
            stringData = new Dictionary<string, string>();
            floatData = new Dictionary<string, float>();
        }
    }
}
