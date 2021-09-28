﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UICommpentExtensionMethodes
{
    public static void SetAlpha(this Graphic graphic, float a)
    {
        Color t = graphic.color;
        t.a = a;
        graphic.color = t;
    }

    public static void FadeTo(this Graphic graphic, float a, float _time)
    {
        Color t = graphic.color;
        DG.Tweening.DOTween.To(() => t.a, x => SetAlpha(graphic, x), a, _time);
    }
}