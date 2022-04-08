using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BmReplaceWindow : EditorWindow
{
    [MenuItem("EditorTools/替换助手", false)]
    static void Open()
    {
        EditorWindow.GetWindow<BmReplaceWindow>(false, "替换助手", true).Show();
    }

    public GameObject prefab;
    private void OnGUI()
    {
        EditorGUILayout.HelpBox("替换预设", MessageType.Info);
        prefab = (GameObject)EditorGUILayout.ObjectField("要替换的预设", prefab, typeof(GameObject));
        if (GUILayout.Button("替换为预设"))
        {
            List<GameObject> objects = new List<GameObject>();
            objects.AddRange(Selection.gameObjects);
            objects.Sort((a, b) =>
            {
                var o = a.transform.GetSiblingIndex() - b.transform.GetSiblingIndex();
                return o;
            });


            foreach (var obj in objects)
            {
                Transform p = obj.transform.parent;                
                GameObject newObj = PrefabUtility.InstantiatePrefab(prefab, p) as GameObject;
                EditorUtility.CopySerialized(obj.transform, newObj.transform);

                DestroyImmediate(obj);
            }

            Undo.RecordObjects(Selection.gameObjects, "Delete");
            
        }
    }
}
