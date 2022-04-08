/****************************************************
 * FileName:		StringExtents
 * CreateTime:		2021-06-22 16:21:23
 * Version:			1.0
 * UnityVersion:	2020.3.5f1c1
 * Description:		Nothing
 * 
*****************************************************/


using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

public static class StringExtents
{

	
	static string[] string_N_Map = new string[5] { "{0:N0}", "{0:N1}", "{0:N2}", "{0:N3}", "{0:N5}" };
	public static string ToN(this int val, int _n)
	{
		return string.Format(string_N_Map[_n], val);
	}

	public static string ToN(this float val, int _n)
	{
		return string.Format(string_N_Map[_n], val);
	}


	static string[] string_D_Map = new string[5] { "D0", "D1", "D2", "D3", "D4" };
	public static string ToD(this float val, int _n)
	{
		return val.ToString(string_D_Map[_n]);
	}
	public static string ToD(this int val, int _n)
	{
		return val.ToString(string_D_Map[_n]);
	}


	static string[] string_F_Map = new string[5] { "F0", "F1", "F2", "F3", "F4" };
	public static string ToF(this int val, int _n)
	{
		return val.ToString(string_F_Map[_n]);
	}
	public static string ToF(this float val, int _n)
	{
		return val.ToString(string_F_Map[_n]);
	}


	public static string ToDicStr(this Dictionary<string, string> data)
	{
		StringBuilder bui = new StringBuilder();
		foreach (var item in data)
			bui.Append($"( {item.Key} : {item.Value} )\r\n");
		return bui.ToString();
	}
}
