using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIProgressBar_Fill : UIProgressBar
{
    public Image fill;     
    private void Start()
    {
        Init();
    }

    public override void SetPercent(float _t)
    {
        base.SetPercent(_t);
        fill.fillAmount = _t;

        UpdateNumber();
    }
}
