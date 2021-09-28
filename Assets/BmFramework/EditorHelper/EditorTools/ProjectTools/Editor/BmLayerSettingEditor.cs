using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using UnityEditorInternal;

[CustomEditor(typeof(BmLayerSetting))]
public class BmLayerSettingEditor : Editor
{
    static string filePath = "Assets/TagAndLayers.asset";
    [MenuItem("EditorTools/工程配置/TagAndLayers/创建配置", false)]
    static void Create()
    {
        // 实例化类  Bullet
        ScriptableObject group = ScriptableObject.CreateInstance<BmLayerSetting>();

        // 如果实例化 Bullet 类为空，返回
        if (!group)
        {
            Debug.LogWarning("group not found");
            return;
        }


        // 生成自定义资源到指定路径
        AssetDatabase.CreateAsset(group, filePath);
    }

    [MenuItem("EditorTools/工程配置/TagAndLayers/编辑", false)]
    static void Select()
    {
        EditorUtility.DisplayProgressBar("提示", "打开中~~~", 0);
        UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(filePath);
        Selection.activeObject = obj;
        EditorUtility.ClearProgressBar();
    }

   
    private void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("操作", MessageType.Info);
        if (GUILayout.Button("读取当前工程配置"))
        {
            ReadAllSetting();
        }
        if (GUILayout.Button("输出配置到当前工程"))
        {
            SaveAllSetting();
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("当前级别: ");
        int level = QualitySettings.GetQualityLevel();
        level =EditorGUILayout.Popup(level, QualitySettings.names);
        if(level!= QualitySettings.GetQualityLevel())
        {
            QualitySettings.SetQualityLevel(level);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.HelpBox("数据", MessageType.Info);
        base.OnInspectorGUI();
    }

    private void SaveAllSetting()
    {
        
    }
    private void ReadAllSetting()
    {
        BmLayerSetting _setting = target as BmLayerSetting;
        int count = 32;

        _setting.layers = (string [])InternalEditorUtility.layers.Clone();
        _setting.tags = (string[])InternalEditorUtility.tags.Clone();


        EditorUtility.SetDirty(target);
    }

}
