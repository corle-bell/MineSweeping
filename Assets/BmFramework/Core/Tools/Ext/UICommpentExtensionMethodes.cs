using System.Collections;
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

    public static void SetAlpha(this SpriteRenderer render, float a)
    {
        Color t = render.color;
        t.a = a;
        render.color = t;
    }

    public static DG.Tweening.Tweener FadeTo(this Graphic graphic, float a, float _time)
    {
        Color t = graphic.color;
        return DG.Tweening.DOTween.To(() => t.a, x => SetAlpha(graphic, x), a, _time);
    }

    public static void ColorFitler(this Graphic graphic, float fitler)
    {
        graphic.materialForRendering.SetFloat("_Filter", fitler);
    }
}
