using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public static class FrameworkTools
{
	
	public static string GetCurrentDataPath()
	{
		string baseDir = "";
		if(Application.platform == RuntimePlatform.Android)
		{
			baseDir = "jar:file://" + Application.dataPath + "!/assets/";
		}
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			baseDir = Application.dataPath + "/Raw/"; 
		}
		if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
		{
			baseDir = Application.dataPath + "/StreamingAssets/";
		}
		return baseDir;
	}

	public static string GetCurrentDataPathInWebRequset()
	{
		string baseDir = "";
		if(Application.platform == RuntimePlatform.Android)
		{
			baseDir = Application.streamingAssetsPath;
		}
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			baseDir = "file://"+Application.streamingAssetsPath + "/Raw/"; 
		}
		if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
		{
			baseDir = "file://"+Application.streamingAssetsPath + "/StreamingAssets/";
		}
		return baseDir;
	}

	public static Vector2 Vector3To2(Vector3 _in)
	{
		return new Vector2(_in.x, _in.z);
	}

	public static float DistanceXZ(Vector3 a, Vector3 b)
    {
		return Vector2.Distance(Vector3To2(a), Vector3To2(b));
    }

	
	public static int GetRandomId(int [] _prob, int _ranodm)
	{
		int t = 0;
		for(int i=0; i<_prob.Length; i++)
		{
			if(_ranodm<=_prob[i]+t)
			{
				return i;
			}
			else
			{
				t += _prob[i];
			}
		}
		return 0;
	}
	public static float Ranomd(float _max)
	{
		return UnityEngine.Random.Range(0f, _max);
	}

	public static float Ranomd(float _min, float _max)
	{
		return UnityEngine.Random.Range(_min, _max);
	}

	public static int Ranomd(int _max)
	{
		return UnityEngine.Random.Range(0, _max);
	}

	public static int Ranomd(int _min, int _max)
	{
		return UnityEngine.Random.Range(_min, _max);
	}

	public static Vector3 RanomdInCircle(float _radius, Vector3 _zero_dir, Vector3 _normal)
	{
		return Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360), _normal) * (_zero_dir.normalized*_radius);
	}


	public static int GetClassValueByNameInt(object obj, string _key)
	{
		return GetClassValueByName<int>(obj, _key);
	}

	public static float GetClassValueByNameFloat(object obj, string _key)
	{
		return GetClassValueByName<float>(obj, _key);
	}

	public static string GetClassValueByNameString(object obj, string _key)
	{
		return GetClassValueByName<string>(obj, _key);
	}

	public static T GetClassValueByName<T>(object obj, string _key)
	{
		return (T)((object)obj.GetType().GetField(_key).GetValue(obj));
	}


	public static T RandomValueWithArray<T>(List<T> arr)
	{
		if (arr == null)
		{
			return default(T);
		}
		if (arr.Count == 0)
		{
			return default(T);
		}
		if (arr.Count == 1)
		{
			return arr[0];
		}
		int id = Ranomd(arr.Count);
		return arr[id];
	}

	public static T RandomValueWithArray<T>(T [] arr)
	{
		if(arr==null)
		{
			return default(T);
		}
		if(arr.Length==0)
		{
			return default(T);
		}
		if(arr.Length==1)
		{
			return arr[0];
		}
		int id = Ranomd(arr.Length);
		return arr[id];
	}

	public static bool IndexOf(object[] _arr, object _v)
	{
		for (int i = 0; i < _arr.Length; i++)
		{
			if (_arr[i].GetHashCode() == _v.GetHashCode())
			{
				return true;
			}
		}
		return false;
	}

	public static bool IndexOf(float[] _arr, float _v)
	{
		for (int i = 0; i < _arr.Length; i++)
		{
			if (_arr[i] == _v)
			{
				return true;
			}
		}
		return false;
	}

	public static bool IndexOf(int[] _arr, int _v)
	{
		for (int i = 0; i < _arr.Length; i++)
		{
			if (_arr[i] == _v)
			{
				return true;
			}
		}
		return false;
	}

	public static void ArrayClean<T>(T [] _arr, T dd=default(T))
	{
		for(int i=0; i<_arr.Length; i++)
		{
			_arr[i] = dd;
		}
	}


	public static int GetMinIndex(int [] _arr, int _start=0)
	{
		int _min_v = 0;
		int _index = 0;
		GetMinIndex(_arr, out _index, out _min_v, _start);
		return _index;
	}

	public static void GetMinIndex(int [] _arr, out int _index, out int _min_v, int _start=0)
	{
		_min_v = 0;
		_index = 0;
		for(int i=_start; i<_arr.Length; i++)
		{
			if(_min_v<_arr[i] || _min_v==0)
			{
				_min_v = _arr[i];
				_index = i;
			}
		}
	}

	public static T DeepCopyByBinary<T>(T obj)
	{
		object obj2;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, obj);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			obj2 = binaryFormatter.Deserialize(memoryStream);
			memoryStream.Close();
		}
		return (T)((object)obj2);
	}


	public static T Mapper<T>(T s)
	{
		return Mapper<T, T>(s);
	}

	public static T MapperToModel<T>(T _out, T _src)
	{
		return MapperToModel<T, T>(_out, _src);
	}

	/// <summary>
	/// 反射实现两个类的对象之间相同属性的值的复制
	/// 适用于初始化新实体
	/// </summary>
	/// <typeparam name="D">返回的实体</typeparam>
	/// <typeparam name="S">数据源实体</typeparam>
	/// <param name="s">数据源实体</param>
	/// <returns>返回的新实体</returns>
	public static D Mapper<D, S>(S s)
	{
		D d = Activator.CreateInstance<D>(); //构造新实例
		try
		{
			var Types = s.GetType();//获得类型  
			var Typed = typeof(D);
			foreach (FieldInfo sp in Types.GetFields())//获得类型的属性字段  
			{
				foreach (FieldInfo dp in Typed.GetFields())
				{
					if (dp.Name == sp.Name)//判断属性名是否相同  
					{
						dp.SetValue(d, sp.GetValue(s));
					}
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
		return d;
	}

	/// <summary>
	/// 反射实现两个类的对象之间相同字段值的复制
	/// 适用于没有新建实体之间
	/// </summary>
	/// <typeparam name="D">返回的实体</typeparam>
	/// <typeparam name="S">数据源实体</typeparam>
	/// <param name="d">返回的实体</param>
	/// <param name="s">数据源实体</param>
	/// <returns></returns>
	public static D MapperToModel<D, S>(D d,S s)
	{
		try
		{
			var Types = s.GetType();//获得类型  
			var Typed = typeof(D);
			foreach (FieldInfo sp in Types.GetFields())//获得类型的属性字段  
			{
				foreach (FieldInfo dp in Typed.GetFields())
				{
					if (dp.Name == sp.Name)//判断属性名是否相同  
					{
						dp.SetValue(d, sp.GetValue(s));
					}
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
		return d;
	}
	public static Vector3 V3RotateAround(Vector3 source, Vector3 axis, float angle)
	{
		Quaternion rotation = Quaternion.AngleAxis(angle, axis);
		return rotation * source;
	}

	public static Vector3 stringToVector3(string _text)
	{
		Vector3 zero = Vector3.zero;
		MatchCollection matchCollection = Regex.Matches(_text, "(?<=\\().*?(?=\\))");
		string value = matchCollection[0].Value;
		string[] array = Regex.Split(value, ",");
		zero.x = float.Parse(array[0]);
		zero.y = float.Parse(array[1]);
		zero.z = float.Parse(array[2]);
		return zero;
	}

	public static float[] stringToFloatArray(string _text, string _flag = ",")
	{
		string[] arr = Split(_text, _flag);
		if (arr.Length == 0)
		{
			return null;
		}
		float[] ret = new float[arr.Length];
		for (int i = 0; i < ret.Length; i++)
		{
			if (arr[i] == "") continue;
			ret[i] = float.Parse(arr[i]);
		}
		return ret;
	}

	public static int [] stringToIntArray(string _text, string _flag=",")
	{
		string [] arr = Split(_text, _flag);
		if(arr.Length==0)
		{
			return null;
		}
		int [] ret = new int[arr.Length];
		for(int i=0; i<ret.Length; i++)
		{
			if(arr[i]=="")continue;
			ret[i] = int.Parse(arr[i]);
		}
		return ret;
	}

	public static void stringToIntArray(string _text, ref int [] _out_arr, string _flag=",")
	{
		string [] arr = Split(_text, _flag);
		for(int i=0; i<_out_arr.Length; i++)
		{
			if(arr[i]=="")continue;
			_out_arr[i] = int.Parse(arr[i]);
		}
	}

	public static void ClearChild(Transform parent)
	{
		if(parent==null)return;
		for(int i=0; i<parent.childCount; i++)
		{
			UnityEngine.Object.Destroy(parent.GetChild(i).gameObject);
		}
	}

	public static void ClearChildNow(Transform parent)
	{
		if (parent == null) return;
		for (int i = 0; i < parent.childCount; i++)
		{
			UnityEngine.Object.DestroyImmediate(parent.GetChild(i).gameObject);
		}
	}

	public static T [] listToArray<T>(IList _list)
	{
		T [] ret = new T [_list.Count];
		for(int i=0; i<ret.Length; i++)
		{
			ret[i] = (T)_list[i];
		}
		return ret;
	}

	
	public static double GetProcessUsedMemory()
	{
		return (double)Process.GetCurrentProcess().WorkingSet64 / 1024.0 / 1024.0;
	}

	/// <summary>
	/// 搜索物体下指定的物体的脚本 或者 指定物体下的脚本
	/// </summary>
	/// <typeparam name="T">类型</typeparam>T
	/// <param name="obj">被搜索的物体</param>
	/// <param name="_name">要搜索的物体名称</param>
	/// <param name="_isChild">是否从目标子集查找</param>
	/// <returns></returns>
	public static T FindChildComponentByName<T>(GameObject obj, string _name, bool _isChild=false)
	{
		return FindChildComponentByName<T>(obj.transform, _name, _isChild);
	}

	public static T FindChildComponentByName<T>(Transform obj, string _name, bool _isChild=false)
	{
		T ret= default(T);
		Transform child = GetChild(obj, _name);
		if(child!=null)
		{
			if(_isChild)ret = child.GetComponentInChildren<T>();
			else ret = child.GetComponent<T>();
		}
		return ret;
	}

	public static GameObject GetChild(GameObject src, string _name)
	{
		Transform transform = GetChild(src.transform, _name);		
		return transform == null?null:transform.gameObject;
	}

	public static Transform GetChild(Transform src, string _name)
	{
		Transform transform = src.Find(_name);
		if (transform == null)
		{
			for (int i = 0; i < src.childCount; i++)
			{
				transform = GetChild(src.GetChild(i), _name);
				if (!(transform == null))
				{
					return transform;
				}
			}
		}
		return transform;
	}

	public static string [] Split(string _src, string flag)
	{
		return _src.Split(flag.ToCharArray());
	}

	public static double FixFloat(float _value, int len)
	{
		return Math.Round(_value, len);
	}

	public static string IntArrayToString(int[] SafetyMeasure) 
	{
		StringBuilder sb = new StringBuilder();
		for(int i=0;i<SafetyMeasure.Length-1;i++){
		   sb.Append(SafetyMeasure[i]+",");
		}
		sb.Append(SafetyMeasure[SafetyMeasure.Length-1]);
		return sb.ToString();
	}

	public static string StringArrayToString(string[] SafetyMeasure)
	{
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < SafetyMeasure.Length - 1; i++)
		{
			sb.Append(SafetyMeasure[i] + ",");
		}
		sb.Append(SafetyMeasure[SafetyMeasure.Length - 1]);
		return sb.ToString();
	}

	public static string FloatArrayToString(float[] SafetyMeasure) 
	{
		StringBuilder sb = new StringBuilder();
		for(int i=0;i<SafetyMeasure.Length-1;i++){
		   sb.Append(SafetyMeasure[i]+",");
		}
		sb.Append(SafetyMeasure[SafetyMeasure.Length-1]);
		return sb.ToString();
	}


	public static bool IsInArray<T>(List<T> _t, int _id)
    {
		return (_id >= 0 && _id < _t.Count);
    }

	public static bool IsInArray<T>(T[] _t, int _id)
	{
		return (_id >= 0 && _id < _t.Length);
	}

	public static void Swap<T>(ref T _t1, ref T _t2)
    {
		T t = _t1;
		_t1 = _t2;
		_t2 = t;
    }

	public static float CalculateLengthOfText(string message, UnityEngine.UI.Text tex)
    {

		Font font = tex.font;
		int fontsize = tex.fontSize;
		font.RequestCharactersInTexture(message, fontsize, FontStyle.Normal);
		CharacterInfo characterInfo;
		float width = 0f;
		for (int i = 0; i < message.Length; i++)
		{

			font.GetCharacterInfo(message[i], out characterInfo, fontsize);
			width += characterInfo.advance;
		}
		return width;
	}

	public static int CalculateHeightOfText(string message, UnityEngine.UI.Text tex)
	{
		return tex.fontSize;
	}


	public static string GetStringInBrackets(string _in)
    {
		return System.Text.RegularExpressions.Regex.Replace(_in, @"(.*\()(.*)(\).*)", "$2");
	}

	//将两个没有高位的int去低位合成一个int  left放高位 right放低位
	public static int MergeInt(int left, int right)
	{
		return ((left & 0XFF)<<8|0X0000) | (right & 0XFF);
	}

	//将一个int的高低位拆开
	public static void DetachInt(int _src, out int _left, out int _right)
	{
		_left = (_src & 0XFF00)>>8;
		_right = (_src & 0XFF);
	}

	public static long MergeLong(int left, int right)
	{
		return ((long)left<<32) | ((long)right);
	}

	//将一个int的高低位拆开
	public static void DetachLong(long _src, out int _left, out int _right)
	{
		_left = (int)(_src>>32 & 0xFFFFFFFF);
		_right = (int)(_src & 0xFFFFFFFF);
	}


	public static string GetValue(string str, string s, string e)
	{
		Regex rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline);
		return rg.Match(str).Value;
	}

	public static Vector3 ConvertMouseToWorld(Transform _trans, Camera _camera)
	{
		return ConvertScreenToWorld(Input.mousePosition, _trans, _camera);
	}

	public static Vector3 ConvertScreenToWorld(Vector3 mouse, Transform _trans, Camera _camera)
	{
		var screenPosition = _camera.WorldToScreenPoint(_trans.position);
		Vector3 pos = mouse;
		pos.z = screenPosition.z;
		pos = _camera.ScreenToWorldPoint(pos);
		return pos;
	}
}
