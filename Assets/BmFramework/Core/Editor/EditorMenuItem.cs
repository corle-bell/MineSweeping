using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BmFramework.Core
{
#if UNITY_EDITOR
    public class EditorMenuItem
    {
        [MenuItem("BmFramework/UI管理器/打开编辑器", false)]
        static void Open()
        {
            UIManagerWindow.Open();
        }

        [MenuItem("BmFramework/编辑器配置/刷新", false)]
        static void EditorConfig()
        {
            EditorHelper.ReadConfig();
        }

        [MenuItem("Assets/BmFramework/UI/Create", priority = 0)]
        static void UICreate()
        {
            UICreateWindow.Open();
        }

        [MenuItem("Assets/BmFramework/Create", priority = 0)]
        static void FrameworkCreate()
        {
            CreateHelper.CreatePrefabToCurrentScene("BmFramework");
        }

        [MenuItem("Assets/GetPath", priority = 0)]
        static void GetAssetPath()
        {
            string[] arr = Selection.assetGUIDs;
            if (arr.Length != 0)
            {
                int len = arr.Length > 50 ? 50 : arr.Length;
                List<string> tt = new List<string>();
                for (int i = 0; i < len; i++)
                {
                    tt.Add(AssetDatabase.GUIDToAssetPath(arr[i]));
                }
                tt.Sort();

                string path = "";
                for (int i = 0; i < len; i++)
                {
                    tt.Add(tt[i]);
                    path += tt[i];
                    if (i < len - 1) path += "\r\n";
                }

                GUIUtility.systemCopyBuffer = path;
            }
        }

        [ExecuteInEditMode]
        [MenuItem("BmFramework/初始化FrameworkMain脚本顺序")]
        private static void SetScriptOrder()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.Load(@"UnityEditor");
            System.Type type = asm.GetType("UnityEditor.ScriptExecutionOrderInspector");
            object obj = asm.CreateInstance("UnityEditor.ScriptExecutionOrderInspector");
            System.Reflection.MethodInfo oMethod = type.GetMethod("SetExecutionOrder", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            GameObject go = new GameObject();
            FrameworkMain fs = go.AddComponent<FrameworkMain>();
            MonoScript ms = MonoScript.FromMonoBehaviour(fs); 
            oMethod.Invoke(obj, new object[] { ms, -1200 });
            GameObject.DestroyImmediate(go);
            System.Reflection.MethodInfo apply = type.GetMethod("Apply", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static); apply.Invoke(obj, null);
            EditorUtility.DisplayDialog("Script Execution Order", "添加成功", "确定");
        }

        [MenuItem("Assets/GetResourcesPath", priority = 0)]
        static void GetResourcesPath()
        {
            string[] arr = Selection.assetGUIDs;
            if (arr.Length != 0)
            {
                int len = arr.Length > 50 ? 50 : arr.Length;
                List<string> tt = new List<string>();
                for (int i = 0; i < len; i++)
                {
                    string p = AssetDatabase.GUIDToAssetPath(arr[i]);
                    int id = p.IndexOf("Resources") + 10;
                    int p_id = p.LastIndexOf(".");
                    tt.Add(p.Substring(id, (p_id - id)));
                }
                tt.Sort();

                string path = "";
                for (int i = 0; i < len; i++)
                {
                    tt.Add(tt[i]);
                    path += tt[i];
                    if (i < len - 1) path += "\r\n";
                }

                GUIUtility.systemCopyBuffer = path;
            }
        }
    }
#endif

}