using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Bm.Helper.Build
{
    public class BuildHelperWindow : EditorWindow
    {
        [MenuItem("Tools/打包小助手", false)]
        static void Open()
        {
            EditorWindow.GetWindow<BuildHelperWindow>(false, "打包小助手", true).Show();
        }


        string _name = "";
        string _version = "";
        int _versonCode = 0;
        string _faceBookId;
        string _packageName;
        ScriptingImplementation buildType;
        Texture2D icon;
        bool isEdit;


        string[] pop_data_achive = new string[] { "NONE", "ARM64", "All" };
        private void OnEnable()
        {
            isEdit = true;

            GetConfig();
        }

        private void OnDisable()
        {
            
        }

        private void OnGUI()
        {
            EditorGUILayout.HelpBox("详细信息", MessageType.Info);
            isEdit = EditorGUILayout.Toggle("是否编辑信息", isEdit);
            if (isEdit)
            {
                _packageName = EditorGUILayout.TextField("包名", _packageName);
                _version = EditorGUILayout.TextField("版本号", _version);
                _versonCode = EditorGUILayout.IntField("构建版本号", _versonCode);
                _name = EditorGUILayout.TextField("应用名称", _name);
                _faceBookId = EditorGUILayout.TextField("FacebookId", _faceBookId);
                buildType = (ScriptingImplementation)EditorGUILayout.EnumPopup("编译模式:", buildType);                                
                icon = (Texture2D)EditorGUILayout.ObjectField("图标", icon, typeof(Texture2D));


                if (GUILayout.Button("更改配置"))
                {
                    SaveConfig();
                }

                if (GUILayout.Button("刷新配置"))
                {
                    GetConfig();
                }

                if (GUILayout.Button("打包"))
                {
                    Build();
                }
            }
        }

        string[] GetBuildScenes()
        {
            List<string> names = new List<string>();
            foreach (EditorBuildSettingsScene e in EditorBuildSettings.scenes)
            {
                if (e == null)
                    continue;
                if (e.enabled)
                    names.Add(e.path);
            }
            return names.ToArray();
        }
        private void Build()
        {
            string path = EditorUtility.SaveFilePanel("选择文件", Application.dataPath, string.Format("{0}.apk", _name), "*.apk");
            BuildPipeline.BuildPlayer(GetBuildScenes(), path, BuildTarget.Android, BuildOptions.None);
        }

        private void GetConfig()
        {
            _packageName = UnityEditor.PlayerSettings.applicationIdentifier;  //包名
            _version = UnityEditor.PlayerSettings.bundleVersion;     //APK版本号
            _name = UnityEditor.PlayerSettings.productName;   //产品名，应用名称             
            _versonCode = UnityEditor.PlayerSettings.Android.bundleVersionCode;
            buildType = PlayerSettings.GetScriptingBackend(BuildTargetGroup.Android);

            #if SDK_FB
            _faceBookId = Facebook.Unity.Settings.FacebookSettings.AppId;
            #endif

            Texture2D[] tmp = PlayerSettings.GetIconsForTargetGroup(BuildTargetGroup.Android, IconKind.Settings);
            icon = tmp[0];

            
        }

        private void SaveConfig()
        {
            UnityEditor.PlayerSettings.bundleVersion = _version;
            UnityEditor.PlayerSettings.applicationIdentifier = _packageName;
            UnityEditor.PlayerSettings.productName = _name;
           
            UnityEditor.PlayerSettings.Android.bundleVersionCode = _versonCode;
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, buildType);

            #if SDK_FB
            Facebook.Unity.Settings.FacebookSettings.AppIds[0] = _faceBookId;
            Facebook.Unity.Settings.FacebookSettings.AppLabels[0] = _name;
            EditorUtility.SetDirty(Facebook.Unity.Settings.FacebookSettings.Instance);
            Facebook.Unity.Editor.ManifestMod.GenerateManifest();
            #endif

            if (buildType == ScriptingImplementation.IL2CPP)
            {
                PlayerSettings.SetArchitecture(BuildTargetGroup.Android, 2);
            }

            _setDefaultIcon(icon);
        }

        private void _setDefaultIcon(Texture2D texture)
        {
            int[] iconSize = PlayerSettings.GetIconSizesForTargetGroup(BuildTargetGroup.Android);
            Texture2D[] textureArray = new Texture2D[iconSize.Length];
            for (int i = 0; i < textureArray.Length; i++)
            {
                textureArray[i] = texture;
            }
            textureArray[0] = texture;
            PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Android, textureArray);

            AssetDatabase.SaveAssets();
        }

        [PostProcessBuild(1)]
        public static void AfterBuild(BuildTarget target, string pathToBuiltProject)
        {
            Debug.Log("Build Success  输出平台: " + target + "  输出路径: " + pathToBuiltProject);
        }
    }
}

