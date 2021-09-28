using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[CustomEditor(typeof(BmQualitySetting))]
public class BmQualitySettingEditor : Editor
{
    static string filePath = "Assets/Quality.asset";
    [MenuItem("EditorTools/工程配置/Quality/创建配置", false)]
    static void Create()
    {
        // 实例化类  Bullet
        ScriptableObject group = ScriptableObject.CreateInstance<BmQualitySetting>();

        // 如果实例化 Bullet 类为空，返回
        if (!group)
        {
            Debug.LogWarning("group not found");
            return;
        }


        // 生成自定义资源到指定路径
        AssetDatabase.CreateAsset(group, filePath);
    }

    [MenuItem("EditorTools/工程配置/Quality/编辑", false)]
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
        BmQualitySetting _setting = target as BmQualitySetting;

        QualitySettings.particleRaycastBudget = _setting.particleRaycastBudget;
        QualitySettings.softVegetation = _setting.softVegetation;
        QualitySettings.vSyncCount = _setting.vSyncCount;
        QualitySettings.antiAliasing = _setting.antiAliasing;
        QualitySettings.asyncUploadTimeSlice = _setting.asyncUploadTimeSlice;
        QualitySettings.asyncUploadBufferSize = _setting.asyncUploadBufferSize;
        QualitySettings.asyncUploadPersistentBuffer = _setting.asyncUploadPersistentBuffer;
        QualitySettings.realtimeReflectionProbes = _setting.realtimeReflectionProbes;
        QualitySettings.billboardsFaceCameraPosition = _setting.billboardsFaceCameraPosition;
        QualitySettings.resolutionScalingFixedDPIFactor = _setting.resolutionScalingFixedDPIFactor;
        QualitySettings.softParticles = _setting.softParticles;
        QualitySettings.skinWeights = _setting.skinWeights;
        QualitySettings.streamingMipmapsActive = _setting.streamingMipmapsActive;
        QualitySettings.streamingMipmapsMemoryBudget = _setting.streamingMipmapsMemoryBudget;
        QualitySettings.streamingMipmapsRenderersPerFrame = _setting.streamingMipmapsRenderersPerFrame;
        QualitySettings.streamingMipmapsMaxLevelReduction = _setting.streamingMipmapsMaxLevelReduction;
        QualitySettings.streamingMipmapsAddAllCameras = _setting.streamingMipmapsAddAllCameras;
        QualitySettings.streamingMipmapsMaxFileIORequests = _setting.streamingMipmapsMaxFileIORequests;
        QualitySettings.maxQueuedFrames = _setting.maxQueuedFrames;
        QualitySettings.masterTextureLimit = _setting.masterTextureLimit;
        QualitySettings.pixelLightCount = _setting.pixelLightCount;
        QualitySettings.maximumLODLevel = _setting.maximumLODLevel;
        QualitySettings.shadowProjection = _setting.shadowProjection;
        QualitySettings.shadowCascades = _setting.shadowCascades;
        QualitySettings.shadowDistance = _setting.shadowDistance;
        QualitySettings.shadows = _setting.shadows;
        QualitySettings.shadowmaskMode = _setting.shadowmaskMode;
        QualitySettings.shadowNearPlaneOffset = _setting.shadowNearPlaneOffset;
        QualitySettings.shadowCascade2Split = _setting.shadowCascade2Split;
        QualitySettings.shadowCascade4Split = _setting.shadowCascade4Split;
        QualitySettings.lodBias = _setting.lodBias;
        QualitySettings.anisotropicFiltering = _setting.anisotropicFiltering;
        QualitySettings.shadowResolution = _setting.shadowResolution;
    }
    private void ReadAllSetting()
    {
        BmQualitySetting _setting = target as BmQualitySetting;
        _setting.particleRaycastBudget = QualitySettings.particleRaycastBudget;
        _setting.softVegetation = QualitySettings.softVegetation;
        _setting.vSyncCount = QualitySettings.vSyncCount;
        _setting.antiAliasing = QualitySettings.antiAliasing;
        _setting.asyncUploadTimeSlice = QualitySettings.asyncUploadTimeSlice;
        _setting.asyncUploadBufferSize = QualitySettings.asyncUploadBufferSize;
        _setting.asyncUploadPersistentBuffer = QualitySettings.asyncUploadPersistentBuffer;
        _setting.realtimeReflectionProbes = QualitySettings.realtimeReflectionProbes;
        _setting.billboardsFaceCameraPosition = QualitySettings.billboardsFaceCameraPosition;
        _setting.resolutionScalingFixedDPIFactor = QualitySettings.resolutionScalingFixedDPIFactor;
        _setting.softParticles = QualitySettings.softParticles;
        _setting.skinWeights = QualitySettings.skinWeights;
        _setting.streamingMipmapsActive = QualitySettings.streamingMipmapsActive;
        _setting.streamingMipmapsMemoryBudget = QualitySettings.streamingMipmapsMemoryBudget;
        _setting.streamingMipmapsRenderersPerFrame = QualitySettings.streamingMipmapsRenderersPerFrame;
        _setting.streamingMipmapsMaxLevelReduction = QualitySettings.streamingMipmapsMaxLevelReduction;
        _setting.streamingMipmapsAddAllCameras = QualitySettings.streamingMipmapsAddAllCameras;
        _setting.streamingMipmapsMaxFileIORequests = QualitySettings.streamingMipmapsMaxFileIORequests;
        _setting.maxQueuedFrames = QualitySettings.maxQueuedFrames;
        _setting.masterTextureLimit = QualitySettings.masterTextureLimit;
        _setting.pixelLightCount = QualitySettings.pixelLightCount;
        _setting.maximumLODLevel = QualitySettings.maximumLODLevel;
        _setting.shadowProjection = QualitySettings.shadowProjection;
        _setting.shadowCascades = QualitySettings.shadowCascades;
        _setting.shadowDistance = QualitySettings.shadowDistance;
        _setting.shadows = QualitySettings.shadows;
        _setting.shadowmaskMode = QualitySettings.shadowmaskMode;
        _setting.shadowNearPlaneOffset = QualitySettings.shadowNearPlaneOffset;
        _setting.shadowCascade2Split = QualitySettings.shadowCascade2Split;
        _setting.shadowCascade4Split = QualitySettings.shadowCascade4Split;
        _setting.lodBias = QualitySettings.lodBias;
        _setting.anisotropicFiltering = QualitySettings.anisotropicFiltering;
        _setting.shadowResolution = QualitySettings.shadowResolution;

        EditorUtility.SetDirty(target);
    }

    private void LogAllProp()
    {
        BmQualitySetting _setting = target as BmQualitySetting;
        Type a = typeof(BmQualitySetting);

        string sss = "";
        foreach (FieldInfo field in a.GetFields())
        {
            //sss += string.Format("_setting.{0} = QualitySettings.{0};", field.Name);
            sss += string.Format("QualitySettings.{0} = _setting.{0};", field.Name);
            sss += "\r\n";
        }

        Debug.Log(sss);
    }
}
