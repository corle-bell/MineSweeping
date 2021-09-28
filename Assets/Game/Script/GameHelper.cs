using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BmFramework.Core;


public class GameDefine
{
    public static string[] levelData = new string[] { "Level01", "Level02", "Level03", "Level04", "Level05", "Level06", "Level07" };
    public static int level = 0;
}
public class GameHelper
{
    public static void LoadLevel()
    {
        int id = GameDefine.level % GameDefine.levelData.Length;
        SceneHelper.LoadScene(GameDefine.levelData[id]);
    }

    public static void LoadLevelAsync(System.Action<AsyncOperation> callback)
    {
        int id = GameDefine.level % GameDefine.levelData.Length;
        SceneHelper.LoadSceneAsync(GameDefine.levelData[id], callback);
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
