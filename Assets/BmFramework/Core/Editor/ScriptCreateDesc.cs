using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEditor;

namespace BmFramework.Core
{
    public class ScriptCreateDesc : UnityEditor.AssetModificationProcessor
    {
        private static void OnWillCreateAsset(string path)
        {
            path = path.Replace(".meta", "");
            if (path.EndsWith(".cs"))
            {
                string fileName = FileHandle.instance.GetFileName(path, ".cs");
                string strContent = EditorHelper.script_init;
                strContent = strContent.Replace("#AuthorName#", "Cyy").Replace("#CreateDate#", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                strContent = strContent.Replace("#SCRIPTNAME#", fileName).Replace("#NOTRIM#", "//code here");

                File.WriteAllText(path, strContent);
                AssetDatabase.Refresh();
            }
        }
    }
}