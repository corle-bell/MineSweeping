using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;
using System;
using BmFramework.Core;

namespace Bm.DefineSymbols
{
    public class DefineSymbolsEditorWindow : EditorWindow
    {
        [MenuItem("Tools/宏定义编辑器/打开", false)]
        static void Open()
        {
            EditorWindow.GetWindow<DefineSymbolsEditorWindow>(false, "宏定义编辑器", true).Show();
        }

        public string storePath = "Assets/BmFramework/EditorHelper/DefineSymbolsEditor/Setting";
        public DefineSymbolsDataNode node = new DefineSymbolsDataNode();
        public DefineSymbolsData defineSymbolsData;
        public int selectId;
        public string assetNameAdd;
        public string [] assetNames;
        #region Editor Fun
        private void OnEnable()
        {
            Scan();

        }

        private void OnDisable()
        {
            EditorApplication.update -= Update;
        }

       

        Vector2 scroll;
        private void OnGUI()
        {
            EditorGUILayout.Space();
            EditorTools.DrawTitle("基本信息", new Color(0.15f, 0.15f, 0.15f));

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("扫描"))
            {
                Scan();
            }

            selectId = EditorGUILayout.Popup(selectId, assetNames);
            if (GUILayout.Button("读取"))
            {
                Load();
            }

            assetNameAdd = EditorGUILayout.TextField("配置名称:", assetNameAdd);
            if (GUILayout.Button("新建"))
            {
                Create(assetNameAdd);
            }
            if (GUILayout.Button("从选择文件克隆"))
            {
                CloneData(assetNameAdd);
            }

            EditorGUILayout.EndHorizontal();


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
            EditorTools.DrawTitle("列表信息", new Color(0.15f, 0.15f, 0.15f));            
            EditorGUILayout.Space();

            if(defineSymbolsData!=null)
            {
                scroll = EditorGUILayout.BeginScrollView(scroll);

                for (int i = 0; i < defineSymbolsData.list.Count; i++)
                {
                    var item = defineSymbolsData.list[i];
                    EditorGUILayout.BeginHorizontal();

#if UNITY_2019_4_OR_NEWER
                    item.name = EditorGUILayout.TextField("宏名称:", item.name, GUI.skin.customStyles[525]);
                    item.desc = EditorGUILayout.TextField("说明:", item.desc, GUI.skin.customStyles[525]);
#else
item.name = EditorGUILayout.TextField("宏名称:", item.name, GUI.skin.customStyles[532]);
                    item.desc = EditorGUILayout.TextField("说明:", item.desc, GUI.skin.customStyles[532]);
#endif
                    item.status = EditorGUILayout.Toggle("状态:", item.status);

                    if (GUILayout.Button("移除"))
                    {
                        defineSymbolsData.list.RemoveAt(i);
                    }

                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndScrollView();

                if (GUI.changed)
                {
                    EditorUtility.SetDirty(defineSymbolsData);
                }
            }

            
        }
#endregion

#region draw fun
#endregion

#region private fun
        private void Create(string _name)
        {
            defineSymbolsData = ScriptableObject.CreateInstance<DefineSymbolsData>();
            AssetDatabase.CreateAsset(defineSymbolsData, GetAssetPath(_name));

            System.Array.Resize(ref assetNames, assetNames.Length + 1);
            selectId = assetNames.Length - 1;
            assetNames[selectId] = _name;
        }
        private void CloneData(string _name)
        {
            if(defineSymbolsData==null)
            {
                Load();
            }
            defineSymbolsData = ScriptableObject.Instantiate(defineSymbolsData);
            AssetDatabase.CreateAsset(defineSymbolsData, GetAssetPath(_name));

            System.Array.Resize(ref assetNames, assetNames.Length + 1);
            selectId = assetNames.Length - 1;
            assetNames[selectId] = _name;
        }
        void Scan()
        {
            string[] resFiles = AssetDatabase.FindAssets("t:DefineSymbolsData", new string[] { storePath });
            if (resFiles != null)
            {
                assetNames = new string[resFiles.Length];
                for (int i = 0; i < resFiles.Length; i++)
                {
                    string assetName = AssetDatabase.GUIDToAssetPath(resFiles[i]); ;
                    string[] sarr = assetName.Split(new char[] { '/' });
                    assetNames[i] = sarr[sarr.Length - 1].Replace(".asset", "");
                }
            }
        }

        string GetAssetPath(string _name)
        {
            return string.Format("{0}/{1}.asset", storePath, _name);
        }

        void Load()
        {
            defineSymbolsData = AssetDatabase.LoadAssetAtPath<DefineSymbolsData>(GetAssetPath(assetNames[selectId]));
            SysDefineCheck();
        }

        void SysDefineCheck()
        {
            string data = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            if (data.Length == 0) return;
            string[] arr = data.Split(new char[] { ';' });

            foreach (var item in arr)
            {
                if (!isExsitInList(item))
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
            foreach (var item in defineSymbolsData.list)
            {
                if (item.name.Equals(_data))
                {
                    return true;
                }
            }
            return false;
        }
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

