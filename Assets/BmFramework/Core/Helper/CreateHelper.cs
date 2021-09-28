using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
namespace BmFramework.Core
{
#if UNITY_EDITOR
    public class CreateHelper
    {
        internal static string UIPrefab_Path = "Assets/BmFramework/Templete/Prefab";
        internal static string UIScript_Path = "Assets/BmFramework/Templete/Script";

        public static void CreateUIPrefab(string _path, string _name)
        {
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            string path = UIPrefab_Path + "/UITemplete.prefab";
            GameObject tmp = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            tmp = GameObject.Instantiate(tmp);
            tmp.AddComponentByString(_name);
            bool success = false;
            PrefabUtility.SaveAsPrefabAsset(tmp, string.Format("{0}/{1}.prefab", _path, _name), out success);
            GameObject.DestroyImmediate(tmp);
        }

        public static void CreatePrefabToCurrentScene(string _name)
        {
            string path = string.Format("{0}/{1}.prefab", UIPrefab_Path, _name);
            GameObject tmp = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            tmp = GameObject.Instantiate(tmp);
            tmp.name = _name;
        }

        public static void CreateUIScript(string _path, string _name, bool isAnimation)
        {
            string _ui_path = string.Format("{0}/{1}.cs", _path, _name);
            string _ui_designer_path = string.Format("{0}/{1}.Designer.cs", _path, _name);

            string _ui_templete_script_path = string.Format("{0}/{1}.script", UIScript_Path, isAnimation ? "UIAnimtionTemplete" : "UITemplete");
            string _ui_templete_designer_script_path = string.Format("{0}/{1}.script", UIScript_Path, isAnimation ? "UIAnimtionTemplete.Designer" : "UITemplete.Designer");

            string ui_text = File.ReadAllText(_ui_templete_script_path);
            string ui_designer_text = File.ReadAllText(_ui_templete_designer_script_path);

            ui_text = ui_text.Replace("UITemplete", _name);
            ui_designer_text = ui_designer_text.Replace("UITemplete", _name);

            File.WriteAllText(_ui_path, ui_text);
            File.WriteAllText(_ui_designer_path, ui_designer_text);
        }
    }
#endif
}
