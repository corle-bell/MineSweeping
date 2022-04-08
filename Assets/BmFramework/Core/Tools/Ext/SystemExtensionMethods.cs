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
}
