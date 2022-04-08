using System.Collections;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

using UnityEditor.SceneManagement;



namespace Bm.EditorTool.Scene
{

	public class SceneWindowEditor : EditorWindow
	{
		[MenuItem("Tools/场景管理器/场景速查", false)]
		static void Open()
		{
			EditorWindow.GetWindow<SceneWindowEditor>(false, "场景速查", true).Show();
		}

		[MenuItem("Tools/场景管理器/快速启动  %F5", false)]
		static void StartMainScene()
		{
			if (launchScene == null)
			{
				return;
			}

		}

		[MenuItem("Tools/场景管理器/回到运行前场景 %LEFT", false)]
		static void BackToEditorScene()
		{
			if (SceneManager.GetActiveScene().name.Equals(lastScene.name) == false)
			{
				EditorSceneManager.OpenScene(lastScene.assetName);
			}
		}


		public static SceneToolsSetting setting;
		public static SceneNode launchScene;

		private bool isScan;
		public static SceneNode lastScene;
		public string nameFilter;
		public const string settgingPath = "Assets/BmFramework/EditorHelper/EditorTools/SceneTools/Editor/SceneSetting.asset";
		Vector2 scrollPos;
		private void OnEnable()
		{
			if(setting==null)
            {
				setting = AssetDatabase.LoadAssetAtPath<SceneToolsSetting>(settgingPath);
			}

			if(setting==null)
            {
				setting = ScriptableObject.CreateInstance<SceneToolsSetting>();
				AssetDatabase.CreateAsset(setting, settgingPath);
			}

			setting.scanPath = EditorPrefs.GetString(PlayerSettings.productName + "SceneWindowEditor_Path", setting.scanPath);
			Scan();
		}

		private static SceneNode FindScene(string _name)
		{
			for (int i = 0; i < setting.sceneNodes.Count; i++)
			{
				if (setting.sceneNodes[i].nickName == _name)
				{
					return setting.sceneNodes[i];
				}
			}
			return null;
		}

		bool CheckIsInBuild(SceneNode node)
		{
			EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
			for (int i = 0; i < scenes.Length; ++i)
			{
				if (scenes[i].path.IndexOf(node.assetName.Replace("\\", "/")) >= 0)
				{
					return true;
				}
			}
			return false;
		}


		void Scan()
		{
			string fullPath = setting.scanPath;  //路径

			string current = EditorPrefs.GetString("SceneManager_Launch", "");
			string lastScenestr = EditorPrefs.GetString("SceneManager_Last", "");

			CheckFolder(fullPath, current, lastScenestr);

			setting.sceneNodes.Sort((left, right) =>
				{
					if (launchScene != null)
					{
						if (left.name == launchScene.name)
						{
							return -1;
						}
						if (right.name == launchScene.name)
						{
							return 1;
						}
					}
					return left.name.CompareTo(right.name);
				});

			UpTheLaunch();
		}

		void UpTheLaunch()
		{

		}

		void CheckFolder(string _path, string current, string lastScenestr)
		{
			//获取指定路径下面的所有资源文件

			string[] resFiles = AssetDatabase.FindAssets("t:Scene", new string[] { _path });
			if (resFiles != null)
			{
				for (int i = 0; i < resFiles.Length; i++)
				{
					SceneNode tt = CheckAndAddOne(resFiles[i]);
					

					if (current == tt.nickName && launchScene == null)
					{
						launchScene = tt;
					}

					if (lastScenestr == tt.nickName && lastScene == null)
					{
						lastScene = tt;
					}
				}
				if(resFiles.Length==0)
                {
					setting.sceneNodes.Clear();
				}
			}

		}

