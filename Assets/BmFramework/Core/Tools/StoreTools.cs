using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTools{
	public static void SetInt(string _key, int _v)
	{
		PlayerPrefs.SetInt(_key, _v);
	}

	public static int GetInt(string _key, int _v)
	{
		return PlayerPrefs.GetInt(_key, _v);
	}

	public static void SetLong(string _key, long _v)
	{
		PlayerPrefs.SetString(_key, _v.ToString());
	}

	public static long GetLong(string _key, long _v)
	{		
		return long.Parse(PlayerPrefs.GetString(_key, "0"));
	}

	public static void SetString(string _key, string _v)
	{
		PlayerPrefs.SetString(_key, _v);
	}

	public static void SetBool(string _key, bool _v)
	{
		PlayerPrefs.SetInt(_key, _v?1:0);
	}

	public static string GetString(string _key, string _v)
	{
		return PlayerPrefs.GetString(_key, _v);
	}

	public static int [] GetIntArray(string _key)
	{
		string r = GetString(_key, "");
		return r==""?null:FrameworkTools.stringToIntArray(r);
	}

	public static void SetIntArray(string _key, int [] _value)
	{
		SetString(_key, FrameworkTools.IntArrayToString(_value));
	}

	public static void SetFloat(string _key, float _v)
	{
		PlayerPrefs.SetFloat(_key, _v);
	}

	public static float GetFloat(string _key, float _v)
	{
		return PlayerPrefs.GetFloat(_key, _v);
	}

	public static bool GetBool(string _key, bool _v)
	{
		return PlayerPrefs.GetInt(_key, _v?1:0)==1?true:false;
	}

	public static void Delete(string _key)
    {
		PlayerPrefs.DeleteKey(_key);
    }
	
	public static void Clean()
	{
		PlayerPrefs.DeleteAll();
	}
}
