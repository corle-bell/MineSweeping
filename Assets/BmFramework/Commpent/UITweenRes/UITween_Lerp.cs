using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BmFramework.Core;
public class UITween_Lerp : UIAnimationRes
{
    public Bm.Lerp.BmLerpAnimation tweenOpen;
    public Bm.Lerp.BmLerpAnimation tweenClose;

    public override void Open()
    {
        if (tweenOpen == null)
        {
            Debug.Log("No Tween Find");
            return;
        }
        if (!tweenOpen.autoPlay) tweenOpen.Play(1);
        tweenOpen.AddEventByPercent(0, () =>
        {
            root.OnOpenAnimation();
        });
        tweenOpen.AddEventByPercent(1, () =>
        {
            root.OnOpenAnimationEnd();
        });
    }
    public override void Close()
    {
        if (tweenClose == null)
        {
            Debug.Log("No Tween Find");
            return;
        }
        if (!tweenClose.autoPlay) tweenClose.Play(1);

        tweenClose.AddEventByPercent(0, () =>
        {
            root.OnCloseAnimation();
        });
        tweenClose.AddEventByPercent(1, () =>
        {
            root.OnCloseAnimationEnd();
        });
        
    }

    private void Reset()
    {
        root = GetComponent<UIAnimationRoot>();
    }
}
