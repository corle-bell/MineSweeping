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
    }

}