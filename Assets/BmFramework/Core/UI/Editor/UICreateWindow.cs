using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BmFramework.Core
{
    internal class UICreateWindow:EditorWindow
    {
       
        public static void Open()
        {
            UICreateWindow t = EditorWindow.GetWindow<UICreateWindow>(false, "UI创建", true);
            t.maxSize = new Vector2(500, 400);
            t.minSize = new Vector2(500, 400);
            t.Show();
        }


        public string uiName="UI";
        public string scriptDefaultPath="Assets/Game/Script/UI";
        public string prefabDefaultPath = "Assets/Game/Resources/UI";
        public bool isUpdate;
        public bool isAnimation;
        void OnGUI()
        {
            uiName = EditorGUILayout.TextField("UI名称:", uiName);
            scriptDefaultPath = EditorGUILayout.TextField("脚本保存路径:", scriptDefaultPath);
            prefabDefaultPath = EditorGUILayout.TextField("UI预制体保存路径:", prefabDefaultPath);
            isAnimation = EditorGUILayout.Toggle("是否存在出入/场动画",isAnimation);

            if (GUILayout.Button("Create"))
            {
                CreateScript(uiName);
            }
        }

        private void CreateScript(string _name)
        {
            CreateHelper.CreateUIScript(scriptDefaultPath, _name, isAnimation);
            EditorUtility.DisplayCancelableProgressBar("提示", "正在编译~", 0.9f);
            AssetDatabase.Refresh();
            isUpdate = true;
        }

        private void Update()
        {
            if (!EditorApplication.isCompiling && isUpdate)
            {
                CreateHelper.CreateUIPrefab(prefabDefaultPath, uiName, isAnimation);

                isUpdate = false;
                EditorUtility.ClearProgressBar();
            }
        }

    }

}

