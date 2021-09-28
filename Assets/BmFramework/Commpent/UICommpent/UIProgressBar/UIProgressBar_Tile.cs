using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIProgressBar_Tile : UIProgressBar
{
    [EnumName("开始位置")]
    public UIDirPos dirStartPos;

    private Image[] skillArr;
    private int index = 0;
    private void Awake()
    {
        skillArr = GetComponentsInChildren<Image>();

        foreach(var i in skillArr)
        {
            i.SetAlpha(0);
        }

        switch (dirStartPos)
        {
            case UIDirPos.Top:
                index = 0;
                break;
            case UIDirPos.Bottom:
                index = skillArr.Length-1;
                break;
        }

        Init();
    }

    public override void SetPercent(float _t)
    {

        base.SetPercent(_t);

        int stopNumber = 0;
        switch (dirStartPos)
        {
            case UIDirPos.Top:
                if (index >= skillArr.Length) return;
                stopNumber = (int)(skillArr.Length * _t);
                for (int i = index; i< stopNumber; i++)
                {
                    skillArr[i].FadeTo(1, 0.5f);
                }
                index = stopNumber;
                break;
            case UIDirPos.Bottom:
                if (index < 0) return;
                stopNumber = skillArr.Length-(int)(skillArr.Length * _t)-1;
                for (int i = index; i>stopNumber; i--)
                {
                    skillArr[i].FadeTo(1, 0.5f);
                }
                index = stopNumber;
                break;
        }
    }

    public override void Clear()
    {
        for (int i = 0; i < skillArr.Length; i++)
        {
            skillArr[i].SetAlpha(0);
        }

        switch (dirStartPos)
        {
            case UIDirPos.Top:
                index = 0;
                break;
            case UIDirPos.Bottom:
                index = skillArr.Length - 1;
                break;
        }
    }

}
