using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BmFramework.Core;


public class GameDefine
{
    public static int level = 0;
}
public class GameHelper
{
    public static void LoadLevel()
    {
        
    }

    public static void LoadLevelAsync(System.Action<AsyncOperation> callback)
    {
        
    }

    public static void RealodLevel()
    {
        SceneHelper.Reload();
    }

    public static Bounds GetBounds(Transform transform)
    {
        Quaternion currentRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f); //先重置角度

        Bounds bounds = new Bounds(transform.position, Vector3.zero);

        foreach (Renderer renderer in transform.GetComponentsInChildren<Renderer>())
        {
            bounds.Encapsulate(renderer.bounds);
        }

        Vector3 localCenter = bounds.center - transform.position;
        bounds.center = localCenter;
        Debug.Log("The local bounds of this model is " + bounds);
        transform.rotation = currentRotation;//恢复旋转
        return bounds;
    }
}
