using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEditor;

public class ScriptCreateDesc : UnityEditor.AssetModificationProcessor
{
    private static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if (path.EndsWith(".cs"))
        {
            string strContent = File.ReadAllText(path);
            strContent = strContent.Replace("#AuthorName#", "Cyy").Replace("#CreateDate#", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            File.WriteAllText(path, strContent);
            AssetDatabase.Refresh();
        }
    }
}