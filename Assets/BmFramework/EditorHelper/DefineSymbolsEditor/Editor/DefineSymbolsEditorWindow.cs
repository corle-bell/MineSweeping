using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;
using System;

namespace Bm.DefineSymbols
{
    public class DefineSymbolsEditorWindow : EditorWindow
    {
        [MenuItem("Tools/宏定义编辑器/打开", false)]
        static void Open()
        {
            EditorWindow.GetWindow<DefineSymbolsEditorWindow>(false, "宏定义编辑器", true).Show();
        }

        public string storePath = "Assets/BmDevelop/DefineSymbolsEditor/DefineSymbolsData_{0}.asset";
        public DefineSymbolsDataNode node = new DefineSymbolsDataNode();
        public DefineSymbolsData defineSymbolsData;
        #region Editor Fun
        private void OnEnable()
        {
            Debug.Log("当前平台:"+EditorUserBuildSettings.selectedBuildTargetGroup);

            storePath = string.Format(storePath, EditorUserBuildSettings.selectedBuildTargetGroup);

            defineSymbolsData = AssetDatabase.LoadAssetAtPath<DefineSymbolsData>(storePath);
            if(defineSymbolsData==null)
            {
                defineSymbolsData = ScriptableObject.CreateInstance<DefineSymbolsData>();
                AssetDatabase.CreateAsset(defineSymbolsData, storePath);
            }
            SysDefineCheck();
        }

        private void OnDisable()
        {
            EditorApplication.update -= Update;
        }


        void SysDefineCheck()
        {
            string data = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            if (data.Length == 0) return;
            string[] arr = data.Split(new char[] { ';'});

            foreach(var item in arr)
            {
                if(!isExsitInList(item))
                {
                    var t = new DefineSymbolsDataNode();
                    t.name = item;
                    t.desc = "";
                    t.status = true;
                    defineSymbolsData.list.Add(t);
                }
            }
  
        }

        bool isExsitInList(string _data)
        {
            foreach(var item in defineSymbolsData.list)
            {
                if(item.name.Equals(_data))
                {
                    return true;
                }
            }
            return false;
        }


        Vector2 scroll;
        private void OnGUI()
        {
            EditorGUILayout.Space();
            BmFramework.Core.EditorTools.DrawTitle("基本信息", new Color(0.15f, 0.15f, 0.15f));

            node.name = EditorGUILayout.TextField("宏名称:", node.name);
            node.desc = EditorGUILayout.TextField("说明:", node.desc);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("添加"))
            {
                 var t = FrameworkTools.DeepCopyByBinary<DefineSymbolsDataNode>(node);
                 defineSymbolsData.list.Add(t);
            }
            if (GUILayout.Button("格式化输出"))
            {
                Debug.Log(Format());                
            }
            if (GUILayout.Button("输出编辑器配置"))
            {
                Debug.Log(PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup));
            }
            if (GUILayout.Button("写入编辑器配置"))
            {
                WriteToEditor();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            BmFramework.Core.EditorTools.DrawTitle("列表信息", new Color(0.15f, 0.15f, 0.15f));            
            EditorGUILayout.Space();
            scroll = EditorGUILayout.BeginScrollView(scroll);

            for (int i=0; i<defineSymbolsData.list.Count; i++)
            {
                var item = defineSymbolsData.list[i];
                EditorGUILayout.BeginHorizontal();

                item.name = EditorGUILayout.TextField("宏名称:", item.name);
                item.desc = EditorGUILayout.TextField("说明:", item.desc);
                item.status = EditorGUILayout.Toggle("装填:", item.status);

                if (GUILayout.Button("移除"))
                {
                    defineSymbolsData.list.RemoveAt(i);
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();

            if(GUI.changed)
            {
                EditorUtility.SetDirty(defineSymbolsData);
            }
        }
        #endregion

        #region draw fun
        #endregion

        #region private fun
        private void WriteToEditor()
        {
            EditorUtility.DisplayCancelableProgressBar("提示", "正在写入编辑器~", 0.9f);
            string ret = Format();
            if(ret.Length==0)
            {
                Debug.Log("无可用内容写入!");
            }
            else
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, Format());
            }

            EditorApplication.update += Update;
        }

        private void Update()
        {
            if (!EditorApplication.isCompiling)
            {
                EditorUtility.ClearProgressBar();
                EditorApplication.update -= Update;
            }
        }
        private string Format()
        {
            string ret="";
            List<string> outName = new List<string>();
            for (int i = 0; i < defineSymbolsData.list.Count; i++)
            {
                var item = defineSymbolsData.list[i];
                if(item.status)
                {
                    outName.Add(item.name);
                }
            }

            for (int i = 0; i < outName.Count; i++)
            {
                ret += outName[i];
                if (i + 1 < outName.Count) ret += ";";
            }
            return ret;
        }
     
        void Log(string _string)
        {
            Debug.Log("EnumEditor: "+ _string);
        }
        #endregion

        #region public fun
        #endregion

    }
}

