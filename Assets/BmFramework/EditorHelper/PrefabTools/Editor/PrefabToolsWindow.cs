using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class PrefabToolsWindow : EditorWindow
{
    public static string prefabSavePath;
    [MenuItem("Assets/PrefabTools/选择路径", priority = 0)]
    static void GetPathByAsset()
    {
        string [] arr =  Selection.assetGUIDs;
        if(arr.Length!=0)
        {
            prefabSavePath = AssetDatabase.GUIDToAssetPath(arr[0]);
        }
    }

    [MenuItem("PrefabTools/保存预设")]
    static void SavePrefabVariant()
    {
        if(prefabSavePath!=null && prefabSavePath =="")
        {
            Debug.Log("请先选择路径");
            return ;
        }

        GameObject[] selects = Selection.gameObjects;

        for(int i=0; i<selects.Length; i++)
        {
            PrefabUtility.SaveAsPrefabAssetAndConnect(selects[i], string.Format("{0}/{1}.prefab", prefabSavePath, selects[i].name), InteractionMode.UserAction);
        }
        prefabSavePath = "";
    }
}
