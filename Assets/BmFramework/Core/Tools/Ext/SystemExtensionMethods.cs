using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemExtensionMethods
{
    public static void Swap<T>(this T[] _script, int _a, int _b)
    {
		if (_a == _b) return;
		var a = _script[_a];
        _script[_a] = _script[_b];
        _script[_b] = a;
    }

	public static void Swap<T>(this List<T> _script, int _a, int _b)
	{
		if (_a == _b) return;
		var a = _script[_a];
		_script[_a] = _script[_b];
		_script[_b] = a;
	}

	public static int Sum(this int[] _script)
	{
		int ret = 0;
		foreach(var i in _script)
        {
			ret += i;
        }
		return ret;
	}

	public static float Sum(this float[] _script)
	{
		float ret = 0;
		foreach (var i in _script)
		{
			ret += i;
		}
		return ret;
	}

	public static T RandomByLen<T>(this T[] arr, int _len)
    {
		if (arr == null)
		{
			return default(T);
		}
		if (arr.Length == 0)
		{
			return default(T);
		}
		if (arr.Length == 1)
		{
			return arr[0];
		}
		int id = UnityEngine.Random.Range(0, _len);
		return arr[id];
	}

	public static T Random<T>(this T[] arr)
	{
		if (arr == null)
		{
			return default(T);
		}
		if (arr.Length == 0)
		{
			return default(T);
		}
		if (arr.Length == 1)
		{
			return arr[0];
		}
		int id = UnityEngine.Random.Range(0, arr.Length);
		return arr[id];
	}

	public static void Clean<T>(this T[] _arr, T dd = default(T))
	{
		for (int i = 0; i < _arr.Length; i++)
		{
			_arr[i] = dd;
		}
	}


	public static void Reverse<T>(this T[] _arr)
    {
		System.Array.Reverse(_arr);
	}

	public static T Last<T>(this T[] _arr)
	{
		return _arr[_arr.Length - 1];
	}

	public static T Last<T>(this List<T> _arr)
	{
		return _arr[_arr.Count - 1];
	}

	public static void Shuffle<T>(this List<T> _arr)
	{
		int len = _arr.Count;
		for (int i = 0; i < _arr.Count - 1; i++)
		{
			int id = FrameworkTools.Ranomd(100) % len;
			len--;
			_arr.Swap(id, len);
		}
	}

	public static void Shuffle<T>(this T[] _arr)
	{
		int len = _arr.Length;
		for (int i = 0; i < _arr.Length - 1; i++)
		{
			int id = FrameworkTools.Ranomd(100) % len;
			len--;
			_arr.Swap(id, len);
		}
	}

	public static void FillByIndex(this int[] arr)
	{
		for(int i=0; i<arr.Length; i++)
        {
			arr[i] = i;
        }
	}


	/// <summary>
	/// 遍历数组
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="_arr"></param>
	/// <param name="_callback"></param>
	public static void ForEach<T>(this T[] _arr, System.Action<T, int> _callback, int _start =0)
	{
		for (int i = _start; i < _arr.Length; i++)
		{
			_callback(_arr[i], i);
		}
	}

	/// <summary>
	/// 遍历两个数组 遍历长度以短的为止
	/// </summary>
	/// <typeparam name="T">左类型</typeparam>
	/// <typeparam name="T1">右类型</typeparam>
	/// <param name="_arr"></param>
	/// <param name="_arr1"></param>
	/// <param name="_callback">Invoke函数</param>
	public static void ForEach<T, T1>(this T[] _arr, T1[] _arr1, System.Action<T, T1> _callback)
	{
		int len = Mathf.Min(_arr.Length, _arr1.Length);
		for (int i = 0; i < len; i++)
		{
			_callback(_arr[i], _arr1[i]);
		}
	}

	/// <summary>
	/// 遍历两个数组 遍历长度以短的为止
	/// </summary>
	/// <typeparam name="T">左类型</typeparam>
	/// <typeparam name="T1">右类型</typeparam>
	/// <param name="_arr"></param>
	/// <param name="_arr1"></param>
	/// <param name="_callback">Invoke函数</param>
	public static void ForEach<T, T1>(this T[] _arr,  T1[] _arr1, System.Action<T, T1, int> _callback)
	{
		int len = Mathf.Min(_arr.Length, _arr1.Length);
		for (int i = 0; i < len; i++)
		{
			_callback(_arr[i], _arr1[i], i);
		}
	}
}
