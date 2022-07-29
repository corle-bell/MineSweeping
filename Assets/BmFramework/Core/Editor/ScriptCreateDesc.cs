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
                string strContent = File.ReadAllText(path);
                
                string title = EditorHelper.script_title;
                title = title.Replace("#AuthorName#", "Cyy").Replace("#CreateDate#", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                if (strContent.Contains($"{fileName} : MonoBehaviour"))
                {
                    strContent = EditorHelper.script_init;
                    strContent = strContent.Replace("#SCRIPTNAME#", fileName).Replace("#NOTRIM#", "//code here");
                    strContent = strContent.Replace("#TITLE#", title);
                }
                else
                {   
                    strContent = strContent.Replace("#TITLE#", title);
                }
                
                

                File.WriteAllText(path, strContent);
                AssetDatabase.Refresh();
            }
        }
    }
}