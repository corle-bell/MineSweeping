using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bm.Lerp;
public class UIProgressBar_Lerp : UIProgressBar
{
    public BmLerpBase bmLerpBase;
    public override void SetPercent(float _t)
    {
        base.SetPercent(_t);
        bmLerpBase.Lerp(_t);

        UpdateNumber();
    }
}
