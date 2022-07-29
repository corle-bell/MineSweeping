using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BmFramework.Core
{
    internal class UIManagerWindow : ManagerBaseEdtior
    {
        public static void Open()
        {
            EditorWindow.GetWindow<UIManagerWindow>(false, "UI管理器", true).Show();
        }
        class UINode : IManagerBaseNode
        {
            public string name {
                set
                {
                    _name = value;
                }
                get
                {
                    return _name;
                }
            }

            public string asset_path
            {
                set
                {
                    _asset_path = value;
                }
                get
                {
                    return _asset_path;
                }
            }

            string _name;
            string _asset_path;
        }

        private void OnEnable()
        {
            Scan();

            ClearButton();

            this.AddButton("Find", this.FindPrefab);
            this.AddButton("Open", this.OpenPrefab);
            this.AddButton("Delete", this.DeletePrefab);
        }

        private void OnGUI()
        {
            DrawList();
        }

        protected override void Scan()
        {
            string path = "Assets/Game/Resources/UI";

            nodeList.Clear();

            string[] resFiles = AssetDatabase.FindAssets("t:Prefab", new string[] { path });
            if (resFiles != null)
            {
                for (int i = 0; i < resFiles.Length; i++)
                {
                    string assetName = AssetDatabase.GUIDToAssetPath(resFiles[i]); ;
                    string[] sarr = assetName.Split(new char[] { '/' });
                    UINode tt = new UINode();
                    tt.name = assetName.Replace(".prefab", "").Replace("Assets/Game/Resources/UI", "");
                    tt.asset_path = assetName;
                    nodeList.Add(tt);
                }
            }

            nodeList.Sort((left, right) =>
            {
                return left.name.CompareTo(right.name);
            });
        }

        void FindPrefab(int _id)
        {
            UINode t = nodeList[_id] as UINode;
            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(t.asset_path);
            Selection.activeObject = obj;
        }

        void OpenPrefab(int _id)
        {
            UINode t = nodeList[_id] as UINode;
            int insId = AssetDatabase.LoadAssetAtPath<GameObject>(t.asset_path).GetInstanceID();
            AssetDatabase.OpenAsset(insId);
            GUIUtility.ExitGUI();
        }

        void DeletePrefab(int _id)
        {
            EditorMessgeBox.Open("提示", "确定要删除么？", "是", "否", (bool isSure) =>
            {
                if(isSure)
                {
                    UINode t = nodeList[_id] as UINode;

                    AssetDatabase.DeleteAsset(t.asset_path);
                    AssetDatabase.DeleteAsset("Assets/Game/Script/UI" + t.name + ".cs");

                    string path = Application.dataPath.Replace("Assets", "Assets/Game/Script/UI" + t.name + ".Designer.cs");
                    if(FileHandle.instance.isExistFile(path))
                    {
                        AssetDatabase.DeleteAsset("Assets/Game/Script/UI" + t.name + ".Designer.cs");
                    }

                    AssetDatabase.Refresh();
                }
            });

            

            GUIUtility.ExitGUI();
        }
    }
}
