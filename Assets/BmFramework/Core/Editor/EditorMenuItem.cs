using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BmFramework.Core
{
    public class EditorMenuItem
    {
        [MenuItem("Tools/UI管理器/打开编辑器", false)]
        static void Open()
        {
            UIManagerWindow.Open();
        }

        [MenuItem("Assets/BmFramework/UI/Create",  priority = 0)]
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
                for (int i=0; i<len; i++)
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
    }

}