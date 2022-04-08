using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensionMethodes
{
    public static Vector2 Abs(this Vector2 vector)
    {
        for (int i = 0; i < 2; ++i) vector[i] = Mathf.Abs(vector[i]);
        return vector;
    }

    public static Vector2 DividedBy(this Vector2 vector, Vector2 divisor)
    {
        for (int i = 0; i < 2; ++i) vector[i] /= divisor[i];
        return vector;
    }

    public static Vector2 Max(this Rect rect)
    {
        return new Vector2(rect.xMax, rect.yMax);
    }

    public static Vector2 Max(this Vector2 vec, Vector2 oper)
    {
        Vector3 ret = vec;
        ret.x = Mathf.Max(ret.x, oper.x);
        ret.y = Mathf.Max(ret.y, oper.y);
        return ret;
    }

    public static Vector2 IntersectionWithRayFromCenter(this Rect rect, Vector2 pointOnRay)
    {
        Vector2 pointOnRay_local = pointOnRay - rect.center;
        Vector2 edgeToRayRatios = (rect.Max() - rect.center).DividedBy(pointOnRay_local.Abs());
        return (edgeToRayRatios.x < edgeToRayRatios.y) ?
            new Vector2(pointOnRay_local.x > 0 ? rect.xMax : rect.xMin,
                pointOnRay_local.y * edgeToRayRatios.x + rect.center.y) :
            new Vector2(pointOnRay_local.x * edgeToRayRatios.y + rect.center.x,
                pointOnRay_local.y > 0 ? rect.yMax : rect.yMin);
    }


    public static Vector3 Muti(this Vector3 Oper1, Vector3 Oper2)
    {
        Oper1.x *= Oper2.x;
        Oper1.y *= Oper2.y;
        Oper1.z *= Oper2.z;
        return Oper1;
    }

    public static Vector3 Normalized_XZ(this Vector3 Oper1, Vector3 Oper2)
    {
        var t = Oper2;
        t.y = Oper1.y;
        return (Oper1-t).normalized;
    }


    public static Vector3 Abs(this Vector3 vec)
    {
        for (int i = 0; i < 3; ++i) vec[i] = Mathf.Abs(vec[i]);
        return vec;
    }

    public static Vector3 Max(this Vector3 vec, Vector3 oper)
    {
        Vector3 ret = vec;
        ret.x = Mathf.Max(ret.x, oper.x);
        ret.y = Mathf.Max(ret.y, oper.y);
        ret.z = Mathf.Max(ret.z, oper.z);
        return ret;
    }
}
