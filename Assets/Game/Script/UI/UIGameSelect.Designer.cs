using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BmFramework.Core;

public partial class UIGameSelect : UIAnimationRoot
{

    public InputField Width;
    public InputField Height;
    public InputField MineNum;

    public int _Width;
    public int _Height;

    void InitChildCommpent()
    {

        _Width = (Screen.width ) / 75;
        _Height = (Screen.height- 20 - 120) / 75;

        int _Num = _Width * _Height / 5;

        Width.text = _Width.ToString();
        Height.text = _Height.ToString();
        MineNum.text = _Num.ToString();
    }
}