		SceneNode CheckAndAddOne(string path)
        {
			string assetName = AssetDatabase.GUIDToAssetPath(path);
			string[] sarr = assetName.Split(new char[] { '/' });
			var name = sarr[sarr.Length - 1];
			var nickName = name.Substring(0, name.IndexOf(".unity"));
			SceneNode tt = FindScene(nickName);
			if(tt==null)
            {
				tt = new SceneNode();
				tt.name = name;
				tt.assetName = assetName;
				tt.fullName = assetName;
				tt.nickName = nickName;
				tt.isInBuild = CheckIsInBuild(tt);
				tt.flag = (FlagType)(1 << (int)FlagType.BuildUsing);

				setting.sceneNodes.Add(tt);
			}
			return tt;
		}

		void OpenScene(SceneNode node, bool isAdd = false)
		{
			if (isAdd == false)
			{
				if (!UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals(node.nickName))
				{
					EditorSceneManager.OpenScene(node.assetName);
				}
			}
			else
			{
				EditorSceneManager.OpenScene(node.assetName, OpenSceneMode.Additive);
			}
		}

		void PlayScene(SceneNode node)
		{
			OpenScene(node);
			EditorApplication.ExecuteMenuItem("Edit/Play");
		}

		void RemoveSceneToBuild(SceneNode node)
		{
			List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
			for (int i = 0; i < scenes.Count; i++)
			{
				if (scenes[i].path.Contains(node.assetName))
				{
					scenes.RemoveAt(i);
				}
			}
			EditorBuildSettings.scenes = scenes.ToArray();
			node.isInBuild = false;
		}

		void AddSceneToBuild(SceneNode node)
		{
			EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

			EditorBuildSettingsScene[] newScene = new EditorBuildSettingsScene[scenes.Length + 1];
			for (int i = 0; i < scenes.Length; ++i)
			{
				newScene[i] = scenes[i];
			}
			newScene[scenes.Length] = new EditorBuildSettingsScene(node.assetName, true);
			EditorBuildSettings.scenes = newScene;
			node.isInBuild = true;
		}

		bool CheckShow(SceneNode t)
        {
			return ((setting.isOnlyShowBuild && t.isInBuild) || !setting.isOnlyShowBuild)
					&& (setting.flagFilter == FlagType.None || IsSelectEventType(t.flag))
					&& (
					(nameFilter != null && nameFilter.Length > 0 && t.nickName.Contains(nameFilter))
					|| nameFilter == null || nameFilter.Length==0
					);

		}
		void OnGUI()
		{
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("刷新", GUILayout.MaxWidth(100)))
			{
				Scan();
			}
			if (GUILayout.Button("清空", GUILayout.MaxWidth(100)))
			{
				setting.sceneNodes.Clear();
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			setting.scanPath = EditorGUILayout.TextField("路径:", setting.scanPath);
			if (GUILayout.Button("保存"))
			{
				EditorPrefs.SetString(PlayerSettings.productName + "SceneWindowEditor_Path", setting.scanPath);
			}
			
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.HelpBox("选项", MessageType.Info);
			GUILayout.Space(5);
			GUILayout.BeginHorizontal();

			EditorGUILayout.LabelField("是否只显示Build场景:", GUILayout.MaxWidth(120));
			setting.isOnlyShowBuild = EditorGUILayout.Toggle(setting.isOnlyShowBuild, GUILayout.MaxWidth(30));
			setting.flagFilter = (FlagType)EditorGUILayout.EnumPopup("", setting.flagFilter, GUILayout.MaxWidth(200));
			nameFilter = EditorGUILayout.TextField(nameFilter);
			if (GUILayout.Button("设置所有为当前Flag"))
			{
				for (int i = 0; i < setting.sceneNodes.Count; i++)
				{
					var t = setting.sceneNodes[i];
					t.flag = (FlagType)(1 << (int)setting.flagFilter);
				}
			}
			if (GUILayout.Button("保存配置"))
			{
				EditorUtility.SetDirty(setting);
				AssetDatabase.SaveAssets();
			}
			GUILayout.EndHorizontal();

			EditorGUILayout.HelpBox("场景列表---------------快速启动：Ctrl+F5   返回启动前的上一场景:Ctrl+Left", MessageType.Info);
			GUILayout.Space(5);
			scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);



