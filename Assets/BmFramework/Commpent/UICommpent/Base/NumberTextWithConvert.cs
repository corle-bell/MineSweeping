using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberTextWithConvert : NumberText
{
	static string[] NumberConvert_EN = new string [] { "", "K", "M", "B", "T", "Qa", "Qi", "Sx" };
	static string[] NumberConvert_CN = new string [] { "", "万", "亿", "兆", "京", "垓" };

	protected override string FormatNumber(float _number)
	{
		return NumberFormatByFlag(_number);
	}


	static string NumberFormatByFlag(float _number)
	{
		/*if (Define.TextureLocationFlag == "CN")
		{
			//Debug.Log("@@@   @#%@$%^^&^^#$$@##$@#$@!");
			return NumberFormat(_number, NumberConvert_CN, 10000);
		}*/
		return NumberFormat(_number, NumberConvert_EN, 1000);
	}

	static string NumberFormat(float _floatValue, string[] arr, int number)
	{
		float t = _floatValue;
		int len = arr.Length;
		int id = 0;
		while (t >= number && id < len)
		{
			t /= number;
			id++;
		}
		string ret = t.ToString("0.##") + arr[id];
		return ret;
	}
}
