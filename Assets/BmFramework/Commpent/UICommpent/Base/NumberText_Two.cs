using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberText_Two : NumberText
{
	protected override string FormatString()
	{
		return "";
	}

	public void FormatTwoNumber(float _a, float _b)
	{
		_floatValue = _a;
		this.text = string.Format(numberFormat, FormatNumber(_floatValue), FormatNumber(_b));
	}
}