			for (int i = 0; i < setting.sceneNodes.Count; i++)
			{
				var t = setting.sceneNodes[i];
				if (t != null && CheckShow(t))
				{
					GUILayout.BeginHorizontal();

#if UNITY_2019_2_4
					int styleId = (launchScene == null) ? 145 : (launchScene.name == t.name ? 149 : 145);
#else
					int styleId = (launchScene == null) ? 158 : (launchScene.name == t.name ? 162 : 158);
#endif

					GUILayout.Label(t.nickName, GUI.skin.customStyles[styleId], GUILayout.MaxWidth(220), GUILayout.MinHeight(30));

					t.flag = (FlagType)EditorGUILayout.EnumMaskPopup("", t.flag, GUILayout.MaxWidth(130));

					GUILayout.Space(5);



#if UNITY_2019_2_4
					GUILayout.Label("", GUI.skin.customStyles[33], GUILayout.MaxWidth(30), GUILayout.MaxHeight(30));
#else
					GUILayout.Label("", GUI.skin.customStyles[189], GUILayout.MaxWidth(30), GUILayout.MaxHeight(30));
#endif



					if (GUILayout.Button("复制名字", GUILayout.MaxWidth(100), GUILayout.MinHeight(30)))
					{
						GUIUtility.systemCopyBuffer = t.nickName;
					}
					if (!t.isInBuild)
					{
						if (GUILayout.Button("添加到Build", GUILayout.MaxWidth(100), GUILayout.MinHeight(30)))
						{
							AddSceneToBuild(t);
						}
					}
					else
					{
						if (GUILayout.Button("从Build中移除", GUILayout.MaxWidth(100), GUILayout.MinHeight(30)))
						{
							RemoveSceneToBuild(t);
						}
					}

					if (launchScene == null || launchScene.name != t.name)
					{
						if (GUILayout.Button("设置为启动场景", GUILayout.MaxWidth(100), GUILayout.MinHeight(30)))
						{
							launchScene = t;
							EditorPrefs.SetString("SceneManager_Launch", launchScene.nickName);

							UpTheLaunch();

						}
					}
					else
					{
#if UNITY_2019_2_4
						GUILayout.Label("启动场景", GUI.skin.customStyles[30], GUILayout.MaxWidth(100), GUILayout.MinHeight(30));
#else
					GUILayout.Label("启动场景", GUI.skin.customStyles[155], GUILayout.MaxWidth(100), GUILayout.MinHeight(30));
#endif

					}



					if (GUILayout.Button("打开", GUILayout.MaxWidth(50), GUILayout.MinHeight(30)))
					{
						OpenScene(t);
					}
					if (GUILayout.Button("叠加打开", GUILayout.MaxWidth(100), GUILayout.MinHeight(30)))
					{
						OpenScene(t, true);
					}
					if (GUILayout.Button("运行", GUILayout.MaxWidth(50), GUILayout.MinHeight(30)))
					{
						PlayScene(t);
					}


					GUILayout.EndHorizontal();


					GUILayout.Space(20);
				}
			}

			GUILayout.EndScrollView();
		}

		public bool IsSelectEventType(FlagType _eventType)
		{
			// 将枚举值转换为int 类型, 1 左移 
			int index = 1 << (int)setting.flagFilter;
			// 获取所有选中的枚举值
			int eventTypeResult = (int)_eventType;
			// 按位 与
			if ((eventTypeResult & index) == index)
			{
				return true;
			}
			return false;
		}
		public static void FastRun()
		{
			if (!SceneManager.GetActiveScene().name.Equals(launchScene.name))
			{
				EditorPrefs.SetString("SceneManager_Last", SceneManager.GetActiveScene().name);
				EditorSceneManager.OpenScene(launchScene.assetName);
			}

			EditorApplication.ExecuteMenuItem("Edit/Play");
		}
	}
}