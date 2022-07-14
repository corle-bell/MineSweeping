using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;

namespace Bm.EditorTool
{
 
    public class BmAssetCreator : EditorWindow
    {
        #region Static Method
        [MenuItem("Tools/AssetCreator/打开", false)]
        public static void Open()
        {
            EditorWindow.GetWindow<BmAssetCreator>(false, "AssetCreator", true).Show();
        }
        #endregion


        private void OnEnable()
        {
            GetClass();
        }

        private void GetClass()
        {
            //这里的代码 引用自 https://www.cnblogs.com/xxj-jing/archive/2011/09/29/2890100.html
            //加载程序集信息
            Assembly ass = Assembly.Load("Assembly-CSharp");
            //Type[] allTypes = asm.GetTypes();    //这个得到的类型有点儿多
            Type[] types = ass.GetExportedTypes(); //还是用这个比较好，得到的都是自定义的类型

            // 验证指定自定义属性（使用的是 4.0 的新语法，匿名方法实现的，不知道的同学查查资料吧！）
            Func<System.Attribute[], bool> IsAtt1 = o =>
            {
                foreach (System.Attribute a in o)
                {
                    if (a is AssetCreatorAttribute)
                        return true;
                }
                return false;
            };

            //查找具有 Attribute.Atts.Att1 特性的类型（使用的是 linq 语法）
            Type[] CosType = types.Where(o =>
            {
                return IsAtt1(System.Attribute.GetCustomAttributes(o, true));
            }).ToArray();

            AssetClassList = new string[CosType.Length];
            for (int i=0; i<CosType.Length; i++)
            {
                Type t = CosType[i];
                AssetClassList[i] = t.Name;
            }
        }


        string[] AssetClassList;
        private string assetPath;
        private int selectId;
        private void OnGUI()
        {
            EditorGUILayout.Space();

            selectId = EditorGUILayout.Popup("选择要创建类型:", selectId, AssetClassList);

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            assetPath = EditorGUILayout.TextField(assetPath);
            if (GUILayout.Button("保存的路径"))
            {
                assetPath = EditorUtility.SaveFilePanelInProject("选择路径", AssetClassList[selectId], "asset", "");
                Repaint();
            }
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("创建"))
            {
                var t = ScriptableObject.CreateInstance(AssetClassList[selectId]);
                AssetDatabase.CreateAsset(t, assetPath);
            }
        }


    }
}

