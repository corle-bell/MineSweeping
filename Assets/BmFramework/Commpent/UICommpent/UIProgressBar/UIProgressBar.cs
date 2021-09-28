using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using BmFramework.Core;
public interface IUIProgressObserve
{
    float GetPercent();
}

public class UIProgressBar : MonoBehaviour
{
    public Image maxIcon;
    [EnumName("标签")]
    public string pTag;
    public float percent;
    [EnumName("比例")]
    public int convert = 100;
    [EnumName("比例显示")]
    public NumberText number;
    private IUIProgressObserve progressObserve;
    public virtual void Init()
    {
        maxIcon?.SetAlpha(0);
    }

    public virtual void SetPercent(float _t)
    {
        percent = _t;
        if (_t==1)
        {
            if(maxIcon!=null)
            {
                maxIcon.FadeTo(1, 0.5f);
            }
        }
    }

    protected void UpdateNumber()
    {
        if(number!=null) number.number = (int)(convert * percent);
    }

    public virtual void Clear()
    {

    }

    public virtual void Logic()
    {
        if(progressObserve!=null) SetPercent(progressObserve.GetPercent());
    }

    public virtual void RegisterObserver()
    {
        if (pTag == null || pTag.Length == 0) return;
        NotifacitionManager.AddObserver(NotifyType.Msg_ProgressObserve, this.OnProgressObserve);
    }

    public virtual void RemoveObserver()
    {
        if (pTag == null || pTag.Length == 0) return;
        NotifacitionManager.RemoveObserver(NotifyType.Msg_ProgressObserve, this.OnProgressObserve);
    }

    private void Awake()
    {
        RegisterObserver();
    }

    private void OnDestroy()
    {
        RemoveObserver();
        progressObserve = null;
    }

    private void OnProgressObserve(NotifyEvent _event)
    {
        if (_event.Cmd == pTag)
        {
            progressObserve = _event.Sender as IUIProgressObserve;
        }
    }

    public void AnimationTo(float value, float time)
    {
        DOTween.To(() => percent, x => percent = x, value, time).OnUpdate(()=> {
            SetPercent(percent);
        });
    }
}
