using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NumberText : Text {
	public int floatlen = 0;
	public string numberFormat = "{0}";
	public virtual float number {
		set{
			_floatValue = value;
			this.text = FormatString();
		}
		get{return _floatValue;}
	}
	protected float _floatValue;
	protected float _floatValueDest;
	public float initFloatValue;
	protected override void Awake() {
		if(initFloatValue!=0)
		{
			number = initFloatValue;
			Debug.Log("name: "+name);
		}
	}

	protected virtual string FormatString()
	{
		return string.Format(numberFormat, FormatNumber(_floatValue));
	}

	protected virtual string FormatNumber(float _number)
    {
		return floatlen > 0 ? (_number.ToString("F" + floatlen)) : ((int)_number).ToString();
	}

	public void FormatTwoNumber(float _a, float _b)
	{
		_floatValue = _a;
		this.text = string.Format(numberFormat, FormatNumber(_floatValue), FormatNumber(_b));
	}

	public void Format()
	{
		this.text = FormatString();
	}

	Tween lastTween;
	public void AnimationTo(float _number, float _time=2.0f)
    {
		if(lastTween!=null && !lastTween.IsComplete())
        {
			lastTween.Complete();
		}
		lastTween = DOTween.To(() => _floatValue, x => number = x, _number, _time).SetUpdate(true);
	}


	public static void ChangeNumber(GameObject tmp, float _value, bool isChild=false, string _childName=null)
	{
		
		if(_childName!=null)
		{
			NumberText nt = FrameworkTools.FindChildComponentByName<NumberText>(tmp, _childName, isChild);
			nt.number = _value;
		}
		else if(isChild==false)
		{
			NumberText nt = tmp.GetComponent<NumberText>();
			nt.number = _value;
		}
		else if(isChild)
		{
			NumberText nt = tmp.GetComponentInChildren<NumberText>();
			nt.number = _value;
		}
	}

	public static void ChangeNumber(Transform tmp, float _value, bool isChild=false, string _childName=null)
	{
		ChangeNumber(tmp.gameObject, _value, isChild, _childName);
	}

}
